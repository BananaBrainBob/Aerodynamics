using UnityEngine;

namespace Engine
{
    public class IP_Airplane_Engine : MonoBehaviour
    {
    #region Variables

        public float maxForce = 200f;
        public float maxRPM = 2550f;
        public AnimationCurve powerCurve = AnimationCurve.Linear(0,0,1,1);
    #endregion

    #region Builtin Methods

    #endregion

    #region Custom Methods

        public Vector3 CalculateForceMode(float throttle)
        {
            var finalThrottle = Mathf.Clamp01(throttle);
            finalThrottle = powerCurve.Evaluate(finalThrottle);
            var finalPower = finalThrottle * maxForce;
            var finalForce = transform.forward * finalPower;

            return finalForce;
        }

    #endregion
    }
}