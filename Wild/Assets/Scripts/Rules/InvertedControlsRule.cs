using Controllers;
using UnityEngine;

namespace Rules
{
    public class InvertedControlsRule : MonoBehaviour, IGameRule
    {
        public string RuleName => "Inverted Controls";
        private PlayerController _player;
        [SerializeField] private bool _active;
        
        private void Awake() {
            _player = FindObjectOfType<PlayerController>();
        }

        public void Activate()
        {
            _active = true;
            _player.EnableControls("PlayerInverted");
        }

        public void Deactivate()
        {
            _active = false;
            _player.EnableControls("Player");
        }
    }
}
