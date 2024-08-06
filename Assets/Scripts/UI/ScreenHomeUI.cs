using UnityEngine;
using UnityEngine.UI;
using System;

namespace UIKit
{
    public class ScreenHomeUI : UIScreenBase
    {
        [SerializeField] Button tapToStart;
        [SerializeField] Button settings;
        [SerializeField] UILogoEyesBlinkAnimation eyeOne;
        [SerializeField] UILogoEyesBlinkAnimation eyeTwo;
        [SerializeField] TapToStartAnimation tapToStartAnimation;
        [SerializeField] Text totalDimondText;
        private void Start()
        {
            tapToStart.onClick.AddListener(StartGame);
            settings.onClick.AddListener(OpenSettings);
        }

        public override void Show(Action Callback = null)
        {
            base.Show(Callback);
            eyeOne.enabled = true;
            eyeTwo.enabled = true;
            tapToStartAnimation.enabled = true;
            totalDimondText.text = GameManager.instance.currentDimond.ToString("00");
        }
        public override void OnScreenShowAnimationStarted()
        {
            base.OnScreenShowAnimationStarted();
            GameManager.instance.cameraController.UnfocusCamera();
        }
        public override void OnScreenHideAnimationCompleted()
        {
            base.OnScreenHideAnimationCompleted();
            eyeOne.enabled = false;
            eyeTwo.enabled = false;
            tapToStartAnimation.enabled = false;
        }
        void StartGame()
        {
            //Show Gameplay screen
            UIController.instance.ShowNextScreen(ScreenType.Gameplay);
        }
        void OpenSettings()
        {
            //open setting as a popup.
            UIController.instance.ShowScreen(ScreenType.Settings);
        }
    }
}
