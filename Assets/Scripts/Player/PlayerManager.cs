using System;
using System.Collections;
using System.Collections.Generic;
using Akali.Common;
using Akali.Scripts.Managers;
using Akali.Scripts.Managers.StateMachine;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    private void Awake()
    {
        GameStateManager.Instance.GameStateMainMenu.OnExecute += StartGame;
    }

    private void StartGame()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AkaliLevelManager.Instance.LevelIsPlaying();
        }
    }
}
