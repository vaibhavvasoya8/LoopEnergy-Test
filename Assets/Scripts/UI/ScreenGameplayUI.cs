using System;
using UnityEngine;
using UnityEngine.UI;
using GamePlay;

namespace UIKit
{
    public class ScreenGameplayUI : UIScreenBase
    {
        [SerializeField] Text levelText;
        [SerializeField] Button settings;
        [SerializeField] Button restart;
        
        void Start()
        {
            settings.onClick.AddListener(OpenSettings);
            restart.onClick.AddListener(Restart);
        }

        public override void Show(Action Callback = null)
        {
            base.Show(Callback);
            levelText.text = "Level-" + (LevelManager.instance.currentLevelIndex+1);
            GameManager.instance.cameraController.FocusCamera();
        }

        void OpenSettings()
        {
            //open setting as a popup.
            UIController.instance.ShowScreen(ScreenType.Settings);
        }

        void Restart()
        {
            LevelManager.instance.ReloadLevel();
        }

    }
}