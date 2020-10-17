﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Phil Swift" || collision.gameObject.tag == "Enemy")
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
