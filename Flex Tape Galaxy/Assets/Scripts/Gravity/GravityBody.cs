using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityBody : MonoBehaviour
{
    [SerializeField] GravityAttractor attractor;
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
        if (!GetComponent<PlayerMovement>().flying && !(GetComponent<PlayerMovement>().IsGrounded() && attractor.planetType == GravityAttractor.planetTypes.rectangle))
        {
            if (attractor)
            {
                attractor.Attract(myTransform);
            }
            rigidbody.AddForce(-transform.up * 250); //player gravity without the actual gravity. I don't know its weird
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer != 9)
        {
            GetComponent<PlayerMovement>().canMove = false;
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
        GetComponent<PlayerMovement>().canMove = true;
        rigidbody.gravityScale = 0;
        if (attractor.gameObject.name == gameObject.name)
        {
            attractor = null;
        }
    }
}
