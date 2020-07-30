using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchGravityPlatform : MonoBehaviour
{
    [SerializeField] private GameObject dumb;

    private void OnTriggerEnter2D(Collider2D collision) //i have to do this script in a child object because you cant do get component with two colliders in the parent
    {
        transform.parent.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        dumb.active = true;
        gameObject.active = false;
    }
}
