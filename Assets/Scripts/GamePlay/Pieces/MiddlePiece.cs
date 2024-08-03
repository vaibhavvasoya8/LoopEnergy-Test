using DG.Tweening;
using UnityEngine;

namespace GamePlay
{
    public class MiddlePiece : Piece
    {
        public override void ApplyRotation()
        {
            realRotation -= 90;

            if (realRotation == 360)
                realRotation = 0;
            content.DORotateQuaternion(Quaternion.Euler(0, 0, realRotation), rotationSpeed);
            RotateValues();
        }
    }
}