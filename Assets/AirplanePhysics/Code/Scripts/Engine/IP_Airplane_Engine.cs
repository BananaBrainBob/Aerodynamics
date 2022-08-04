using Propellers;
using UnityEngine;

namespace Engine
{
    public class IP_Airplane_Engine : MonoBehaviour
    {
    #region Variables

        [Header("Engine Properties")] public float maxForce = 200f;
        public float maxRPM = 2550f;
        public AnimationCurve powerCurve = AnimationCurve.Linear(0, 0, 1, 1);
        [Header("Propellers")] public IP_Airplane_Propeller propeller;

    #endregion

    #region Builtin Methods

    #endregion

    #region Custom Methods

        public Vector3 CalculateForce(float throttle)
        {
            //Calculate power
            var finalThrottle = Mathf.Clamp01(throttle);
            finalThrottle = powerCurve.Evaluate(finalThrottle);

            //Calculate RPM
            if (propeller)
            {
                float currentRPM = finalThrottle * maxRPM;
                propeller.HandlePropeller(currentRPM);
            }
            //Create Force
            var finalPower = finalThrottle * maxForce;
            var finalForce = transform.forward * finalPower;

            return finalForce;
        }

    #endregion
    }
}