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
        DOTween.To(()=> collider.radius, x=> collider.radius = x, 9, 0.7f).SetLoops(-1,LoopType.Restart);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Constants.Mines)
        {
            print("MineDetect");
            Taptic.Medium();
            other.GetComponent<Mine>().ground.GetComponent<MeshRenderer>().material.DOColor(Color.red, 0.2f).OnComplete(() => other.GetComponent<Mine>().ground.GetComponent<MeshRenderer>().material.DOColor(Color.white, 0.2f));
        }
        
        if (other.gameObject.layer == Constants.Key)
        {
            Taptic.Medium();
            other.GetComponent<Key>().ground.GetComponent<MeshRenderer>().material.DOColor(Color.green, 0.2f).OnComplete(() => other.GetComponent<Key>().ground.GetComponent<MeshRenderer>().material.DOColor(Color.white, 0.2f));
        }
    }
}
