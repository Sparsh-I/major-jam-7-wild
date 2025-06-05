using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private TextMeshProUGUI gameText;
        [SerializeField] private float typingFrequency;
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private TextMeshProUGUI gameOverReason;
        
        public Coroutine DisplayTextCoroutine;
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            gameText.enabled = false;
        }
        
        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Restart();
        }

        private void Start()
        {
            Restart();
        }

        private void Restart()
        {
            Time.timeScale = 1f;
            gameText.text = "";
            if (gameText != null)
            {
                gameText.text = "";
                gameText.enabled = false;
            }

            if (gameOverScreen != null)
                gameOverScreen.SetActive(false);
        }

        public void SetGameText(string text)
        {
            gameText.enabled = true;

            if (DisplayTextCoroutine != null) StopCoroutine(DisplayTextCoroutine);
            
            DisplayTextCoroutine = StartCoroutine(DisplayText(text));
        }

        private IEnumerator DisplayText(string text)
        {
            gameText.text = "";
            foreach (var letter in text.ToCharArray())
            {
                gameText.text += letter;
                yield return new WaitForSeconds(typingFrequency);
            }
            DisplayTextCoroutine = null;
        }

        public void DisplayGameOverScreen(string reason)
        {
            Time.timeScale = 0f;
            gameOverReason.text = reason;
            gameOverScreen.SetActive(true);
        }
    }
}
