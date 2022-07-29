using System;
using UnityEngine;

namespace Propellers
{
    public class IP_Airplane_Propeller : MonoBehaviour
    {
    #region Variables

        [Header("Propeller Properties")] public float minQuadRPMs = 300f;
        public float minTextureSwap = 600f;
        public GameObject mainProp;
        public GameObject blurredProp;

        [Header("Material Properties")] public Material blurredPropMat;
        public Texture2D blurLvl1;
        public Texture2D blurLvl2;

    #endregion

    #region Builtin Methods

        private void Start()
        {
            if (mainProp && blurredProp)
                HandleSwapping(0f);
        }

    #endregion

    #region Custom Methods

        public void HandlePropeller(float currentRPM)
        {
            //Get degrees per second
            var dps = (currentRPM * 360f) / 60f * Time.deltaTime;

            //Rotate Propeller
            transform.Rotate(Vector3.forward, dps);

            if (mainProp && blurredProp)
                HandleSwapping(currentRPM);
        }

        private void HandleSwapping(float CurrentRPM)
        {
            var blurred = CurrentRPM > minQuadRPMs;

            blurredProp.SetActive(blurred);
            mainProp.SetActive(!blurred);

            if (blurred && blurredPropMat && blurLvl1 && blurLvl2)
            {
                blurredPropMat.SetTexture("_BaseMap", CurrentRPM > minTextureSwap
                    ? blurLvl2
                    : blurLvl1);
            }
        }

    #endregion
    }
}