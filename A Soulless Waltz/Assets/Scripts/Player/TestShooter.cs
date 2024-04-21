using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShooter : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float accuracy;
    private Vector3 movementVector;
    private Vector3 leftAngleVector;
    private Vector3 rightAngleVector;
    private Vector3 mousePosition;
    private Vector3 mouseVector;

    void Update() {            
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseVector = (mousePosition - transform.position);

        float leftAngleBound = Mathf.Atan2(mouseVector.y, mouseVector.x) + (-accuracy * Mathf.Deg2Rad);
        float rightAngleBound = Mathf.Atan2(mouseVector.y, mouseVector.x) + (accuracy * Mathf.Deg2Rad);
        leftAngleVector = new Vector2(Mathf.Cos(leftAngleBound), Mathf.Sin(leftAngleBound));
        rightAngleVector = new Vector2(Mathf.Cos(rightAngleBound), Mathf.Sin(rightAngleBound));

        Debug.Log(Mathf.Abs(Mathf.Sin(accuracy * Mathf.Deg2Rad)) + " " + Mathf.Abs(Mathf.Atan2(mouseVector.y, mouseVector.x)));

        if (Input.GetKeyDown(KeyCode.G)) {

            float defaultAngle = Mathf.Atan2(mouseVector.y, mouseVector.x) + (Random.Range(-accuracy, accuracy) * Mathf.Deg2Rad);
            
            Debug.Log("" + defaultAngle);

            movementVector = new Vector2(Mathf.Cos(defaultAngle), Mathf.Sin(defaultAngle));

            GameObject projectileSpawned = Instantiate(projectile, transform.position, Quaternion.LookRotation(transform.forward, movementVector));
            Rigidbody2D projectileRB = projectileSpawned.GetComponent<Rigidbody2D>();
            projectileRB.velocity = movementVector * 10f;
        }

    }

    private void OnDrawGizmos() {
        Gizmos.DrawLine(transform.position, transform.position + leftAngleVector * 1.5f * Mathf.Sqrt(Mathf.Pow(mouseVector.x, 2) + Mathf.Pow(mouseVector.y, 2)));
        Gizmos.DrawLine(transform.position, transform.position + rightAngleVector * 1.5f * Mathf.Sqrt(Mathf.Pow(mouseVector.x, 2) + Mathf.Pow(mouseVector.y, 2)));
        Gizmos.DrawWireSphere(mousePosition, Mathf.Abs(Mathf.Sin(accuracy * Mathf.Deg2Rad)) * Mathf.Sqrt(Mathf.Pow(mouseVector.x, 2) + Mathf.Pow(mouseVector.y, 2)));
    }
}

