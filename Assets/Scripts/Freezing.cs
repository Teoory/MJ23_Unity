using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Freezing : MonoBehaviour
{
    public GameObject FreezeIcon;
    public Text FreezeText;

    public float cooling = 0;
    public float maxCooling = 20;

    private bool FreezeLocation;


    private void Awake() {
        FreezeIcon.SetActive(false);
        FreezeText.gameObject.SetActive(false);
        
    }

    private void Start() {
        cooling = 0;
        FreezeLocation = false;
    }

    private void Update() {
        if(cooling >= maxCooling)
        {
            cooling = maxCooling;
            SceneManager.GetActiveScene();
            Debug.Log("öldün!");
        }

        if(cooling <= 0)
        {
            cooling = 0;
            FreezeIcon.SetActive(false);
            FreezeText.gameObject.SetActive(false);
        }
        
        FreezeText.text = "-" + cooling.ToString("F0") + " °C";

        if(FreezeLocation)
        {
            float t = Time.deltaTime / 1f;
            cooling += t;
            FreezeIcon.SetActive(true);
            FreezeText.gameObject.SetActive(true);
        }

        if(FreezeLocation == false && cooling >= 0.01f)
        {
            float t = Time.deltaTime / 1f;
            cooling -= (t+0.01f);
        }
    }

    private void OnTriggerStay(Collider other) {
        if(other.CompareTag("FreezeLocation"))
        {
            FreezeLocation = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("FreezeLocation") )
        {
            FreezeLocation = false;
        }
    }
}
