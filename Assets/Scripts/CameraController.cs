using System.Collections;
using System.Collections.Generic;
using Akali.Common;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    public GameObject target;
    public float smoothSpeed;
    public Vector3 offset;
    public Vector3 lookatOffset,startOffset;
    public bool isFollow,isFinal;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        isFollow = true;
    }
    
    public void Final()
    {
        offset = Vector3.Lerp(offset, new Vector3(0,1f, 3), 1f * Time.deltaTime);
    }
    
    public void CameraFollow()
    {
        if (target == null) return;
    
        if (target != null)
        {
            Vector3 desiredPosition = new Vector3(target.transform.position.x,target.transform.position.y,target.transform.position.z) + offset;
            Vector3 smoothed = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothed;
            transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z);
            Vector3 lookAtTarget = new Vector3(target.transform.position.x,0,target.transform.position.z) + lookatOffset;
            transform.LookAt(new Vector3(lookAtTarget.x,lookAtTarget.y,lookAtTarget.z));    
        }
    
    }
    
    private void LateUpdate()
    {
            
        if (target == null)
        {
            return;
        }
    
        if (isFollow)
        {
            CameraFollow();
        }

        if (isFinal)
        {
            Final();
        }
    }
}
