using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsePurplePlate : MonoBehaviour
{
    [SerializeField] public bool Plate = false;
    public GameObject Level1PressurePlate1;

    private void OnTriggerStay(Collider other) {
        if (other.tag == "Player" || other.tag == "Box" )
        {
            PlateActive();
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player" || other.tag == "Box" )
        {
            PlateDeactive();
        }
    }

    void PlateActive() {
        if ((gameObject.tag == "Plate"))
        {
            Plate = true;
                var PressurePlateRenderer = Level1PressurePlate1.GetComponent<Renderer>();
                Color BlueColor = new Color(0.4f, 0.9f, 0.7f, 1.0f);
                PressurePlateRenderer.material.SetColor("_Color", BlueColor);
                Level1PressurePlate1.transform.position = new Vector3(transform.position.x, 0.501f, transform.position.z);
        }
    }

    void PlateDeactive() {
        Plate = false;
            var PressurePlateRenderer = Level1PressurePlate1.GetComponent<Renderer>();
            Color PurpleColor = new Color(0.3696442f, 0f, 1.0f, 1.0f);
            PressurePlateRenderer.material.SetColor("_Color", PurpleColor);
            Level1PressurePlate1.transform.position = new Vector3(transform.position.x, 0.55f, transform.position.z);
    }
}
