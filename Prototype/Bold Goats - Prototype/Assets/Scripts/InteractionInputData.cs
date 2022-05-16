using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InteractionInputData", menuName = "InteractionSystem/InputData")]

public class InteractionInputData : ScriptableObject
{
    // Variables
    private bool interactedClicked;
    private bool interactedReleased;

    // Properties
    public bool InteractedClick
    {
        get
        {
            return interactedClicked;
        }

        set
        {
            interactedClicked = value;
        }
    }

    public bool InteractedRelease
    {
        get
        {
            return interactedReleased;
        }

        set
        {
            interactedReleased = value;
        }
    }

    // Reset When Opening Game Again
    public void ResetInput()
    {
        interactedClicked = false;
        interactedReleased = false;
    }
}
