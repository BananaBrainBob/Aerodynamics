using AirplaneInput;
using Unity.Mathematics;
using UnityEngine;

namespace Characteristics
{
    public class IP_Airplane_Characteristics : MonoBehaviour
    {
    #region Variables

        [Header("Characteristics Properties")] public float maxMPH = 110f;
        public float rbLerpSpeed = 0.01f;


        [Header("Lift Properties")] public float maxLiftPower = 800;
        public AnimationCurve liftCurve;

        [Header("Drag Properties")] public float dragFactor = .01f;

        [Header("Control Properties")] public float pitchSpeed = 1000f;

        public float rollSpeed = 1000f;
        public float yawSpeed = 1000f;

        private float forwardSpeed;
        public float ForwardSpeed => forwardSpeed;
        private float mph;
        public float MPH => mph;


        private IP_Base_Airplane_Input input;

        private Rigidbody rb;

        private float startDrag;
        private float startAngularDrag;
        private float maxMPS; //MetersPerSecond
        private float normalizedMPH;
        private float angleOfAttack;
        private float pitchAngle;
        private float rollAngle;

    #endregion

    #region Builtin Methods

    #endregion

    #region Custom Methods

        public void InitCharacteristics(Rigidbody CurrentRigidbody, IP_Base_Airplane_Input currentInput)
        {
            //Basic initialization  
            input = currentInput;
            rb = CurrentRigidbody;
            startDrag = rb.drag;
            startAngularDrag = rb.angularDrag;

            //find the max meters per second
            maxMPS = maxMPH.MilesPerHourToMitersPerSecond();
        }

        public void UpdateCharacteristics()
        {
            if (!rb)
                return;

            CalculateForwardSpeed();
            CalculateLift();
            CalculateDrag();

            HandlePitch();
            HandleRoll();
            HandleYaw();
            HandleBanking();

            HandleRigidbodyTransform();
        }


        void CalculateForwardSpeed()
        {
            Vector3 localVelocity = transform.InverseTransformDirection(rb.velocity);
            forwardSpeed = Mathf.Max(0, localVelocity.z);
            forwardSpeed = Mathf.Clamp(forwardSpeed, 0, maxMPS);


            mph = forwardSpeed.MetersPerSecondToMilesPeHour();
            mph = Mathf.Clamp(mph, 0, maxMPH);
            normalizedMPH = Mathf.InverseLerp(0, maxMPH, mph);
        }

        void CalculateLift()
        {
            angleOfAttack = Vector3.Dot(rb.velocity.normalized, transform.forward);
            angleOfAttack *= angleOfAttack;

            Vector3 liftDir = Vector3.up;
            float liftPower = liftCurve.Evaluate(forwardSpeed) * maxLiftPower;

            Vector3 finalLiftForce = liftDir * (liftPower * angleOfAttack);
            rb.AddForce(finalLiftForce);
        }

        void CalculateDrag()
        {
            float speedDrag = forwardSpeed * dragFactor;
            float finaDrag = startDrag + speedDrag;

            rb.drag = finaDrag;
            rb.angularDrag = startAngularDrag * forwardSpeed;
        }


        private void HandlePitch()
        {
            Vector3 flatForward = transform.forward;
            flatForward.y = 0f;
            flatForward = flatForward.normalized;

            pitchAngle = Vector3.Angle(transform.forward, flatForward);
            Vector3 pitchTorque = input.Pitch * pitchSpeed * transform.right;

            rb.AddTorque(pitchTorque);
        }

        private void HandleRoll()
        {
            Vector3 flatRight = transform.right;
            flatRight.y = 0f;
            flatRight = flatRight.normalized;

            rollAngle = Vector3.Angle(transform.right, flatRight);
            Vector3 rollTorque = input.Roll * rollSpeed * transform.forward;

            rb.AddTorque(rollTorque);
        }

        private void HandleYaw()
        {
            Vector3 yawTorque = input.Yaw * yawSpeed * transform.up;
            rb.AddTorque(yawTorque);
        }

        private void HandleBanking()
        {
            float bankSide = Mathf.InverseLerp(-90, 90, rollAngle);
            float bankAmount = Mathf.Lerp(-1, 1, bankSide);
            Vector3 bankTorque = bankAmount * rollSpeed * transform.up;
            rb.AddTorque(bankTorque);
        }

        private void HandleRigidbodyTransform()
        {
            if (rb.velocity.magnitude > 1f)
            {
                Vector3 updatedVelocity = Vector3.Lerp(rb.velocity, transform.forward * forwardSpeed,
                    forwardSpeed * angleOfAttack * Time.deltaTime * rbLerpSpeed);
                rb.velocity = updatedVelocity;

                Quaternion updatedRotation =
                    Quaternion.Slerp(rb.rotation, Quaternion.LookRotation(rb.velocity.normalized, transform.up),
                        Time.deltaTime * rbLerpSpeed);

                rb.MoveRotation(updatedRotation);
            }
        }

    #endregion
    }
}