using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIKit;
using UnityEngine.UI;
using GamePlay;
public class ScreenLevelCompleteUI : UIScreenBase
{
    [SerializeField] Button home;
    [SerializeField] Button nextLevel;

    void Start()
    {
        home.onClick.AddListener(ShowHomeScreen);
        nextLevel.onClick.AddListener(LoadNextLevel);
    }

    void ShowHomeScreen()
    {
        //open home screen.
        UIController.instance.ShowNextScreen(ScreenType.MainMenu);
        LevelManager.instance.LoadNextLevel();
    }

    void LoadNextLevel()
    {
        //Load next Level
        UIController.instance.ShowNextScreen(ScreenType.Gameplay);
        LevelManager.instance.LoadNextLevel();
    }

}
