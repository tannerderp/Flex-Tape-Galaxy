using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointFlag : MonoBehaviour
{
    [SerializeField] private Sprite checkedSprite;

    private bool Checked = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!Checked && collision.gameObject.tag == "Player")
        {
            GetComponent<SpriteRenderer>().sprite = checkedSprite;
        }
    }
}
