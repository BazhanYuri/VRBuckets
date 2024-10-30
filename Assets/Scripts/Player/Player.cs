using Enums;
using Multiplayer.Services;
using Normal.Realtime;
using UI.Player;
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
        public TeamChangePopUp teamChangePopUp;
        public PCMovement pcMovement;
        public AreaThrowChecker areaThrowChecker;
        public AudioListener audioListener;
        public VoiceSpeakSwitcher voiceSpeakSwitcher;
        public PlayerJumper playerJumper;


        public Team team;

        public int Index
        {
            get => RealtimeView.ownerID;
        }

        private void Start()
        {
            if (RealtimeView.isOwnedLocally)
            {
                vrCamera.enabled = true;
                XROrigin.enabled = true;
                ballsSpawner.enabled = true;
                
                ScoreBoard scoreBoard = FindObjectOfType<ScoreBoard>();
                
                RealtimeTransform realtimeTransform = scoreBoard.GetComponent<RealtimeTransform>();
                realtimeTransform.RequestOwnership();
            }
            else
            {
                vrCamera.enabled = false;
                Destroy(XROrigin);
                Destroy(leftController);
                Destroy(rightController);
                Destroy(ballsSpawner);
                Destroy(pcMovement);
                Destroy(audioListener);
                Destroy(voiceSpeakSwitcher);
                Destroy(playerJumper);
            }
        }
        public void AssignTeam(Team second)
        {
            team = second;
            teamChangePopUp.SetText(team);
        }
    }
}