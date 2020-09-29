using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    public Rigidbody player;
    public Rigidbody target;
    public Rigidbody intercept;
    public Vector3 offset, boxCenter;

    private float velX, velZ,
    velXtemp, velZtemp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("p") && player.name == "Target")
        {
            gameObject.GetComponent<ParticleSystem>().enableEmission = true;
        }   
        else
        {            
            gameObject.GetComponent<ParticleSystem>().enableEmission = false;
        }

        if (player.name == "Intercept" && player.velocity.magnitude > 0.5f)
        {
            gameObject.GetComponent<ParticleSystem>().enableEmission = true;
        }
        
        boxCenter = player.GetComponent<BoxCollider>().center;
        float heightConst = boxCenter.y;

        transform.position = player.transform.TransformPoint(new Vector3 (boxCenter.x, boxCenter.y - heightConst, boxCenter.z));  
        transform.rotation = Quaternion.LookRotation(-transform.up); 
    }
}
