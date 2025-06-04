using System;
using TMPro;
using UnityEngine;

namespace Controllers
{
    public class HiddenDoorController : InteractableController
    {
        [Header("Hidden Door Settings")]
        [SerializeField] private GameObject hiddenDoor;
        [SerializeField] private bool startVisible;

        private void Start()
        {
            hiddenDoor.SetActive(startVisible);
        }
        
        protected override void RemoveChildObject()
        {
            if (transform.childCount > 0 && !itemStaysTheSame)
            {
                var child = transform.GetChild(0);
                child.gameObject.SetActive(itemToAddTo);
            }
            
            hiddenDoor.SetActive(!startVisible);
        }
    }
}