using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateDetect : MonoBehaviour
{
    //LEVEL 1 Objects
    [Header("Level 1")]
    public bool Level1AllPlateActive;
    public GameObject Level1Door;
    public UsePurplePlate Level1plate1;
    public UsePurplePlate Level1plate2;

    //LEVEL 2 Objects
    [Header("Level 2")]
    public bool Level2AllPlateActive;
    public GameObject Level2Door;
    public UsePlate Level2plate1;
    public UsePlate Level2plate2;
    void Update()
    {
        //LEVEL 1   || 2 plate var aynı anda ikisinede basılırsa aktif kalır
        if (Level1plate1.Plate == true && Level1plate2.Plate == true)
        {
            Level1AllPlateActive = true;
        }

        if (Level1AllPlateActive == true) {
            Level1Door.SetActive(false);
        }else
        {
            Level1Door.SetActive(true);
        }

        //LEVEL 2   || 2 plate var aynı anda ikisinede basılırsa aktif olur ustünden çekilirsen pasif olur.
        if (Level2plate1.Plate == true && Level2plate2.Plate == true)
        {
            Level2AllPlateActive = true;
        }else 
        {
            Level2AllPlateActive = false;
        }

        if (Level2AllPlateActive == true) {
            Level2Door.SetActive(false);
        }else
        {
            Level2Door.SetActive(true);
        }
    }
}
