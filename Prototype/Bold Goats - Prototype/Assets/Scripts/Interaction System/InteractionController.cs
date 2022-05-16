using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    // Variables
    [Header("Data")]
    public InteractionInputData interactionInputData;
    public InteractionData interactionData;

    [Space, Header("UI")]
    public InteractionUIPanel uiPanel;


    [Space]
    [Header("Ray Settings")]
    public float rayDistance;
    public float raySphereRadius;
    public LayerMask interactableLayer;

    public Camera cam;
    private bool interacting;
    private float holdTimer = 0f;

    public AudioSource Terminal;

    void Update()
    {
        CheckForInteractable();
        CheckForInteractableInput();
    }

    void CheckForInteractable()
    {
        if (GameManager.Instance.Player != null && cam != null)
        {
            Vector3 posToDraw = new Vector3
                (
                GameManager.Instance.Player.transform.position.x,
                GameManager.Instance.Player.transform.position.y + 1.5f, 
                GameManager.Instance.Player.transform.position.z
                );

            Ray ray = new Ray(posToDraw, cam.transform.forward);
            RaycastHit hitInfo;

            bool hitSomething = Physics.SphereCast(ray, raySphereRadius, out hitInfo, rayDistance, interactableLayer);
            if (hitSomething)
            {
                InteractableBase _interactable = hitInfo.transform.GetComponent<InteractableBase>();
                uiPanel.Show(hitSomething);

                if (_interactable != null)
                {
                    if (interactionData.IsEmpty())
                    {
                        interactionData.Interactable = _interactable;
                        uiPanel.SetTooltip(_interactable.TooltipMessage);
                    }
                    else
                    {
                        if (!interactionData.IsSameInteractable(_interactable))
                        {
                            interactionData.Interactable = _interactable;
                            uiPanel.SetTooltip(_interactable.TooltipMessage);
                        }
                    }
                }
            }
            else
            {
                uiPanel.ResetUI();
                interactionData.ResetData();
                uiPanel.Show(hitSomething);
            }

            Debug.DrawRay(ray.origin, ray.direction * rayDistance, hitSomething ? Color.green : Color.red);
        }
    }

    void CheckForInteractableInput()
    {
        if (interactionData.IsEmpty())
            return;

        if (interactionInputData.InteractedClick)
        {
            interacting = true;
            holdTimer = 0f;
        }

        if (interactionInputData.InteractedRelease)
        {
            interacting = false;
            holdTimer = 0f;
            uiPanel.UpdateProgressBar(holdTimer);
        }

        if (interacting)
        {
            if (!interactionData.Interactable.IsInteractable)
                return;

            if (interactionData.Interactable.HoldInteract)
            {
                holdTimer += Time.deltaTime;

                float heldPercent = holdTimer / interactionData.Interactable.HoldDuration;
                uiPanel.UpdateProgressBar(heldPercent);

                if (heldPercent > 1f)
                {
                    //uiPanel.Show(false);
                    if (interactionData.Interactable.tag == "Interact") 
                    {
                        Terminal.Play();
                    }
                    interactionData.Interact();
                    interacting = false;
                }
            }
            else
            {
                //uiPanel.Show(false);
                interactionData.Interact();
                interacting = false;
            }
        }

    }
}