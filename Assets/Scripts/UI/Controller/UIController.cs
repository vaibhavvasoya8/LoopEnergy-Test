using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UIKit
{
    [Serializable]
    public class UIScreen
    {
        public ScreenType screenType;
        public UIScreenBase screenView;
    }

    public enum ScreenType
    {
        None,
        MainMenu,
        Settings,
        Gameplay,
        LevelComplete
    }
    public class UIController : Singleton<UIController>
    {

        public ScreenType StartScreen;
        public List<UIScreen> Screens;

        [SerializeField]
        List<ScreenType> currentScreens;

        [SerializeField] EventSystem eventSystem;


        private IEnumerator Start()
        {
            currentScreens = new List<ScreenType>();

            yield return null;
            ShowScreen(StartScreen);

            yield return new WaitForSeconds(5f);
        }

        public void ShowNextScreen(ScreenType screenType)
        {
            if (!currentScreens.Contains(screenType))
            {
                if (currentScreens.Count > 0)
                {
                    HideScreen(currentScreens.Last(), () =>
                    {
                        ShowScreen(screenType);
                    });
                }
                else
                {
                    ShowScreen(screenType);
                }
            }
        }

        public void ShowScreen(ScreenType screenType)
        {
            currentScreens.Add(screenType);
            getScreen(screenType).Show();

        }

        public void HideScreen(ScreenType screenType, Action Callback)
        {
            getScreen(screenType).Hide(() =>
            {
                currentScreens.Remove(screenType);
                Callback();
            });

        }

        public UIScreenBase getScreen(ScreenType screenType)
        {
            return Screens.Find(screen => screen.screenType == screenType).screenView;
        }
        public bool IsTapOnUI()
        {
            return EventSystem.current.IsPointerOverGameObject();
        }

    }

}