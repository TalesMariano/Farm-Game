using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridFarm : MonoBehaviour
{
    public Planter planter;

    [SerializeField]
    public GridSpace[] grid;



    public void ReceiveClick(Transform transf)
    {
        int numGrid;
        int.TryParse(transf.name, out numGrid);


        int planterClickValue = planter.ReceiveInputGrid(grid[numGrid].cell.transform, grid[numGrid].occupied);

        if (planterClickValue == 1)
            FillCell(numGrid);
        else if (planterClickValue == 2)
            EmptyCell(numGrid);
    }



    public void FillCell(int numCell)
    {
        grid[numCell].occupied = true;
    }

    public void EmptyCell(int numCell)
    {
        grid[numCell].occupied = false;
    }

    [ContextMenu("Get Cell")]
    void GetCell()
    {
        grid = new GridSpace[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            grid[i] = new GridSpace();


            grid[i].cell = transform.GetChild(i).gameObject;
            transform.GetChild(i).name = i + "";
        }
    }
}

[System.Serializable]
public class GridSpace
{
    public GameObject cell;
    public bool occupied = false;
    
    //public Farm farm;
}