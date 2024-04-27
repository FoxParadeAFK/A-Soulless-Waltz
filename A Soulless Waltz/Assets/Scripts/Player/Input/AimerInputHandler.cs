using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AimerInputHandler : MonoBehaviour
{
    private PlayerInput playerInput;
    public Vector2 RawAimInput { get; private set; }
    public bool KeyboardActive { get; private set;}

    private void Start() {
        playerInput = GetComponent<PlayerInput>();
    }
    public void OnAimInput(InputAction.CallbackContext context) {
        RawAimInput = context.ReadValue<Vector2>();

        if (playerInput.currentControlScheme == "Keyboard Mouse") {
            RawAimInput = Camera.main.ScreenToWorldPoint(RawAimInput);
            KeyboardActive = true;
        }

        // if (playerInput.currentControlScheme == "Gamepad") {
        //     Debug.Log("Gamepad");
        //     KeyboardActive = false;
        // }

        // Debug.Log(RawAimInput);
    }
}
