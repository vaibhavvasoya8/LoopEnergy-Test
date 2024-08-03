using DG.Tweening;
using UnityEngine;

namespace GamePlay
{
    public class BulbPiece : Piece
    {
        public bool isPowerOn;
        ParticleSystem glowEffect;

        public void OnEnable()
        {
            Events.OnUpdateBulbPower += UpdateBulbPower;
        }
        public void OnDisable()
        {
            Events.OnUpdateBulbPower -= UpdateBulbPower;
        }
        public override void OnStart()
        {
            base.OnStart();
            glowEffect = GetComponentInChildren<ParticleSystem>();
        }

        public override void ApplyRotation()
        {
            realRotation -= 90;

            if (realRotation == 360)
                realRotation = 0;
            content.DORotateQuaternion(Quaternion.Euler(0, 0, realRotation), rotationSpeed);
            RotateValues();
        }

        void UpdateBulbPower()
        {
            if (!isGlowing)
            {
                isPowerOn = false;
            }
            else if (!isPowerOn)
            {
                isPowerOn = true;
                glowEffect.Play();
            }
        }
    }
}