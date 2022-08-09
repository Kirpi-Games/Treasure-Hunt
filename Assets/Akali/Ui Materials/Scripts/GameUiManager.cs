using System;
using Akali.Common;
using Akali.Scripts.Managers.StateMachine;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Akali.Ui_Materials.Scripts
{
    public class GameUiManager : Singleton<GameUiManager>
    {
        public GameObject mainMenuTutorial;
        public GameObject playingLevel;
        public GameObject playingCoinBar;
        public GameObject background;
        public GameObject completeUi;
        public GameObject completeButton;
        public GameObject failUi;
        public GameObject failButton;
        public GameObject inGame;
        
        
        //Game
        public TextMeshProUGUI notifText, keybar;
        
        private void Awake()
        {
            GameStateManager.Instance.GameStateMainMenu.OnEnter += SetActiveMainMenuUi;
            GameStateManager.Instance.GameStateMainMenu.OnExit += SetActiveMainMenuUi;
            GameStateManager.Instance.GameStatePlaying.OnEnter += SetActivePlayingUi;
            GameStateManager.Instance.GameStatePlaying.OnExit += SetActivePlayingUi;
            GameStateManager.Instance.GameStateComplete.OnEnter += SetActiveCompleteUi;
            GameStateManager.Instance.GameStateComplete.OnExit += SetActiveCompleteUi;
            GameStateManager.Instance.GameStateFail.OnEnter += SetActiveFailUi;
            GameStateManager.Instance.GameStateFail.OnExit += SetActiveFailUi;
        }

        private void OnDestroy()
        {
            GameStateManager.Instance.GameStateMainMenu.OnEnter -= SetActiveMainMenuUi;
            GameStateManager.Instance.GameStateMainMenu.OnExit -= SetActiveMainMenuUi;
            GameStateManager.Instance.GameStatePlaying.OnEnter -= SetActivePlayingUi;
            GameStateManager.Instance.GameStatePlaying.OnExit -= SetActivePlayingUi;
            GameStateManager.Instance.GameStateComplete.OnEnter -= SetActiveCompleteUi;
            GameStateManager.Instance.GameStateComplete.OnExit -= SetActiveCompleteUi;
            GameStateManager.Instance.GameStateFail.OnEnter -= SetActiveFailUi;
            GameStateManager.Instance.GameStateFail.OnExit -= SetActiveFailUi;
        }

        public void Notif(String x, Color c)
        {
            notifText.text = x;
            notifText.color = c;
            notifText.gameObject.transform.DOScale(1, 0.7f);
            Invoke("HideNotif",1.5f);
        }

        public void HideNotif()
        {
            notifText.gameObject.transform.DOScale(0, 0.4f);
        }
        
        public void SetActiveMainMenuUi()
        {
            mainMenuTutorial.SetActive(!mainMenuTutorial.activeSelf);
        }

        public void SetActivePlayingUi()
        {
            playingLevel.SetActive(!playingLevel.activeSelf);
            playingCoinBar.SetActive(!playingCoinBar.activeSelf);
            inGame.SetActive(!inGame.activeSelf);
        }

        private void SetActiveCompleteUi()
        {
            background.SetActive(!background.activeSelf);
            completeUi.SetActive(!completeUi.activeSelf);
            completeButton.SetActive(!completeButton.activeSelf);
        }

        private void SetActiveFailUi()
        {
            background.SetActive(!background.activeSelf);
            failUi.SetActive(!failUi.activeSelf);
            failButton.SetActive(!failButton.activeSelf);
        }
    }
}