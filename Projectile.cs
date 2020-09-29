using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    public Rigidbody missile;
    public Rigidbody mask;
    public float velY = 20f;
    public float velX = 0f;
    public float velZ = 0f;
    public float throttleSpeed = 2f;
    private Vector3 velocityVector, tempVector;
    private Quaternion rotCorrect = Quaternion.Euler(90, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        velocityVector = new Vector3 (0, velY, 0);
        tempVector = velocityVector;        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey("p"))
        {
            missile.AddForce(transform.up * velY);
            
            if (Input.GetKey("w"))
            {
                missile.AddTorque(transform.right, ForceMode.Acceleration);
            }
            if (Input.GetKey("s"))
            {
                missile.AddTorque(-transform.right, ForceMode.Acceleration);
            }
            if (Input.GetKey("a"))
            {
                missile.AddTorque(transform.forward, ForceMode.Acceleration);
            }
            if (Input.GetKey("d"))
            {
                missile.AddTorque(-transform.forward, ForceMode.Acceleration);
            }
        }        

    }
}
