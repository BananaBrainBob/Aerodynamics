using System.Collections.Generic;
using AirplaneInput;
using Engine;
using UnityEngine;
using Wheels;

namespace AirplaneController
{
    public class IP_AirplaneController : IP_BaseRigidbody_Controller
    {
    #region Variables

        [Header("Base Airplane Properties"), SerializeField]
        private IP_Base_Airplane_Input input;

        [SerializeField, Tooltip("Weight is in pounds")]
        private float airplaneWeight = 800f;

        [Tooltip("Center Of Gravity")] public Transform COG;

        [Header("Engines"), SerializeField] private List<IP_Airplane_Engine> engines;
        [Header("Wheels"), SerializeField] private List<IP_Airplane_Wheel> wheels;

        private bool bEnginesExist => engines is not { Count: > 0 };

    #endregion

    #region Builtin Methods

        protected override void Start()
        {
            base.Start();

            float finalWeight = airplaneWeight.PoundsToKilograms();
            if (rb)
                rb.mass = finalWeight;
            if (COG)
                rb.centerOfMass = COG.localPosition;
            if (wheels != null || wheels.Count > 0)
            {
            }
        }

    #endregion

    #region Custom Methods

        protected override void HandlePhysics()
        {
            if (!input)
                return;
            HandleEngine();
            HandleAerodynamics();
            HandleSteering();
            HandleBrakes();
            HandleAltitude();
        }

        private void HandleAltitude()
        {
        }

        private void HandleBrakes()
        {
        }

        private void HandleSteering()
        {
        }

        private void HandleAerodynamics()
        {
        }

        private void HandleEngine()
        {
            if (bEnginesExist)
                return;

            foreach (var engine in engines)
                rb.AddForce(engine.CalculateForce(input.Throttle));
            
        }

    #endregion
    }
}