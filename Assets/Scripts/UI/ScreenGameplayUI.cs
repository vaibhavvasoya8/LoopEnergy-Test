using System;
using UnityEngine;
using UnityEngine.UI;
using GamePlay;

namespace UIKit
{
    public class ScreenGameplayUI : UIScreenBase
    {
        [Header("Level Text Refrence")]
        [SerializeField] Text levelText;

        [Header("Button Refrence")]
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

        /// <summary>
        /// Show setting screen as a popup.
        /// </summary>
        void OpenSettings()
        {
            UIController.instance.ShowScreen(ScreenType.Settings);
        }

        /// <summary>
        /// Reload level.
        /// </summary>
        void Restart()
        {
            LevelManager.instance.ReloadLevel();
        }
        /// <summary>
        /// Load previous level and screen refrash.
        /// </summary>
        void LoadPreviousLevel()
        {
            if(LevelManager.instance.currentLevelIndex - 1 >= 0)
            {
                LevelManager.instance.LoadPreviousLevel();
            }
            ScreenRefrash();
        }

        /// <summary>
        /// Load next level when next level is unlocked and screen refrash.
        /// </summary>
        void LoadNextLevel()
        {
            if (LevelManager.instance.currentLevelIndex < SavedDataHandler.instance._saveData.levelCompleted)
            {
                LevelManager.instance.LoadNextLevel();
            }
            ScreenRefrash();
        }

        /// <summary>
        /// Update level text, next and priveous level buttons.
        /// </summary>
        void ScreenRefrash()
        {
            levelText.text = "#" + (LevelManager.instance.currentLevelIndex + 1);
            GameManager.instance.cameraController.FocusCamera();

            previousLevel.interactable = (LevelManager.instance.currentLevelIndex > 0);
            nextLevel.interactable = (LevelManager.instance.currentLevelIndex < SavedDataHandler.instance._saveData.levelCompleted);

        }
    }
}