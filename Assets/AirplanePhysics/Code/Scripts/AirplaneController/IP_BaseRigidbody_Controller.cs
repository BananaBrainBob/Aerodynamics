using System;
using UnityEngine;

namespace AirplaneController
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(AudioSource))]
    public class IP_BaseRigidbody_Controller : MonoBehaviour
    {
    #region Variables

        private Rigidbody rb;

        private AudioSource aSource;

    #endregion

    #region Builtin Methods

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            aSource = GetComponent<AudioSource>();
            if (aSource)
                aSource.playOnAwake = false;
        }

        private void FixedUpdate()
        {
            if (!rb)
                return;

            HandlePhysics();
        }

    #endregion

    #region Custom Methods

        protected virtual void HandlePhysics()
        {
            
        }

    #endregion
    }
}