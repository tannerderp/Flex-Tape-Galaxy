using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] public float speed = 0.1f;

    // Update is called once per frame
    void Update()
    {
        transform.position += -transform.right * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            Destroy(gameObject);
        }
    }
}
