using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBody : MonoBehaviour
{
    [SerializeField] GravityAttractor attractor;
    [SerializeField] private bool isPlayer = false;
    private Transform myTransform;
    private Rigidbody2D rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.gravityScale = 0;
        rigidbody.freezeRotation = true;
        myTransform = transform;
    }

    void FixedUpdate()
    {
        if (!isPlayer || (isPlayer && !GetComponent<PlayerMovement>().LevelOver))
        {
            bool pissOff = false; //i hate every bit of this fixed updates. Why did I have to have this many if statements just so the enemies and player can have the same script for physics.
            bool doAttract = true;
            if (isPlayer)
            {
                if (!GetComponent<PlayerMovement>().flying)
                {
                    pissOff = true;
                    if (attractor && !(attractor.planetType != GravityAttractor.planetTypes.rectangle || (isPlayer && attractor.planetType == GravityAttractor.planetTypes.rectangle && !GetComponent<PlayerMovement>().IsGrounded())))
                    {
                        doAttract = false;
                    }
                }
            }
            else
            {
                pissOff = true;
            }
            if (pissOff)
            {
                if (attractor)
                {
                    attractor.Attract(myTransform);
                }
                if (doAttract)
                {
                    rigidbody.AddForce(-transform.up * 250); //player gravity without the actual gravity. I don't know its weird
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer != 9 && col.gameObject.tag != "NOT A PLANET")
        {
            if (isPlayer)
            {
                GetComponent<PlayerMovement>().canMove = false;
            }
            rigidbody.gravityScale = 0;
            rigidbody.velocity = new Vector2(0, 0);
            rigidbody.angularVelocity = 0;
            GravityAttractor obj = col.GetComponent("GravityAttractor") as GravityAttractor;
            if (obj)
            {
                attractor = obj;
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag != "NOT A PLANET")
        {
            if (isPlayer)
            {
                GetComponent<PlayerMovement>().canMove = true;
            }
            rigidbody.gravityScale = 0;
            if (attractor.gameObject.name == gameObject.name)
            {
                attractor = null;
            }
        }
    }
}
