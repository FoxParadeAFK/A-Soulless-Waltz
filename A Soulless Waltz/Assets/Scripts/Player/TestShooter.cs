using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShooter : MonoBehaviour
{
    private Transform aimer;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float accuracy;
    private Vector3 movementVector;
    private Vector3 leftAngleVector;
    private Vector3 rightAngleVector;
    private Vector3 mousePosition;
    private Vector3 mouseVector;

    private void Start() {
        aimer = GameObject.Find("Aimer").GetComponent<Transform>();
    }

    void Update() {            
        // mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseVector = aimer.position - transform.position;

        // * Drawing the debug lines

        if (Input.GetKey(KeyCode.Space)) {

            // * Aiming master script
            float defaultAngle = Mathf.Atan2(mouseVector.y, mouseVector.x) + (Random.Range(-accuracy, accuracy) * Mathf.Deg2Rad);
            movementVector = new Vector2(Mathf.Cos(defaultAngle), Mathf.Sin(defaultAngle));


            // Debug.Log("" + movementVector);

            GameObject projectileSpawned = Instantiate(projectile, transform.position, Quaternion.LookRotation(transform.forward, movementVector));
            Rigidbody2D projectileRB = projectileSpawned.GetComponent<Rigidbody2D>();
            projectileRB.velocity = movementVector * 10f;
        }

    }

    private void OnDrawGizmos() {
        // * Move to the weapon dependent 
        float leftAngleBound = Mathf.Atan2(mouseVector.y, mouseVector.x) + (-accuracy * Mathf.Deg2Rad);
        float rightAngleBound = Mathf.Atan2(mouseVector.y, mouseVector.x) + (accuracy * Mathf.Deg2Rad);
        leftAngleVector = new Vector2(Mathf.Cos(leftAngleBound), Mathf.Sin(leftAngleBound));
        rightAngleVector = new Vector2(Mathf.Cos(rightAngleBound), Mathf.Sin(rightAngleBound));
        // Requires the position
        // Mouse Vector
        Gizmos.DrawLine(transform.position, transform.position + leftAngleVector * 1.5f * Mathf.Sqrt(Mathf.Pow(mouseVector.x, 2) + Mathf.Pow(mouseVector.y, 2)));
        Gizmos.DrawLine(transform.position, transform.position + rightAngleVector * 1.5f * Mathf.Sqrt(Mathf.Pow(mouseVector.x, 2) + Mathf.Pow(mouseVector.y, 2)));
        Gizmos.DrawWireSphere(aimer.position, Mathf.Abs(Mathf.Sin(accuracy * Mathf.Deg2Rad)) * Mathf.Sqrt(Mathf.Pow(mouseVector.x, 2) + Mathf.Pow(mouseVector.y, 2)));
    }
}

