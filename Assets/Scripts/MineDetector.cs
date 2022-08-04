using System;
using System.Collections;
using System.Collections.Generic;
using Akali.Common;
using Akali.Scripts.Utilities;
using DG.Tweening;
using UnityEngine;

public class MineDetector : Singleton<MineDetector>
{
    private SphereCollider collider;

    private void Awake()
    {
        collider = GetComponent<SphereCollider>();
    }

    private void Start()
    {
        BrowseMine();
    }

    private void BrowseMine()
    {
        DOTween.To(()=> collider.radius, x=> collider.radius = x, 5, 1).SetLoops(-1,LoopType.Restart);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Constants.Mines)
        {
            print("MineDetect");
        }
    }
}
