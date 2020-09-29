using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform intercept;
    public Transform target;
    public float camHeight = 2.0f;
    public float camDistance = 10.0f;
    public float camLookOffset = 5f;
    public float switchCameraHeight = 75f;
    public float aerialCamHeight = 30f;
    public float aerialCamDistance = 15f;
    private Vector3 camOffset, direction;
    public bool targetHit = false;
    private float angleToTarget, angleX, angleZ, resolvedPitch,
    camX, camZ, radians,
    rotX, rotY, rotZ;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!targetHit) 
        {
            angleToTarget = (float)transform.eulerAngles.y;
            angleX = (float)transform.eulerAngles.x;
            angleZ = (float)transform.eulerAngles.z;

            resolvedPitch = Mathf.Sqrt(angleX*angleX + angleZ*angleZ);

            if (target.position.y < switchCameraHeight)
            {
                camX = -camDistance*Mathf.Sin(angleToTarget*3.14159f/180f);
                camZ = -camDistance*Mathf.Cos(angleToTarget*3.14159f/180f);

                float offsetX = camLookOffset*Mathf.Cos(angleToTarget*3.14159f/180f);
                float offsetZ = camLookOffset*Mathf.Sin(angleToTarget*3.14159f/180f);

                camOffset = new Vector3 (camX + offsetX, camHeight, camZ + offsetZ);
                transform.position = intercept.position + camOffset;
                transform.LookAt(target);
            }
            else
            {
                camX = -aerialCamDistance*Mathf.Sin(angleToTarget*3.14159f/180f);
                camZ = -aerialCamDistance*Mathf.Cos(angleToTarget*3.14159f/180f);

                float offsetX = camLookOffset*Mathf.Cos(angleToTarget*3.14159f/180f);
                float offsetZ = camLookOffset*Mathf.Sin(angleToTarget*3.14159f/180f);

                camOffset = new Vector3 (camX + offsetX, aerialCamHeight, camZ + offsetZ);
                transform.position = target.position + camOffset;
                transform.LookAt(intercept);
            }
        }
        else
        {
            transform.position = target.position + camOffset;
            transform.LookAt(new Vector3 (0, 1, 0));
        }
    }
}
