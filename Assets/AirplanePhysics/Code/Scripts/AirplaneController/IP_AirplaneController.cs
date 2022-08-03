using System.Collections.Generic;
using AirplaneInput;
using Characteristics;
using Engine;
using UnityEngine;
using Wheels;

namespace AirplaneController
{
    [RequireComponent(typeof(IP_Airplane_Characteristics))]
    public class IP_AirplaneController : IP_BaseRigidbody_Controller
    {
    #region Variables

        [Header("Base Airplane Properties")]
        [SerializeField]private IP_Base_Airplane_Input input;

        [SerializeField]private IP_Airplane_Characteristics characteristics;

        [SerializeField, Tooltip("Weight is in pounds")]
        private float airplaneWeight = 800f;

        [Tooltip("Center Of Gravity")]public Transform COG;

        [Header("Engines")]
        [SerializeField]private List<IP_Airplane_Engine> engines;

        [Header("Wheels")]
        [SerializeField]private List<IP_Airplane_Wheel> wheels;

        private bool bEnginesExist => engines is not { Count: > 0 };

    #endregion

    #region Builtin Methods

        protected override void Start()
        {
            base.Start();

            if (rb)
            {
                float finalWeight = airplaneWeight.PoundsToKilograms();
                rb.mass = finalWeight;

                if (COG)
                {
                    rb.centerOfMass = COG.localPosition;
                }

                if (characteristics)
                {
                    characteristics.InitCharacteristics(rb, input);
                }
            }


            if (wheels is { Count: > 0 })
            {
                foreach (var wheel in wheels)
                    wheel.InitWheel();
            }
        }

    #endregion

    #region Custom Methods

        protected override void HandlePhysics()
        {
            if (!input)
                return;
            HandleEngine();
            HandleCharacteristic();
            HandleSteering();
            HandleBrakes();
            HandleAltitude();
        }

        private void HandleCharacteristic()
        {
            if (characteristics)
                characteristics.UpdateCharacteristics();
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


        private void HandleEngine()
        {
            if (bEnginesExist)
                return;

            foreach (var engine in engines)
                rb.AddForce(engine.CalculateForce(input.StickyThrottle));
        }

    #endregion
    }
}