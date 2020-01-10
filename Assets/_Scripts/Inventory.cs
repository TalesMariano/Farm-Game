using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Inventory : MonoBehaviour
{

    public SO_Product[] products;  // temporary type

    public int[] numThings;

    public string textInv;

    public TMP_Text text;


    [ContextMenu("Fill Text")]
    void FillText()
    {
        textInv = "";

        for (int i = 0; i < products.Length; i++)
        {
            textInv += products[i].productName + " x " + numThings[i] + "\n";
        }

        PrintText();
    }


    void PrintText()
    {
        text.text = textInv;

    }
}
