using UnityEngine;

namespace AirplaneCamera
{
    public class IP_Airplane_Camera : IP_Basic_Follow_Camera
    {
    #region Veriables

        [Header("Airplane Camera Properties")]
        public float minHeightFromGround = 2f;

    #endregion

        protected override void HandleCamera()
        {
            RaycastHit hit;

            base.HandleCamera();

            if (!Physics.Raycast(transform.position, Vector3.down, out hit))
                return;

            if (hit.distance < minHeightFromGround && hit.transform.tag == "Ground")
            {
                float wantedHeight = originalHeight + (minHeightFromGround - hit.distance);
            }
        }
    }
}