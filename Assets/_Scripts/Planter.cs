using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planter : MonoBehaviour
{
    public GameObject[] cropPrefabs;
    public int selectCrop = 0;
    public bool planting = false;





    public void PlantCrop(Transform father)
    {
        if (!planting)
            return; // break the planting function if it is not in planting mode

        GameObject newTree = Instantiate(cropPrefabs[selectCrop], father, false) as GameObject;

        planting = false;
    }



    public void SelectSeed(int numSeed)
    {
        selectCrop = numSeed;
        planting = true;
    }
}
