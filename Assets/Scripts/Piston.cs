using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston : MonoBehaviour
{
    public bool PistonActive = false;
    private bool butonActive = false;
    public GameObject PistonMove;
    public GameObject PistonPush;
    private float scaleSpeed = 0.025f;
    public float timer = 0;

    private void Update() {
        PistonActived();
    }

    private void FixedUpdate() {
        
        
        if(butonActive == true) 
        {
            timer += 0.1f;
            var newScale = Mathf.Lerp(0.4f, 3.0f, timer * scaleSpeed);
            PistonPush.transform.localScale = new Vector3(0.1f, 0.1f, newScale);
            var newPushPos = Mathf.Lerp(7.7f, 6.5f, timer * scaleSpeed);
            PistonPush.transform.localPosition = new Vector3(5.6f, -3.8f, newPushPos);

            var newPos = Mathf.Lerp(7.5f, 5.0f, timer * scaleSpeed);
            PistonMove.transform.localPosition = new Vector3(5.6f, -3.8f, newPos);
        }

        if (timer == 30)
        {
            timer = 0;
            butonActive = false;
        }

        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Player")
        {
            PistonActive = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player")
        {
            PistonActive = false;
        }
    }


    void PistonActived() {
        if(PistonActive == true && Input.GetKeyDown(KeyCode.E)) {
            butonActive = true;
            PistonActive = false;
            Debug.Log("E bastın!");
        }
    }
    // void PistonDeactived() {
        
    //     if(PistonActive == false)
    //     {
    //         PistonMove.transform.localPosition = new Vector3(5.6f, -3.8f, 7.5f) * Time.deltaTime * growFactor;
    //         PistonPush.transform.localScale = new Vector3(0.1f, 0.1f, 0.4f) * Time.deltaTime * growFactor;
    //     }
    // }
}
