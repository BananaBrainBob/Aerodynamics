using UnityEngine;
using UnityEngine.InputSystem;

namespace AirplaneInput
{
    public class IP_Base_Airplane_Input : MonoBehaviour
    {
    #region variables

        protected float pitch = 0f;
        protected float roll = 0f;
        protected float yaw = 0f;
        protected float throttle = 0f;
        public float throttleSpeed = 0.1f;

        protected int flaps;
        protected float brake = 0f;

        protected float stickyThrottle;
        public float StickyThrottle => stickyThrottle;
        [SerializeField] private InputActionMap PlaneControls;

    #endregion

    #region Properties

        public float Pitch => pitch;
        public float Roll => roll;
        public float Yaw => yaw;
        public float Throttle => throttle;
        public int Flaps => flaps;
        public float Brake => brake;

    #endregion

    #region Builtin Methods

        // Start is called before the first frame update
        void Start()
        {
            PlaneControls.Enable();
        }

        // Update is called once per frame
        void Update()
        {
            HandleInput();
            StickyThrottleControl();
        }

    #endregion

    #region Custom Methods

        void HandleInput()
        {
            //Process main controls
            pitch = PlaneControls.FindAction("Pitch").ReadValue<float>();
            roll = PlaneControls.FindAction("Roll").ReadValue<float>();
            yaw = PlaneControls.FindAction("Yaw").ReadValue<float>();
            throttle = PlaneControls.FindAction("Throttle").ReadValue<float>();

            //Process brake controls
            brake = PlaneControls.FindAction("Brake").IsPressed() ? 1 : 0;

            //Process flaps
            if (PlaneControls.FindAction("Raise Flaps").WasPressedThisFrame())
                flaps += 1;
            if (PlaneControls.FindAction("Lower Flaps").WasPressedThisFrame())
                flaps -= 1;
            flaps = Mathf.Clamp(flaps, 0, 3);
        }

        private void StickyThrottleControl()
        {
            stickyThrottle += (throttle * throttleSpeed * Time.deltaTime);
            stickyThrottle = Mathf.Clamp01(stickyThrottle);
        }

    #endregion
    }
}