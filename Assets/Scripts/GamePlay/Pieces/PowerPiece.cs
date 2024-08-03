using System.Collections;
using UnityEngine;

namespace GamePlay
{
    public class PowerPiece : Piece
    {
        public float duration = 0.3f; // Duration of the shake
        public float strength = 25f; // Strength of the shake
        public override void OnStart()
        {
            base.OnStart();
            isConnected = true;
        }

        public override void ApplyRotation()
        {
            StartCoroutine(ShakeOnZAxis());
        }

        IEnumerator ShakeOnZAxis()
        {
            Vector3 originalRotation = content.localEulerAngles;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float zShake = Random.Range(-strength, strength);
                content.localEulerAngles = originalRotation + new Vector3(0, 0, zShake);
                yield return null;
            }

            content.localEulerAngles = originalRotation;
        }
    }
}