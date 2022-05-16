using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public InteractionInputData interactionInputData;
    

    // Start is called before the first frame update
    void Start()
    {
        interactionInputData.ResetInput();
    }

    // Update is called once per frame
    void Update()
    {
        GetInteractionInputData();
    }

    void GetInteractionInputData()
    {
        interactionInputData.InteractedClick = Input.GetKeyDown(KeyCode.E);

        if (interactionInputData.InteractedClick)
        {
            SoundManager.PlaySound(SoundManager.Sound.ActivateTerminal);
        }
        //Debug.Log("E clicked" + interactionInputData.InteractedClick);
        interactionInputData.InteractedRelease = Input.GetKeyUp(KeyCode.E);
        //Debug.Log("E clicked" + interactionInputData.InteractedRelease);
    }
}
