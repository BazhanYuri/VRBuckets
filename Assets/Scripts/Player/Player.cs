using Normal.Realtime;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Player
{
    public class Player : MonoBehaviour
    {
        public Camera vrCamera;
        public RealtimeView RealtimeView;
        public XROrigin XROrigin;
        public BallsSpawner ballsSpawner;
        public ActionBasedController leftController;
        public ActionBasedController rightController;
        

        private void Start()
        {
            if (RealtimeView.isOwnedLocally)
            {
                vrCamera.enabled = true;
                XROrigin.enabled = true;
                ballsSpawner.enabled = true;
            }
            else
            {
                vrCamera.enabled = false;
                Destroy(XROrigin);
                Destroy(leftController);
                Destroy(rightController);
                Destroy(ballsSpawner);
            }
        }
    }
}