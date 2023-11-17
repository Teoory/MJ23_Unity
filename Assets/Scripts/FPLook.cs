using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPLook : MonoBehaviour
{
    [SerializeField]
    Transform character;
    public float sensitivity = 2;
    public float smoothing = 1.5f;

    Vector3 velocity;
    Vector3 frameVelocity;



    void Reset()
    {
        // Get the character from the FirstPersonMovement in parents.
        character = GetComponentInParent<KarakterKontrol>().transform;
    }

    void Start()
    {
        // Lock the mouse cursor to the game screen.
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Get smooth velocity.
        Vector3 mouseDelta = new Vector3(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        Vector3 rawFrameVelocity = Vector3.Scale(mouseDelta, Vector3.one * sensitivity);
        frameVelocity = Vector3.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing);
        velocity += frameVelocity;
        velocity.y = Mathf.Clamp(velocity.y, -90, 90);

        // Rotate camera up-down and controller left-right from velocity.
        transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);
        character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
    }



    
    void OnDrawGizmosSelected()
    {
        // Draw a line in the Editor to show whether we are touching the ground.
        // Debug.DrawLine(RaycastOrigin, RaycastOrigin + Vector3.forward * RaycastDistance, Color.red);
        
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 5;
        Debug.DrawRay(transform.position, forward, Color.red);
    }
}
