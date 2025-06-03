using Controllers;
using UnityEngine;

namespace Rules
{
    public class SwapHandsRule : MonoBehaviour, IGameRule
    {
        public string RuleName => "Swap Hands";
        [SerializeField] private bool _active;

        public bool IsSwapHandsActive() => _active;
        private PlayerController _player;
        
        
        private void Awake() {
            _player = FindObjectOfType<PlayerController>();
        }

        public void Activate()
        {
            _active = true;
            _player.UpdateControlScheme(_player.isInverted, true);
        }

        public void Deactivate()
        {
            _active = false;
            _player.EnableControls("Player");
        }
    }
}
