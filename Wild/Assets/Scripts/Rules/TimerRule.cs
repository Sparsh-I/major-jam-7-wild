using TMPro;
using UnityEngine;

namespace Rules
{
    public class TimerRule : MonoBehaviour, IGameRule
    {
        public string RuleName => "Timer";
        
        [Header("Timer Settings")]
        [SerializeField] private int max30SecondInterval;
        [SerializeField] private TextMeshProUGUI timerText;
        
        private float _timeLimit;
        private float _timer;
        private bool _active;

        private void Start()
        {
            timerText.enabled = false;
            _timeLimit = Random.Range(1, max30SecondInterval + 1) * 30;
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
                Debug.Log("Time's up!");
                return;
            }
            
            timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }
}
