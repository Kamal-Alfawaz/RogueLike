using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

interface IInteractable{
    public void Interact();
}

public class Interactor : MonoBehaviour
{
    public Transform InteractorSource;
    public float InteractRange;
    PauseMenu action;

    private void Awake() 
    {
        action = new PauseMenu();
    }

    private void Start() {
        action.Interact.InteractButton.performed += _ => Interacting();
    }
    
    private void OnEnable(){
        action.Enable();
    }

    private void OnDisable(){
        action.Disable();
    }

    private void Interacting(){
        Ray r = new Ray(InteractorSource.position, InteractorSource.forward);
        if(Physics.Raycast(r, out RaycastHit hitInfo, InteractRange)){
            if(hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactObj)){
                interactObj.Interact();
            }else{
                Debug.Log("Second If");
            }
        }else{
            Debug.Log("First If");
        }
    }

}
