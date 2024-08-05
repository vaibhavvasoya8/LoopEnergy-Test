using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay
{
    public class WifiPiece : Piece
    {
        private void OnEnable()
        {
            Events.OnWifiConectionUpdate += WifiConnectionUpdate;
        }
        private void OnDisable()
        {
            Events.OnWifiConectionUpdate -= WifiConnectionUpdate;
        }
        public override void ApplyRotation()
        {
            realRotation -= 90;

            if (realRotation == 360)
                realRotation = 0;
            content.DORotateQuaternion(Quaternion.Euler(0, 0, realRotation), rotationSpeed);
            RotateValues();
        }

        void WifiConnectionUpdate(Piece source)
        {
            if (source == this) return;
            isConnected = true;
            UpdateConnectedCellColor(source.GetComponent<Piece>());
        }




    }
}