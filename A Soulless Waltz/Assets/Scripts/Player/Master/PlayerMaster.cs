using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMaster : MonoBehaviour {
    private Rigidbody2D RB;
    private float movementSpeed = 300;

    public Action EventFireAuto;
    public Action EventFireSemi;
    public Action EventAimActive;
    public Action EventAimCancel;
    // public Action EventReload;
    public Action EventBreakReload;
    public Action<int> EventRotation;

    private bool isAiming;

    private bool scrollReseted;

    // public Action EventReload;

    private void Start() {
        RB = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector2 XYMovementVector = new Vector2(horizontalInput, verticalInput).normalized;

        RB.velocity = XYMovementVector * movementSpeed * (isAiming? 0.2f : 1) * Time.deltaTime;

        // * Fire the weapon
        if (Input.GetMouseButton(0)) HandleFireInputAuto();
        if (Input.GetMouseButtonDown(0)) HandleFireInputSemi();

        // * To aim
        if (Input.GetMouseButton(1)) HandleAimingActive();
        if (Input.GetMouseButtonUp(1)) HandleAimingCancel();

        // * Reload (WIP)
        if (Input.GetKeyDown(KeyCode.R)) HandleReload();

        // * Rotate
        if (Input.mouseScrollDelta.y == 0) scrollReseted = true;
        if (Input.mouseScrollDelta.y >= 1.1f && scrollReseted) {
            scrollReseted = false;
            HandleRotation(1);
        }
        if (Input.mouseScrollDelta.y <= -1.1f && scrollReseted) {
            scrollReseted = false;
            HandleRotation(-1);

        }
    }

    private void HandleFireInputAuto() => EventFireAuto?.Invoke();
    private void HandleFireInputSemi() => EventFireSemi?.Invoke();
    private void HandleAimingActive() {
        isAiming = true;
        EventAimActive?.Invoke();
    }
    private void HandleAimingCancel() {
        isAiming = false;
        EventAimCancel?.Invoke();
    }
    private void HandleReload() => EventBreakReload?.Invoke();
    private void HandleRotation(int counterwise) => EventRotation?.Invoke(counterwise);
}
