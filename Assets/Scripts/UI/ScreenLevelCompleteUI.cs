using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIKit;
using UnityEngine.UI;
using GamePlay;
using System;

public class ScreenLevelCompleteUI : UIScreenBase
{
    [SerializeField] Button home;
    [SerializeField] Button nextLevel;

    void Start()
    {
        home.onClick.AddListener(ShowHomeScreen);
        nextLevel.onClick.AddListener(LoadNextLevel);
    }
    public override void Show(Action Callback = null)
    {
        base.Show(Callback);
        AudioManager.instance.Play(AudioType.LevelComplete);
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
