using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rules
{
    public class TimerRule : MonoBehaviour, IGameRule
    {
        public string RuleName => "Timer";

        [SerializeField] private string deathReason;
        
        [Header("Timer Settings")]
        [SerializeField] private int min30SecInterval;
        [SerializeField] private int max30SecInterval;
        [SerializeField] private TextMeshProUGUI timerText;
        
        private float _timeLimit;
        private float _timer;
        private bool _active;

        private void Start()
        {
            timerText.enabled = false;
            _timeLimit = Random.Range(min30SecInterval, max30SecInterval + 1) * 30;
        }
        
        public void Activate()
        {
            timerText.enabled = true;
            _timer = _timeLimit;
            _active = true;
        }

        public void Deactivate()
        {
            timerText.enabled = false;
            _active = false;
        }
        
        private void Update()
        {
            if (!_active) return;
            
            _timer -= Time.deltaTime;
            int minutes = (int) (_timer / 60);
            int seconds = (int) (_timer % 60);
            
            if (_timer <= 0)
            {
                GameManager.Instance.DisplayGameOverScreen(deathReason);
                return;
            }
            
            timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }
}
