using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBase : MonoBehaviour, IInteractables
{    
    // Variables
    [Header("Interactable Settings")]

    [SerializeField] private float holdDuration;

    [Space]
    [SerializeField] private bool holdInteract;

    [SerializeField] private bool multipleUse;

    [SerializeField] private bool isInteractable;

    [SerializeField] private string tooltipMessage = "Interact";

    // Properties
    public float HoldDuration
    {
        get
        {
            return holdDuration;
        }
    }
    
    public bool HoldInteract
    {
        get
        {
            return holdInteract;
        }
    }

    public bool MultipleUse
    {
        get
        {
            return multipleUse;
        }
    }

    public bool IsInteractable
    {
        get
        {
            return isInteractable;
        }
    }

    public string TooltipMessage
    {
        get
        {
            return tooltipMessage;
        }
    }


    // Mehtods
    public virtual void OnInteract()
    {
        
    }
}
