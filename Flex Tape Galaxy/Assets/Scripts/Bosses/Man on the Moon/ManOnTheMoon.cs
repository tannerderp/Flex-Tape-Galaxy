using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManOnTheMoon : MonoBehaviour
{ //no way this is efficient...
    //prefabs
    [SerializeField] private GameObject AsteroidPrefab;
    [SerializeField] private GameObject Player;

    //different positions
    //scale positions
    private Vector3 InBetween = new Vector3(1.568292f, 1.568292f, 1.568292f);
    private Vector3 AsteroidScale = new Vector3(0.9707728f, 0.9707728f, 0.9707728f);
    private Vector3 AsteroidPos = new Vector3(0.57f, 5.251f, 0);

    private int mainCounter = 0;
    private int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = InBetween;
    }

    // Update is called once per frame
    void Update()
    {
        AsteroidPhase();
    }

    private void AsteroidPhase()
    {
        if (transform.localScale != AsteroidScale)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, AsteroidScale, 1 * Time.deltaTime);
            transform.position += -transform.up * 4 * Time.deltaTime;
            mainCounter = 0;
            counter = 0;
        }
        else
        {
            mainCounter++;
            if(mainCounter < 180)
            {
                counter++;
                if(counter >= 30)
                {
                    counter = 0;
                    GameObject A = Instantiate(AsteroidPrefab) as GameObject;
                    A.transform.position = new Vector3(Random.Range(345.8f, 368.79f), 69.45f, 0);
                    Quaternion rotation = Quaternion.LookRotation(Player.transform.position - A.transform.position, A.transform.TransformDirection(Vector3.up));
                    A.transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
                    A.GetComponent<Asteroid>().speed = Random.Range(0.075f, 0.2f);
                }
            }
        }
    }
}
