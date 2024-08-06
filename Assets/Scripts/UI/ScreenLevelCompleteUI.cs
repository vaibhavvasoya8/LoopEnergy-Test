using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIKit;
using UnityEngine.UI;
using GamePlay;
using System;

public class ScreenLevelCompleteUI : UIScreenBase
{
    [Header("Button Refrence")]
    [SerializeField] Button home;
    [SerializeField] Button nextLevel;

    [Header("Text Refrence")]
    [SerializeField] Text winDimondText;
    [SerializeField] Text totalDimondText;
    [SerializeField] Text complementMessageText;

    int winDimond = 0;
    void Start()
    {
        home.onClick.AddListener(ShowHomeScreen);
        nextLevel.onClick.AddListener(LoadNextLevel);
    }
    public override void Show(Action Callback = null)
    {
        base.Show(Callback);
        complementMessageText.text = LevelManager.instance.GetComplementMessage();
        AudioManager.instance.Play(AudioType.LevelComplete);
        winDimond = LevelManager.instance.GetWinDiamond();
        totalDimondText.text = GameManager.instance.currentDimond.ToString("00");
        winDimondText.text = winDimond.ToString("00");
        winDimondText.gameObject.SetActive(true);
    }
    public override void OnScreenShowAnimationCompleted()
    {
        base.OnScreenShowAnimationCompleted();
        StartCoroutine(LerpDimondText(winDimondText,winDimond,0,1f));
        StartCoroutine(LerpDimondText(totalDimondText, GameManager.instance.currentDimond, GameManager.instance.currentDimond + winDimond, 1f,()=>
        {
            GameManager.instance.currentDimond += winDimond;
            winDimondText.gameObject.SetActive(false);
        }));
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
   
    private IEnumerator LerpDimondText(Text text, int startValue, int endValue, float duration, Action completeCallback=null)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            int currentValue = Mathf.RoundToInt(Mathf.Lerp(startValue, endValue, t));
            text.text = currentValue.ToString("00");
            yield return null;
        }
        text.text = endValue.ToString("00"); // Ensure it ends at the target value
        completeCallback?.Invoke();
    }
}
