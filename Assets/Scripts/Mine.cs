using System;
using System.Collections;
using System.Collections.Generic;
using Akali.Scripts.Managers;
using Akali.Scripts.Utilities;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public GameObject ground;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            AkaliLevelManager.Instance.LevelIsFail();
            //ragdoll
            //particle
        }

        if (other.gameObject.layer == 12)
        {
            ground = other.gameObject;
        }
    }
}
