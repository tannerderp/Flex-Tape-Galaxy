using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchSeal : MonoBehaviour
{
    private bool PlayerOn = false;
    private PlayerMovement player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerOn && player.spinning == true)
        {
            player.flying = true;
            player.spinning = false;
            player.GetComponent<PlayerFly>().targetObjectIndex = 0;
            player.GetComponent<PlayerFly>().launcher = gameObject;
            player.GetComponent<PlaySound>().ExecuteSoundPlay(player.GetComponent<PlayerFly>().flySound);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerOn = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerOn = false;
        }
    }
}
