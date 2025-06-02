using System;
using TMPro;
using UnityEngine;

public class InteractableController : MonoBehaviour
{
    [Header("Interaction Texts")]
    [Tooltip("The text to display when interacting before getting the needed item")] [SerializeField]
    private string beforeUnlockText; 

    [Tooltip("The text to display when interacting when holding the needed item")] [SerializeField]
    private string atUnlockText;

    [Tooltip("The text to display when interacting after getting this item")] [SerializeField]
    private string afterUnlockText;
    
    [Header("Interaction Rules")]
    [SerializeField] private bool hasBeenUnlocked;
    [SerializeField] private bool hasNeededItem;
    
    
    private void Start()
    {
        hasBeenUnlocked = false;
    }

    public string InteractWithPlayer()
    {
        if (hasBeenUnlocked)
        {
            return afterUnlockText;
        }

        if (hasNeededItem)
        {
            hasBeenUnlocked = true;
            return atUnlockText;
        }
        else
        {
            return beforeUnlockText;
        }
    }
}