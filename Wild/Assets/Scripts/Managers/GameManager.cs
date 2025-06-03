using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public static GameManager Instance => _instance;

        [SerializeField] private TextMeshProUGUI gameText;
        [SerializeField] private float typingFrequency;

        public Coroutine DisplayTextCoroutine;
        
        private string _currentText;
        
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            gameText.enabled = false;
        }

        public void SetGameText(string text)
        {
            gameText.enabled = true;

            if (DisplayTextCoroutine != null)
            {
                StopCoroutine(DisplayTextCoroutine);
                gameText.text = _currentText;
                DisplayTextCoroutine = null;
                _currentText = text;
            }
            else
            {
                _currentText = text;
                DisplayTextCoroutine = StartCoroutine(DisplayText(_currentText));
            }

        }

        private IEnumerator DisplayText(string text)
        {
            gameText.text = "";
            foreach (var letter in text.ToCharArray())
            {
                gameText.text += letter;
                yield return new WaitForSeconds(typingFrequency);
            }
        }
    }
}
