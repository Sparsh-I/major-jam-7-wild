using System;
using TMPro;
using UnityEngine;

namespace Controllers
{
    public class InteractableController : MonoBehaviour
    {
        [Header("Item Settings")]
        [SerializeField] private string itemObtained;
        [SerializeField] private string itemNeeded;
        
        [Header("Interaction Texts")]
        [Tooltip("The text to display when interacting before getting the needed item")] [SerializeField]
        private string beforeUnlockText;

        [Tooltip("The text to display when interacting when holding the needed item")] [SerializeField]
        private string atUnlockText;

        [Tooltip("The text to display when interacting after getting this item")] [SerializeField]
        private string afterUnlockText;

        private bool _hasBeenUnlocked;
        private bool _completedFirstInteraction;


        private void Start()
        {
            _completedFirstInteraction = false;
            _hasBeenUnlocked = false;
        }

        public string InteractWithPlayer(PlayerController player)
        {
            if (_hasBeenUnlocked)
            {
                return afterUnlockText;
            }

            if (player.HasNeededItem(itemNeeded) && _completedFirstInteraction)
            {
                _hasBeenUnlocked = true;
                player.PickUpItem(itemObtained);
                RemoveChildObject();
                return atUnlockText;
            }

            _completedFirstInteraction = true;
            return beforeUnlockText;
        }

        private void RemoveChildObject()
        {
            if (transform.childCount > 0)
            {
                Transform child = transform.GetChild(0);
                child.gameObject.SetActive(false);
            }
        }
    }
}