using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    //REMEMBER, you have to put a kinematic rigidbody2d on the actual moving platform object or else the triggers won't sense anything.
    [SerializeField] private GameObject[] movePoints;
    [SerializeField] private float moveSpeed = 0.1f;

    private int pointIndex = 0;

    // Update is called once per frame
    void Update()
    {
        Vector2 target = new Vector2(movePoints[pointIndex].transform.position.x, movePoints[pointIndex].transform.position.y);
        transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), target, moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Moving Platform Trigger")
        {
            pointIndex++;
            if (pointIndex > movePoints.Length - 1)
            {
                pointIndex = 0;
            }
        }
    }
}
