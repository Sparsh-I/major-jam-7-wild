using UnityEngine;

namespace Managers
{
    public class RulebookManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] descriptions;

        private void Start()
        {
            foreach (var description in descriptions) description.SetActive(false);
            
            descriptions[4].SetActive(true);
        }
        
        public void SwapHandsDescription()
        {
            foreach (var description in descriptions) description.SetActive(false);
            
            descriptions[0].SetActive(true);
        }
        
        public void InvertControlsDescription()
        {
            foreach (var description in descriptions) description.SetActive(false);
            
            descriptions[1].SetActive(true);
        }
        
        public void DisappearingGroundDescription()
        {
            foreach (var description in descriptions) description.SetActive(false);
            
            descriptions[2].SetActive(true);
        }
        
        public void TimerDescription()
        {
            foreach (var description in descriptions) description.SetActive(false);
            
            descriptions[3].SetActive(true);
        }
    }
}