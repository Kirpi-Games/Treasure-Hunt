using System;
using System.Collections;
using System.Collections.Generic;
using Akali.Scripts.Managers.StateMachine;
using Akali.Scripts.Utilities;
using UnityEngine;

public class Final : MonoBehaviour
{
    public GameObject door;

    private void Start()
    {
        GameStateManager.Instance.GameStatePlaying.OnExecute += CheckDoorStatus;
    }

    void CheckDoorStatus()
    {
        if (PlayerManager.Instance.haveKey)
        {
            door.layer = Constants.Door;
        }
        else
        {
            door.layer = Constants.Barrier;
        }
    }
    
}
