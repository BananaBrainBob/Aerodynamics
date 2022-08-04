using System;
using AirplaneInput;
using UnityEngine;

namespace ControlSurfaces
{
    public class IP_Airplane_Control_Surface : MonoBehaviour
    {
    #region Variables

        [Header("Control Surfaces Properties")]
        public ControlSurfaceType m_controlSurfaceType;

        public float maxAngle = 30f;
        public Vector3 axis = Vector3.right;
        public Transform controlSurfaceGraphic;
        public float smoothSpeed;
        private float wantedAngle;

    #endregion

    #region Builtin Methods

        private void Update()
        {
            if (!controlSurfaceGraphic)
                return;
            Vector3 finalAngleAxis = axis * wantedAngle;
            controlSurfaceGraphic.localRotation = Quaternion.Slerp(controlSurfaceGraphic.localRotation,
                Quaternion.Euler(finalAngleAxis), Time.deltaTime * smoothSpeed);
        }

    #endregion

    #region Custom Methods

        public void HandleControlSurface(IP_Base_Airplane_Input input)
        {
            var inputValue = m_controlSurfaceType switch
            {
                ControlSurfaceType.Rudder => input.Yaw,
                ControlSurfaceType.Elevator => input.Pitch,
                ControlSurfaceType.Flap => input.Flaps,
                ControlSurfaceType.Aileron => input.Roll,
                _ => throw new ArgumentOutOfRangeException()
            };

            wantedAngle = maxAngle * inputValue;
        }

    #endregion
    }
}