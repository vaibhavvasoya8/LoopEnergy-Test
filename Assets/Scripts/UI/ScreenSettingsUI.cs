using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UIKit
{
    public class ScreenSettingsUI : UIScreenBase
    {
        [SerializeField] Button backButton;
        void Start()
        {
            backButton.onClick.AddListener(CloseSettingsPopup);
        }

        void CloseSettingsPopup()
        {
            UIController.instance.HideScreen(ScreenType.Settings,null);
        }
    }
}