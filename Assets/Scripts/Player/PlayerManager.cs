using System;
using System.Collections;
using System.Collections.Generic;
using Akali.Common;
using Akali.Scripts.Managers;
using Akali.Scripts.Managers.StateMachine;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    public bool haveKey;
    
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

    public IEnumerator CompleteGame()
    {
        yield return new WaitForSeconds(2);
        AkaliLevelManager.Instance.LevelIsCompleted();
    }
}
