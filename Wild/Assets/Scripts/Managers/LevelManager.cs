using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private string levelToLoad;
    
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player")) SceneManager.LoadScene(levelToLoad);
        }
    }
}
