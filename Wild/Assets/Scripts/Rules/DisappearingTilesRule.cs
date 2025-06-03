using System.Collections;
using System.Security;
using TMPro;
using UnityEngine;

namespace Rules
{
    public class DisappearingTilesRule : MonoBehaviour, IGameRule
    {
        public string RuleName => "Disappearing Ground";
        
        [Header("Time Settings")]
        [SerializeField] private float maxIntervalLength;
        [SerializeField] private float hideTime;
        
        [Header("Countdown Settings")]
        [SerializeField] private float countdownStartTime;
        [SerializeField] private TextMeshProUGUI countdownTitle;
        [SerializeField] private TextMeshProUGUI countdownText;
        
        [Header("References")]
        [SerializeField] private GameObject floor;

        private float _interval;
        private float _timer;
        private bool _active;
        private bool _isBlinking;

        private void Start()
        {
            _active = false;
            countdownTitle.gameObject.SetActive(false);
        }
        
        public void Activate()
        {
            _interval = Random.Range(countdownStartTime, maxIntervalLength);
            _timer = _interval;
            _active = true;
        }
        
        public void Deactivate()
        {
            _active = false;
            countdownTitle.gameObject.SetActive(false);
        }
        
        private void Update()
        {
            if (!_active) return;
            
            _interval = Random.Range(countdownStartTime, maxIntervalLength);

            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                StartCoroutine(ShowAndHideGround());
                _timer = _interval;
            } 
            else if (_timer <= countdownStartTime)
            {
                countdownTitle.gameObject.SetActive(true);
                countdownText.text = $"{(int) _timer}";
            }
            else
            {
                countdownTitle.gameObject.SetActive(false);
            }
        }
        private IEnumerator ShowAndHideGround()
        {
            floor.gameObject.SetActive(false);
            yield return new WaitForSeconds(hideTime);
            floor.gameObject.SetActive(true);
        }
    }
}
