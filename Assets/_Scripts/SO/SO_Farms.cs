using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Farm_", menuName = "ScriptableObjects/SO_Farms", order = 1)]
public class SO_Farms : ScriptableObject
{
    public string farmName;
    public SO_Product product;

    public Color treeColor = Color.white;   //Tree color tint

    [Header ("Stats")]
    public int productionTime;          // How much time in seconds it take to produce
    public float productionTimeImprov = 0.3f;  // How much time it improve



    public int buildingCost;            // How much time it cost to build/plant farm
    public int levelCost;               // How much gold it cost to level up

    public int colectTime;              // how long in secs it take to collect the product
    public int colectQuantity = 1;      // How many products are aquirede when colect
    public float colectQuantityImprov = 0.3f;  // How much time it improve




}
