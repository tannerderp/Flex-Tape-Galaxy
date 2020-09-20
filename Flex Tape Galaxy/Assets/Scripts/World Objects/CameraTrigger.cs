using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField] private GameObject cam;
    [SerializeField] private GameObject mainCam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        mainCam.GetComponent<Camera>().enabled = false;
        cam.GetComponent<Camera>().enabled = true;
    }
}
