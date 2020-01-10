using UnityEngine;

public class Planter : MonoBehaviour
{
    GridFarm gridFarm;

    public GameObject[] cropPrefabs;
    public int selectCrop = 0;
    public bool planting = false;   // planting crop mode
    public bool destroying = false; // destroy crop mode

    [Header("Cursors")]
    public Texture2D imgSeeds;
    public Texture2D imgShovel;
    CursorMode cursorMode = CursorMode.Auto;
    Vector2 hotSpot = Vector2.zero;
    Vector2 hotSpotShovel = new Vector2(10,80);

    public void ReceiveInput(Transform father, bool occupied)
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


    /// <summary>
    /// 
    /// </summary>
    /// <param name="father"></param>
    /// <param name="occupied"></param>
    /// <returns>0 = nothing, 1 = space filled, 2 = space cleared</returns>
    public int ReceiveInputGrid(Transform father, bool occupied)
    {
        if (planting)
        {
            if (!occupied)
            {
                PlantCrop(father);
                return 1;
            }
            else
            {
                UI_Messages.instance.ReceiveMessage("Area Ocupada");
                return 0;
            }

        }
        else if (destroying)
        {
            DestroyCrop(father);
            return 2;
        }
        return 0;
    }

    /// <summary>
    /// Plant crop in design space in grid
    /// </summary>
    /// <param name="father"></param>
    public void PlantCrop(Transform father)
    {
        //if (!planting)
        //    return; // break the planting function if it is not in planting mode

        GameObject newTree = Instantiate(cropPrefabs[selectCrop], father, false) as GameObject; // Instantiate tree prefab

        planting = false;
        Cursor.SetCursor(null, hotSpot, cursorMode);
    }

    public void DestroyCrop(Transform father)
    {
        //if (!destroying)
        //    return;
        if(father.childCount > 0)   // If there is a crop to destroy
            Destroy(father.GetChild(0).gameObject);

        destroying = false;
        Cursor.SetCursor(null, hotSpot, cursorMode);
    }

    public void SelectSeed(int numSeed)
    {
        destroying = false;

        int cost = cropPrefabs[numSeed].GetComponent<Farm>().so_farm.buildingCost;

        if (!GameManager.instance.CheckGold(cost))  // if there is not enouth gold, dont select seed
            return;

        selectCrop = numSeed;
        planting = true;

        // Check for enough gold

        GameManager.instance.LoseGold(cropPrefabs[selectCrop].GetComponent<Farm>().so_farm.buildingCost);


        Cursor.SetCursor(imgSeeds, hotSpot, cursorMode);
    }

    /// <summary>
    /// Click button to destroy crops
    /// 
    /// clicking it twice disable destroy mode
    /// </summary>
    public void SelectDestroy()
    {
        planting = false;

        if (!destroying)
        {
            destroying = !destroying;
            Cursor.SetCursor(imgShovel, hotSpotShovel, cursorMode);
        }
        else
        {
            destroying = !destroying;
            Cursor.SetCursor(null, hotSpot, cursorMode);
        }
        
    }

    [ContextMenu("Cursor")]
    private void OnBeforeTransformParentChanged()
    {
        Cursor.SetCursor(imgSeeds, hotSpot, cursorMode);
    }
}
