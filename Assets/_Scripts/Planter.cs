using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planter : MonoBehaviour
{
    public GameObject[] cropPrefabs;
    public int selectCrop = 0;
    public bool planting = false;
    public bool destroying = false;



    public void ReceiveInput(Transform father)
    {
        if (planting)
        {
            PlantCrop(father);
        }
        else if (destroying)
        {
            DestroyCrop(father);
        }
    }

    public void PlantCrop(Transform father)
    {
        //if (!planting)
        //    return; // break the planting function if it is not in planting mode

        



        GameObject newTree = Instantiate(cropPrefabs[selectCrop], father, false) as GameObject;

        planting = false;
    }

    public void DestroyCrop(Transform father)
    {
        //if (!destroying)
        //    return;
        if(father.childCount > 0)
            Destroy(father.GetChild(0).gameObject);

        destroying = false;
    }

    public void SelectSeed(int numSeed)
    {
        selectCrop = numSeed;
        planting = true;

        // Check for enough gold

        GameManager.instance.LoseGold(cropPrefabs[selectCrop].GetComponent<Farm>().so_farm.buildingCost);
    }

    public void SelectDestroy()
    {
        destroying = !destroying;
    }
}
