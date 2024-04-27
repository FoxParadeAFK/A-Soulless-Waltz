using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLogic : MonoBehaviour {
    [SerializeField] private LayerMask whatIsWalls;
    [SerializeField] private float raycastDistance;

    private Rigidbody2D rigidbody2D;
    private Vector3 reboundVector;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        if (Physics2D.Raycast(transform.position, transform.up * raycastDistance, raycastDistance, whatIsWalls)) {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + transform.up * raycastDistance);
    }
}
