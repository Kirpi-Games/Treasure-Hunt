using System;
using System.Collections;
using System.Collections.Generic;
using Akali.Common;
using Akali.Scripts.Managers.StateMachine;
using Akali.Scripts.Utilities;
using DG.Tweening;
using UnityEngine;

public class MineDetector : Singleton<MineDetector>
{
    private SphereCollider collider;
    public Color color;
    public bool isTrigger;

    private void Awake()
    {
        collider = GetComponent<SphereCollider>();
    }

    private void Start()
    {
        BrowseMine();
        GameStateManager.Instance.GameStatePlaying.OnExecute += SetTrigger;
    }

    private void BrowseMine()
    {
        DOTween.To(()=> collider.radius, x=> collider.radius = x, 0.6f, 0.7f).SetLoops(-1,LoopType.Restart);
        transform.DOScale(9,0.7f).SetLoops(-1,LoopType.Restart);
    }

    public void SetTrigger()
    {
        if (collider.radius >= 0.59f)
        {
            isTrigger = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == Constants.Mines)
        {
            if (!isTrigger)
            {
                print("MineDetect");
                Taptic.Heavy();
                isTrigger = true;
            }
            
            other.GetComponent<Mine>().ground.GetComponent<MeshRenderer>().material.DOColor(Color.red, 0.2f).OnComplete(() => other.GetComponent<Mine>().ground.GetComponent<MeshRenderer>().material.DOColor(color, 0.2f));
        }
        
        if (other.gameObject.layer == Constants.Key)
        {
            //Taptic.Medium();
            //other.GetComponent<Key>().ground.GetComponent<MeshRenderer>().material.DOColor(Color.green, 0.2f).OnComplete(() => other.GetComponent<Key>().ground.GetComponent<MeshRenderer>().material.DOColor(color, 0.2f));
        }
    }
}
