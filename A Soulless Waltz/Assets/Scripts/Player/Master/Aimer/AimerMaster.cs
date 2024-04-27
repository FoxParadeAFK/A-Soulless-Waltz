using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimerMaster : MonoBehaviour
{
    private AimerInputHandler aimerInputHandler;
    public Vector3 MouseVector { get; private set; }

    private void Start() {
        aimerInputHandler = GetComponent<AimerInputHandler>();
    }

    private void Update() {
        transform.position = aimerInputHandler.RawAimInput;
        MouseVector = transform.position - transform.parent.position;
    }

    public Vector3 MovementVectorCalculation(float accuracy) {
        float defaultAngle = Mathf.Atan2(MouseVector.y, MouseVector.x) + (Random.Range(-accuracy, accuracy) * Mathf.Deg2Rad);
        return new Vector2(Mathf.Cos(defaultAngle), Mathf.Sin(defaultAngle));
    }
}
