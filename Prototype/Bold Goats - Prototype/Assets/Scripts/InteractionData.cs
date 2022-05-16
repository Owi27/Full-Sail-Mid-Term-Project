using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Interaction Data", menuName = "InteractionSystem/InteractionData")]

public class InteractionData : ScriptableObject
{
    // Variable
    private InteractableBase interactable;

    // Properties
    public InteractableBase Interactable
    {
        get
        {
            return interactable;
        }
        set
        {
            interactable = value;
        }
    }

    // Methods
    public void Interact()
    {
        interactable.OnInteract();
        ResetData();
    }

    public bool IsSameInteractable(InteractableBase _newInteractable)
    {
        return interactable == _newInteractable;
    }

    public bool IsEmpty()
    {
        return interactable == null;
    }

    public void ResetData()
    {
        interactable = null;
    }
}
