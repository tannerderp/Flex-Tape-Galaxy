using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{
    public bool IsArmPhase = false;
    [SerializeField] private Sprite poppedSprite;
    [SerializeField] private int direction = 1;
    [SerializeField] private float slowMoveAmount = 4f;
    [SerializeField] private float fastMoveAmount = 12f;
    private Vector3 originalPos;

    public int counter = 0;
    private bool popped = false;

    private void Start()
    {
        originalPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsArmPhase)
        {
            counter++;
            if(counter <= 120)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, transform.localPosition * slowMoveAmount * -direction, 2 * Time.deltaTime);
            }
            else if(counter<=240)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, transform.localPosition * fastMoveAmount * -direction, 8 * Time.deltaTime);
            }
            else if(transform.localPosition != originalPos)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, originalPos, 8 * Time.deltaTime);
            }
            else
            {
                IsArmPhase = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.GetComponent<PlayerMovement>().spinning == true)
            {
                popped = true;
                GetComponent<SpriteRenderer>().sprite = poppedSprite;
                if (IsArmPhase)
                {
                    counter = 241;
                }
            }
            else
            {
                collision.gameObject.GetComponent<PlayerMovement>().BossHit();
                counter = 241;
                transform.localPosition = originalPos;
            }
        }
    }
}
