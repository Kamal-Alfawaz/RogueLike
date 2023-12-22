using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;

    public static InputManager Instance {
        get {
            return _instance; 
        } 
    }

    private PlayerMovements playerMovements;

    private void Awake() {
        if(_instance != null && _instance != this){
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }

        playerMovements = new PlayerMovements();
    }

    private void OnEnable() {
        playerMovements.Enable();
    }

    private void OnDisable() {
        playerMovements.Disable();
    }

    public Vector2 GetPlayerMovement(){
        return playerMovements.Player.Move.ReadValue<Vector2>(); 
    }

    public Vector2 GetMouseDelta(){
        return playerMovements.Player.Look.ReadValue<Vector2>(); 
    }

    public bool GetJump(){
        return playerMovements.Player.Jump.triggered;
    }

}
