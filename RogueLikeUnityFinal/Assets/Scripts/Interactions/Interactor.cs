using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable
{
    public void Interact();
    public void InRange();
    public void OutOfRange();
}

public class Interactor : MonoBehaviour
{
    public float InteractRange;
    private IInteractable currentInteractable = null;
    PauseMenu action;

    private void Awake() {
        action = new PauseMenu();
    }

    private void OnEnable(){
        action.Enable();
    }

    private void OnDisable(){
        action.Disable();
    }

    // Update is called once per frame
    private void Update(){
        bool foundInteractable = false;

        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

        if(Physics.Raycast(ray, out RaycastHit hitInfo, InteractRange)){
            if(hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj)){
                foundInteractable = true;

                // If focusing on a new interactable object
                if(currentInteractable != interactObj){
                    // Notify the previous interactable that it's no longer in range
                    if(currentInteractable != null){
                        currentInteractable.OutOfRange();
                    }

                    // Update the current interactable and notify it
                    currentInteractable = interactObj;
                    currentInteractable.InRange();
                }

                // Check for interaction
                if(action.Interact.InteractButton.triggered){
                    currentInteractable.Interact();
                }
            }
        }

        // If no interactable is found and there was a previous interactable, notify it
        if(!foundInteractable && currentInteractable != null){
            currentInteractable.OutOfRange();
            currentInteractable = null;
        }
    }
}
