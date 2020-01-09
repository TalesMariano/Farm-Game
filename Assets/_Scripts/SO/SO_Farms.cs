using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Farm_", menuName = "ScriptableObjects/SO_Farms", order = 1)]
public class SO_Farms : ScriptableObject
{
    public string farmName;
    public SO_Product product;

    public int level = 1; // how good it is

    [Header ("Stats")]
    public int productionTime;          // How much time in seconds it take to produce
    public int colectTime;              // how long in secs it take to collect the product
    public int colectQuantity = 1;      // How many products are aquirede when colect

    
    
        
}
