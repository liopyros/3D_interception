using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interception : MonoBehaviour
{
    public Rigidbody target;
    public Rigidbody intercept;
    public GameObject explosionEffect;
    public bool hasExploded = false;
    public float timeFactor = 1f;
    private float currentElapsed = 0f;

    private Vector3 interceptP, targetP,
    interceptV, targetV, 
    interceptA, targetA;
    private Vector3 gravity = Physics.gravity;
    private Vector3 forceVector, tempVector;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (tempVector != Vector3.zero)
        {
            forceVector = tempVector;
        }

        targetP = target.transform.TransformPoint(target.centerOfMass);  
        targetV = target.velocity;
        interceptP = intercept.transform.TransformPoint(intercept.centerOfMass); 
        interceptV = intercept.velocity;
        
        float targetVX = transform.TransformDirection(target.velocity).x;
        float targetVY = transform.TransformDirection(target.velocity).y;
        float targetVZ = transform.TransformDirection(target.velocity).z;
        
        float targetVXmag = target.velocity.x;
        float targetVYmag = target.velocity.y;
        float targetVZmag = target.velocity.z;
        
        if (Input.GetKey("m"))
        {
            float interceptVX = (targetVX * timeFactor + targetP.x - interceptP.x) / timeFactor;
            float interceptVY = (targetVY * timeFactor + targetP.y - interceptP.y) / timeFactor;
            float interceptVZ = (targetVZ * timeFactor + targetP.z - interceptP.z) / timeFactor;

            intercept.velocity = new Vector3 (interceptVX, interceptVY, interceptVZ);
        }
        if (Input.GetKey("b"))
        {
            if (intercept.position.y < 1)
            {
                float interceptAX = ((targetP.x - interceptP.x) + targetVX * timeFactor) * 2f / (timeFactor * timeFactor);
                float interceptAY = ((targetP.y - interceptP.y) + targetVY * timeFactor) * 2f / (timeFactor * timeFactor);
                float interceptAZ = ((targetP.z - interceptP.z) + targetVZ * timeFactor) * 2f / (timeFactor * timeFactor);  
                
                forceVector = new Vector3 (interceptAX, interceptAY, interceptAZ);   

                currentElapsed = Time.time;
            }
        }
    
        intercept.AddForce(intercept.transform.InverseTransformDirection(forceVector));
        
        Debug.DrawLine(interceptP, interceptP + (forceVector*100), Color.white);

        if (intercept.position.y < target.position.y)
        {
            tempVector = forceVector;
        }
        else
        {
            tempVector = Vector3.zero;
        }

        if (currentElapsed != 0f && (Time.time - currentElapsed) > timeFactor*0.95)  
        {
            Explode();
            hasExploded = true;
        }
    }

    void Explode()
    {
        if (!hasExploded)
        {
            Instantiate(explosionEffect, intercept.transform.position, intercept.transform.rotation);

            Destroy(target.gameObject);
            Destroy(intercept.gameObject);
        }
    }
}
