using System.Collections;
using System.Collections.Generic;
using Akali.Ui_Materials.Scripts;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject ground;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 3)
        {
            PlayerManager.Instance.haveKey = true;
            
            //particle
        }

        if (other.gameObject.layer == 12)
        {
            ground = other.gameObject;
        }
    }
}
