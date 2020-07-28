using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private BoxCollider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Phil Swift")
        {
            GetComponent<SpriteRenderer>().enabled = false;
            Die();
        }
    }

    private void Die()
    {
        GetComponent<AudioSource>().Play();
        Destroy(gameObject, GetComponent<AudioSource>().clip.length);
    }
}
