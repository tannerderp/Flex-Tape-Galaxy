using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManOnTheMoon : MonoBehaviour
{ //no way this is efficient...
    //prefabs
    [SerializeField] private GameObject AsteroidPrefab;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject LeftArm;
    [SerializeField] private GameObject RightArm;

    //different positions
    //scale positions
    private Vector3 InBetween = new Vector3(1.568292f, 1.568292f, 1.568292f);
    private Vector3 InBetweenPos;
    private Vector3 AsteroidScale = new Vector3(0.9707728f, 0.9707728f, 0.9707728f);
    private Vector3 AsteroidPos = new Vector3(0.57f, 5.251f, 0);
    private Vector3 HandSweepScale = new Vector3(2.539504f, 2.539504f, 2.539504f);
    private Vector3 HandSweepPos = new Vector3(0.5700111f, 14.5f, 0);

    private int mainCounter = 0;
    private int counter = 0;
    private string currentPhase = "Asteroid";
    private int phasesIndex = 0;
    private string[] phases = new string[] { "Asteroid", "Hand Sweep" };

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = InBetween;
        InBetweenPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPhase == "Asteroid")
        {
            AsteroidPhase();
        } else if(currentPhase == "In Between")
        {
            InBetweenPhase();
        } else if(currentPhase == "Hand Sweep")
        {
            HandSweepPhase();
        }
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
            else
            {
                counter = 0;
                if (mainCounter >= 250) { currentPhase = "In Between"; mainCounter = 0; }
            }
        }
    }
    private void HandSweepPhase()
    {
        if (transform.localScale != HandSweepScale && transform.localPosition != HandSweepPos)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, HandSweepScale, 1 * Time.deltaTime);
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, HandSweepPos, 4 * Time.deltaTime);
            mainCounter = 0;
            counter = 0;
        }
        else
        {
            if (mainCounter >= 900)
            {
                LeftArm.GetComponent<Arm>().IsArmPhase = false;
                LeftArm.SetActive(false);
                RightArm.SetActive(false);
                RightArm.GetComponent<Arm>().IsArmPhase = false;
                counter = 0;
                mainCounter = 0;
                LeftArm.GetComponent<Arm>().counter = 0;
                RightArm.GetComponent<Arm>().counter = 0;
                currentPhase = "In Between";
            }
            else if(mainCounter<450)
            {
                LeftArm.GetComponent<Arm>().IsArmPhase = true;
                LeftArm.SetActive(true);
                mainCounter++;
            } else if (mainCounter < 900)
            {
                LeftArm.GetComponent<Arm>().IsArmPhase = false;
                LeftArm.SetActive(false);
                RightArm.GetComponent<Arm>().IsArmPhase = true;
                RightArm.SetActive(true);
                mainCounter++;
            }
        }
    }
    private void InBetweenPhase()
    {
        counter++;
        if (transform.localScale != InBetween && transform.position != InBetweenPos)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, InBetween, 1 * Time.deltaTime);
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, InBetweenPos, 4 * Time.deltaTime);
        }
        else
        {
            if(transform.localPosition != InBetweenPos)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, InBetweenPos, 4 * Time.deltaTime);
            }
            else
            {
                if (counter >= 180)
                {
                    phasesIndex++;
                    if (phasesIndex > phases.Length - 1){
                        phasesIndex = 0;
                    }
                    counter = 0;
                    mainCounter = 0;
                    currentPhase = phases[phasesIndex];
                }
            }
        }
    }
}
