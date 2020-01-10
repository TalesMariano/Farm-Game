using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Product_", menuName = "ScriptableObjects/SO_Product", order = 1)]
public class SO_Product : ScriptableObject
{
    public string productName;

    public Sprite sprite;

    public int value;
}
