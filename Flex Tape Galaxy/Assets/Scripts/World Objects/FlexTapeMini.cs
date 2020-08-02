using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlexTapeMini : MonoBehaviour
{
    private bool collected = false;
    private GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Phil Swift")
        {
            GetComponent<AudioSource>().Play();
            player = collision.gameObject;
            collected = true;
            player.GetComponent<PlayerMovement>().LevelOver = true;
        }
    }

    private void Update()
    {
        if (collected)
        {
            player.GetComponent<Animator>().SetBool("Dabbing", true);
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }
}
