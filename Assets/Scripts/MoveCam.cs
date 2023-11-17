using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCam : MonoBehaviour
{
    public Transform cemarePosition;

    private void Update() {
        transform.position = cemarePosition.position;
    }
}
