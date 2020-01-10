using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridFarm : MonoBehaviour
{
    [SerializeField]
    public GridSpace[] grid;

    [ContextMenu("Get Cell")]
    void GetCell()
    {
        grid = new GridSpace[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            grid[i] = new GridSpace();


            grid[i].cell = transform.GetChild(i).gameObject;
        }
    }
}

[System.Serializable]
public class GridSpace
{
    public GameObject cell;
    public bool occupied = false;
    
    public Farm farm;
}