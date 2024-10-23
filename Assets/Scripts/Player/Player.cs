using System;
using Normal.Realtime;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.XR.Interaction.Toolkit;

namespace Player
{
    public class Player : MonoBehaviour
    {
        public Camera vrCamera;
        public RealtimeView RealtimeView;
        public XROrigin XROrigin;
        public ActionBasedController leftController;
        public ActionBasedController rightController;

        private void Start()
        {
            if (RealtimeView.isOwnedLocally)
            {
                vrCamera.enabled = true;
                XROrigin.enabled = true;
            }
            else
            {
                vrCamera.enabled = false;
                Destroy(XROrigin);
                Destroy(leftController);
                Destroy(rightController);
            }
        }
    }
}