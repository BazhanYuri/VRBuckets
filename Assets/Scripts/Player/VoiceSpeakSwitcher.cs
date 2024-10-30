using Normal.Realtime;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class VoiceSpeakSwitcher : MonoBehaviour
    {
        public InputActionReference voiceSwitchAction;
        public RealtimeAvatarVoice voice;
        
        
        private void OnEnable()
        {
            voiceSwitchAction.action.Enable();
            voiceSwitchAction.action.performed += OnSwitch;
        }
        
        private void OnDisable()
        {
            voiceSwitchAction.action.performed -= OnSwitch;
        }

        private void Start()
        {
            voice.mute = true;
        }

        private void OnSwitch(InputAction.CallbackContext context)
        {
            voice.mute = !voice.mute;
        }
    }
}