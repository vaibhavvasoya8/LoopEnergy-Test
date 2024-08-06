using DG.Tweening;
using UnityEngine;

namespace GamePlay
{
    public class BulbPiece : Piece
    {
        [HideInInspector]
        public bool isPowerOn;
        private ParticleSystem glowEffect;

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
            //Get the particle refrence.
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
        /// <summary>
        /// Play the Bulb Light effect and sound.
        /// </summary>
        void UpdateBulbPower()
        {
            if (!isGlowing)
            {
                isPowerOn = false;
            }
            else if (!isPowerOn)
            {
                isPowerOn = true;
                PlayGlow();
                AudioManager.instance.Play(AudioType.BulbOn);
            }
        }

        private void PlayGlow()
        {
            glowEffect.Play();
        }
    }
}