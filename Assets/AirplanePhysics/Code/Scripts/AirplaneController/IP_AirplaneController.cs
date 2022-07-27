using AirplaneInput;
using UnityEngine;

namespace AirplaneController
{
    public class IP_AirplaneController : IP_BaseRigidbody_Controller
    {
    #region Variables

        [Header("Base Airplane Properties"), SerializeField]
        private IP_Base_Airplane_Input input;

        [SerializeField, Tooltip("Weight is in pounds")]
        private float airplaneWeight;

        [SerializeField,Tooltip("Center Of Gravity")] private Transform COG;

    #endregion

    #region Custom Methods

        protected override void HandlePhysics()
        {
        }

    #endregion
    }
}