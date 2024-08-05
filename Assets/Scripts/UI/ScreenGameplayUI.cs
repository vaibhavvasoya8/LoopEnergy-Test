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
        
        [Header("Next & Previous level button")]
        [SerializeField] Button previousLevel;
        [SerializeField] Button nextLevel;
        void Start()
        {
            settings.onClick.AddListener(OpenSettings);
            restart.onClick.AddListener(Restart);
            previousLevel.onClick.AddListener(LoadPreviousLevel);
            nextLevel.onClick.AddListener(LoadNextLevel);
        }

        public override void Show(Action Callback = null)
        {
            base.Show(Callback);
            ScreenRefrash();
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

        void LoadPreviousLevel()
        {
            if(LevelManager.instance.currentLevelIndex - 1 >= 0)
            {
                LevelManager.instance.LoadPreviousLevel();
            }
            ScreenRefrash();
        }
        void LoadNextLevel()
        {
            if (LevelManager.instance.currentLevelIndex + 1 <= SavedDataHandler.instance._saveData.levelCompleted)
            {
                LevelManager.instance.LoadNextLevel();
            }
            ScreenRefrash();
        } 

        void ScreenRefrash()
        {
            levelText.text = "#" + (LevelManager.instance.currentLevelIndex + 1);
            GameManager.instance.cameraController.FocusCamera();

            previousLevel.interactable = (LevelManager.instance.currentLevelIndex > 0);
            nextLevel.interactable = (LevelManager.instance.currentLevelIndex < SavedDataHandler.instance._saveData.levelCompleted-1);

        }
    }
}