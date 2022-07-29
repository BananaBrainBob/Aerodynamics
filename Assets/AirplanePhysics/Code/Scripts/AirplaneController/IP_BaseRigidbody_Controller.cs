using System;
using UnityEngine;

namespace AirplaneController
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(AudioSource))]
    public class IP_BaseRigidbody_Controller : MonoBehaviour
    {
    #region Variables

        protected Rigidbody rb;

        protected AudioSource aSource;

    #endregion

    #region Builtin Methods

        protected virtual void Start()
        {
            rb = GetComponent<Rigidbody>();
            aSource = GetComponent<AudioSource>();
            if (aSource)
                aSource.playOnAwake = false;
        }

        protected void FixedUpdate()
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