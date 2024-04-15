using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private Rigidbody2D rb;
    private Vector2 workspace;
    [SerializeField] private float movementVelocity;
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        float horizontalMovementInput = Input.GetAxisRaw("Horizontal");
        float verticalMovementInput = Input.GetAxisRaw("Vertical");

        Vector2 inputVector = new Vector2(horizontalMovementInput, verticalMovementInput).normalized;
        rb.velocity = inputVector * movementVelocity * Time.deltaTime;
    }
}
