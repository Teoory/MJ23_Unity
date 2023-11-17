using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessMaze : MonoBehaviour
{
    public Transform TargetPosition;

    private void OnTriggerEnter(Collider other) {
        other.transform.position = TargetPosition.transform.position;
    }
}
