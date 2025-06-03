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
        [SerializeField] private float interval;
        [SerializeField] private float hideTime;
        
        [Header("Countdown Settings")]
        [SerializeField] private float countdownStartTime;
        [SerializeField] private TextMeshProUGUI countdownTitle;
        [SerializeField] private TextMeshProUGUI countdownText;

        [Header("Blink Settings")]
        [SerializeField] private int numberOfBlinks;
        [SerializeField] private float blinkTime;
        
        [Header("References")]
        [SerializeField] private GameObject floor;
        
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
            _timer = interval;
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

            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                StartCoroutine(ShowAndHideGround());
                _timer = interval;
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

            // if (!_active) return;
            //
            // _timer -= Time.deltaTime;
            // if (_timer <= 0)
            // {
            //     StartCoroutine(ShowAndHideGround());
            //     _timer = interval;
            // } 
            // else if (_timer <= blinkTime * 2 * numberOfBlinks)
            // {
            //     StartCoroutine(BlinkGround());
            // }
            //
            //
            //  if (!_active || _isBlinking) return;
            //
            // _timer -= Time.deltaTime;
            //  if (_timer <= blinkTime * 2 * numberOfBlinks && !_isBlinking)
            // {
            //     StartCoroutine(BlinkAndFade());
            //     _timer = interval;
            // }

        }

        private IEnumerator BlinkAndFade()
        {
            _isBlinking = true;
            
            for (var i = 0; i < numberOfBlinks; i++)
            {
                foreach (Transform child in floor.transform)
                {
                    var fader = child.GetComponent<MeshFader>();
                    if (fader) fader.FadeOut(blinkTime);
                }
                
                yield return new WaitForSeconds(blinkTime);
                
                foreach (Transform child in floor.transform)
                {
                    var fader = child.GetComponent<MeshFader>();
                    if (fader) fader.FadeIn(blinkTime);
                }
                
                yield return new WaitForSeconds(blinkTime);
            }
            
            foreach (Transform child in floor.transform)
            {
                var fader = child.GetComponent<MeshFader>();
                if (fader) fader.FadeOut(blinkTime);
            }

            yield return new WaitForSeconds(blinkTime);
            floor.SetActive(false);

            yield return new WaitForSeconds(hideTime);
            floor.SetActive(true);

            foreach (Transform child in floor.transform)
            {
                var fader = child.GetComponent<MeshFader>();
                if (fader) fader.FadeIn(blinkTime);
            }

            _isBlinking = false;
        }
        
        private IEnumerator BlinkGround()
        {
            for (var i = 0; i < numberOfBlinks; i++)
            {
                foreach (Transform child in floor.transform)
                    child.GetComponent<MeshRenderer>().enabled = false;
                
                yield return new WaitForSeconds(blinkTime);
                
                foreach (Transform child in floor.transform)
                    child.GetComponent<MeshRenderer>().enabled = true;
                
                yield return new WaitForSeconds(blinkTime);
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
