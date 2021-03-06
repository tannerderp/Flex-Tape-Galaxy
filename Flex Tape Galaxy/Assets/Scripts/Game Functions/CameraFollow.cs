﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private bool followY = true; //whether the camera moves on the y axis with the player or not

    // Update is called once per frame
    void Update()
    {
        if (followY)
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        }
    }
}
