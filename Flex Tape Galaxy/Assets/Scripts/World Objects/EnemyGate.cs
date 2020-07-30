using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGate : MonoBehaviour
{
    private GameObject parent;
    private int targetChildCount; //what the child count needs to be at for the gate to open

    [SerializeField] private int enemyCount; //how many enemies need to be destroyed before the gate opens

    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.gameObject;
        targetChildCount = parent.transform.childCount - enemyCount;
    }

    // Update is called once per frame
    void Update()
    {
        if(parent.transform.childCount == targetChildCount)
        {
            Destroy(gameObject);
        }
    }
}
