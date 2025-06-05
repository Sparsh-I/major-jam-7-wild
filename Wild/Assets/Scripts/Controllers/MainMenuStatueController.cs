using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers
{
    public class MainMenuStatueController : InteractableController
    {
        [Header("Main Menu Settings")]
        [SerializeField] private string sceneToLoad;
        [SerializeField] private float delayTime;

        
        protected override void RemoveChildObject()
        {
            if (transform.childCount > 0 && !itemStaysTheSame)
            {
                var child = transform.GetChild(0);
                child.gameObject.SetActive(itemToAddTo);
            }

            StartCoroutine(DelaySceneLoad());
        }

        private IEnumerator DelaySceneLoad()
        {
            yield return new WaitForSeconds(delayTime);
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}