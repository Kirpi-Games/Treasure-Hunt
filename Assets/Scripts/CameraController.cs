using System.Collections;
using System.Collections.Generic;
using Akali.Common;
using DG.Tweening;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    public GameObject target;
    public float smoothSpeed;
    public Vector3 offset;
    public Vector3 lookatOffset;
    private Vector3 treasureOffset = new Vector3(2, 2, -2);
    private Vector3 finalOffset = new Vector3(0.5f, 1, -2);
    private Vector3 normalOffset = new Vector3(0, 7, -7);
    public bool isFollow;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        isFollow = true;
    }
    
    public void Treasure()
    {
        DOTween.To(()=> offset, x=> offset = x, treasureOffset, 0.5f);
        Invoke("NormalOffset",1f);
    }

    private void NormalOffset()
    {
        DOTween.To(()=> offset, x=> offset = x, normalOffset, 0.5f);
    }
    
    public void Final()
    {
        DOTween.To(()=> offset, x=> offset = x, finalOffset, 0.5f);
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

        
    }
}
