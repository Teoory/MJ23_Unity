using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    [Header("Pickup ayarlari")]
    [SerializeField] Transform HolderArea;
    private GameObject holdObj;
    private Rigidbody holdObjRB;

    public bool holding = false;


    [Header("Fizik Sistemi")]
    [SerializeField] private float pickupRange = 5.0f;
    [SerializeField] private float pickupForce = 150.0f;

    
    [Header("keyCodes")]
    public KeyCode HoldKey = KeyCode.C;

    private void Update() {
        if(Input.GetKeyDown(HoldKey))
        {
            if (holdObj == null)
            {
                RaycastHit hit;
                if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, pickupRange) && hit.transform.tag == "Box")
                { 
                    //pickup object
                    PickupObject(hit.transform.gameObject);
                    holding = true;
                    
                    
                }
            }else
            {
                //drop object
                DropObject();
                holding = false;
            }
        }
        if(holdObj != null)
        {
            //move object
            MoveObject();            
        }
    }

    void MoveObject()
    {
        if(Vector3.Distance(holdObj.transform.position, HolderArea.position) > 0.1f)
        {
            Vector3 moveDirection = (HolderArea.position - holdObj.transform.position);
            holdObjRB.AddForce(moveDirection * pickupForce);
        }
    }

    void PickupObject(GameObject pickObj)
    {
        if(pickObj.CompareTag("Box")){
            if(pickObj.GetComponent<Rigidbody>())
            {
                pickObj.GetComponent<Collider>().isTrigger = true;
                holdObjRB = pickObj.GetComponent<Rigidbody>();
                holdObjRB.useGravity = false;
                holdObjRB.drag = 10;
                holdObjRB.constraints = RigidbodyConstraints.FreezeRotation;
    
                holdObjRB.transform.parent = HolderArea;
                holdObj = pickObj;
            }
        }
    }
    void DropObject()
    {
        holdObj.GetComponent<Collider>().isTrigger = false;
        holdObjRB.useGravity = true;
        holdObjRB.drag = 1;
        holdObjRB.constraints = RigidbodyConstraints.None;

        holdObj.transform.parent = null;
        holdObj = null;
    }

    
}
