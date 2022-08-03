using System;
using UnityEngine;

namespace AirplaneCamera
{
    public class IP_Basic_Follow_Camera : MonoBehaviour
    {
    #region Variables

        [Header("Basic Follow Camera properties")]
        public Transform target;

        public float distance = 5f;
        public float height = 2f;
        public float smoothSpeed = .5f;

        private Vector3 SmoothVelocity;
        protected float originalHeight;

    #endregion

    #region Builtin Methods

        private void Start()
        {
            originalHeight = height;
        }

        private void FixedUpdate()
        {
            if (target)
                HandleCamera();
        }

    #endregion

    #region Custom Methods

        protected virtual void HandleCamera()
        {
            var wantedPosition = target.position + (-target.forward * distance) + (Vector3.up * height);
            transform.position = Vector3.SmoothDamp(transform.position,wantedPosition,ref SmoothVelocity,smoothSpeed);
            
            transform.LookAt(target);
        }

    #endregion
    }
}