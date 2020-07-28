using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFly : MonoBehaviour
{
    [SerializeField] public AudioClip flySound;

    [SerializeField] private float flySpeed = 10;
    
    public GameObject launcher;

    public int targetObjectIndex = 0;
    private GameObject targetObject;

    public void Fly()
    {
        targetObject = launcher.transform.GetChild(targetObjectIndex).gameObject;
        Vector3 vectorToTarget = targetObject.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg-90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 5);
        transform.position += transform.up * flySpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GetComponent<PlayerMovement>().flying == true)
        {
            targetObjectIndex++;
            if(targetObjectIndex > 1)
            {
                GetComponent<PlayerMovement>().flying = false;
            }
        }
    }
}
