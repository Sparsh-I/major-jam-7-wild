using System;
using Managers;
using TMPro;
using UnityEngine;

namespace Controllers
{
    public class InteractableController : MonoBehaviour
    {
        [Header("Item Settings")]
        [Tooltip("What item do you get after unlocking this item?")] [SerializeField]
        private string itemObtained;
        
        [Tooltip("What item is needed to unlock this interactable?")] [SerializeField]
        private string itemNeeded;
        
        [Tooltip("Does this item change appearance by having something added to it (e.g. statue with rock added)?")] [SerializeField] 
        protected bool itemToAddTo;
        
        [Tooltip("Does this item stay the same after interacting?")] [SerializeField] 
        protected bool itemStaysTheSame;
        
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

            if (GameManager.Instance.DisplayTextCoroutine == null) _completedFirstInteraction = true;
            return beforeUnlockText;
        }

        protected virtual void RemoveChildObject()
        {
            if (transform.childCount > 0 && !itemStaysTheSame)
            {
                var child = transform.GetChild(0);
                child.gameObject.SetActive(itemToAddTo);
            }
        }
    }
}