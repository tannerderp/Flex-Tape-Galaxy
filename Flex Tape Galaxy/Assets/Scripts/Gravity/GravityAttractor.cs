using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAttractor : MonoBehaviour //The planet attracting objects when they're in the gravity field
{ 

    [SerializeField] float gravity = 9.8f; //dont worry neither of these variables actually do anything
    [SerializeField] float rotationSmoothness = 5f;
    public enum planetTypes //drop down list
    {
        sphere,
        rectangle
    };
    [SerializeField] public planetTypes planetType = planetTypes.sphere;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Attract(Transform body)
    {
        Vector3 gravityUp = (body.transform.position - transform.position).normalized;
        Vector3 localUp = body.transform.up;
        if (planetType == planetTypes.sphere)
        {
            Quaternion targetRotation = Quaternion.FromToRotation(localUp, gravityUp) * body.rotation;
            body.rotation = targetRotation;
        } else if(planetType == planetTypes.rectangle)
        {
            if (Vector2.Distance(body.position, transform.position) >= 0) //note to self. Never go upside down on a rectangle, just make another rectangle and put it under it and flip the rotation. Calculations are hard...
            {
                body.rotation = transform.rotation;
            }
            else
            {
                body.rotation = new Quaternion(body.rotation.x, body.rotation.y, -transform.rotation.z, body.rotation.w);
            }
        }
    }
}
