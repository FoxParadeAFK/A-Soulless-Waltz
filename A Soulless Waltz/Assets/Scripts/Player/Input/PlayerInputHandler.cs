using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour {
    public Vector2 RawMovementInput { get; private set; }
    public Vector2 NormMovementInput { get; private set; }
    public bool NormFireInputAuto { get; private set; }
    public bool NormFireInputSemi { get; private set; }

    public void OnMoveInput(InputAction.CallbackContext context) {
        RawMovementInput = context.ReadValue<Vector2>();

        NormMovementInput = Vector2Int.RoundToInt(RawMovementInput);
    }

    public void OnFireInput(InputAction.CallbackContext context) {
        if (context.started) {
            NormFireInputAuto = true;
            NormFireInputSemi = true;
        }

        if (context.canceled) {
            NormFireInputAuto = false;
        }
    }

    public void OnFireSemiDisable() => NormFireInputSemi = false;

}
