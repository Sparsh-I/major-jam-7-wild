using System;
using TMPro;
using UnityEngine;

namespace Controllers
{
    public class HiddenDoorController : InteractableController
    {
        [SerializeField] private GameObject hiddenDoor;

        private void Start()
        {
            hiddenDoor.SetActive(false);
        }
        
        protected override void RemoveChildObject()
        {
            if (transform.childCount > 0 && !itemStaysTheSame)
            {
                var child = transform.GetChild(0);
                child.gameObject.SetActive(itemToAddTo);
            }
            
            hiddenDoor.SetActive(true);
        }
    }
}