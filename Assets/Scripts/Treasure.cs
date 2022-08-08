using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    public GameObject tresureObj;
    public Transform treasureParent;
    private Animator animator;
    private string chestOpen = "ChestOpen";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OpenChest()
    {
        Instantiate(tresureObj, treasureParent.position, treasureParent.rotation, treasureParent);
        animator.Play(chestOpen);
    }
    
}
