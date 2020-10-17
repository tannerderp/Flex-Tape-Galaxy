using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpVel;
    [SerializeField] float airSpinBoost; //how much velocity is added vertically if the player spins in air
    [SerializeField] LayerMask layerMask;
    [SerializeField] AudioClip spinSound;
    [SerializeField] AudioClip hurtSound;

    public float direction = 1f;
    public bool canMove = true;
    public int health = 3;
    public bool LevelOver = false;

    private Vector3 horizontalVel; //The velocity going left to right depending on the rotation of phil. Physics are weird.
    public bool spinning = false;
    private int spinCooldown = 110;
    private bool airSpin = false; //whether or not the spin is done in the air or on the ground. this is used to do spin cooldown crap
    public bool flying = false;
    private int jumpCooldown = 10; //once again i shouldn't have to make this int. But frickin physics and bad code required me to

    Rigidbody2D rigidBody;
    BoxCollider2D boxCollider;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!LevelOver)
        {
            Movement();
        }
    }

    private void Movement()
    {
        if (flying == false)
        {
            animator.SetBool("Flying", false);
            if (animator.GetBool("Hurt") == false)
            {
                //rigidBody.gravityScale = 0;
                var horizontal = Input.GetAxis("Horizontal"); //movement
                var rawHorizontal = Input.GetAxisRaw("Horizontal");
                //rigidBody.AddForce(rigidBody.transform.right*(horizontal*moveSpeed), ForceMode2D.Impulse);
                horizontalVel = rigidBody.transform.right * (horizontal * moveSpeed);
                horizontalVel *= 0.1f;
                //velChange = horizontalVel - previousHorizontalVel;
                transform.position += new Vector3(horizontalVel.x * 0.25f, horizontalVel.y * 0.25f, horizontalVel.z * 0.25f);

                jumpCooldown++;
                if (IsGrounded() && (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))) //jumping
                {
                    rigidBody.AddForce(transform.up * jumpVel);
                    spinCooldown = 115;
                    jumpCooldown = 0;
                }
                if (IsGrounded())
                {
                    if (!(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && horizontal == 0 && animator.GetBool("Jumping") == false && jumpCooldown > 20)
                    {
                        rigidBody.velocity = new Vector2(0, 0);
                    }
                    animator.SetBool("Jumping", false);
                }
                else if(!LevelOver)
                {
                    animator.SetBool("Jumping", true);
                }

                if (rawHorizontal != 0) //direction player is facing
                {
                    direction = rawHorizontal;
                }
                var absoluteValue = Mathf.Abs(transform.localScale.x);
                var change = absoluteValue * direction;
                transform.localScale = new Vector2(change, transform.localScale.y);
                animator.SetFloat("Speed", Mathf.Abs(horizontal * moveSpeed));
                spinCooldown++;
                var animationTime = 0.16f / 40;
                if (!spinning)
                {
                    if ((Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Space)) && spinCooldown > 120)
                    {
                        if (!IsGrounded())
                        {
                            rigidBody.velocity = new Vector2(0, 0);
                            rigidBody.AddForce(transform.up * airSpinBoost);
                            airSpin = true;
                        }
                        else
                        {
                            //Debug.Log(true);
                            airSpin = false;
                        }
                        spinning = true;
                        animator.SetBool("Spinning", true);
                        spinCooldown = 0;
                        GetComponent<PlaySound>().ExecuteSoundPlay(spinSound);
                    }
                }
                else
                {
                    if (spinCooldown == 1)
                    {
                        animator.SetBool("Spinning", false);
                    }
                    if (spinCooldown > 120)
                    {
                       // Debug.Log("Reset");
                        spinning = false;
                        animator.SetBool("Spinning", false);
                    }
                    if (IsGrounded() && airSpin == true)
                    {
                        spinning = false;
                        spinCooldown = 115;
                    }
                }
            }
            else
            {
                if (IsGrounded() && rigidBody.velocity.y < 0.1)
                {
                    //canMove = true;
                    animator.SetBool("Hurt", false);
                }
            }
        }
        else
        {
            animator.SetBool("Spinning", false);
            animator.SetBool("Flying", true);
            rigidBody.velocity = new Vector2(0, 0);
            GetComponent<PlayerFly>().Fly();
        }
    }

    public bool IsGrounded()
    {
        RaycastHit2D boxCast = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, -transform.up, 0.1f, layerMask);
        return boxCast.collider != null; 
    }

    public void HurtPlayer(Collision2D collision)
    {
        rigidBody.velocity = new Vector2(0, 0);
        float enemyDirection = -(collision.gameObject.transform.localScale.x / Mathf.Abs(collision.gameObject.transform.localScale.x));
        direction = -enemyDirection;
        transform.localScale = new Vector2(transform.localScale.x * direction, transform.localScale.y);
        canMove = false;
        animator.SetBool("Hurt", true);
        transform.position += transform.up * 2;
        rigidBody.AddForce(transform.right * -direction * 2000f);
        health--;
        GetComponent<PlaySound>().ExecuteSoundPlay(hurtSound);
        FindObjectOfType<HealthMeter>().ChangeSprite(health);
    }

    public void BossHit() //man i wish i didn't have to be so inefficient because i cant call the hurtplayer function from another script because of the FRICKIN COLLISION DATA
    {
        rigidBody.velocity = new Vector2(0, 0);
        float enemyDirection = direction;
        direction = -enemyDirection;
        transform.localScale = new Vector2(transform.localScale.x * direction, transform.localScale.y);
        canMove = false;
        animator.SetBool("Hurt", true);
        transform.position += transform.up * 2;
        rigidBody.AddForce(transform.right * -direction * 2000f);
        health--;
        GetComponent<PlaySound>().ExecuteSoundPlay(hurtSound);
        FindObjectOfType<HealthMeter>().ChangeSprite(health);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if((collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Boss") && animator.GetBool("Hurt") == false)
        {
            if (!spinning && !LevelOver)
            {
                HurtPlayer(collision);
            }
            if (collision.gameObject.tag != "Boss")
            {
                Destroy(collision.gameObject);
            }
        }
    }

    /*private void OnTriggerEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Boss")
        {
            HurtPlayer(collision);
        }
    }*/
}
