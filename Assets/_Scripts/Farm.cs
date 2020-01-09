using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Farm : MonoBehaviour
{
    public SO_Farms so_farm;


    [Header("Temp Vars")]


    public int t_numProducts = 0;


    [Header("Stats")]

    //Colheita

    float harvestTimeTotal = 20;
    float harvestTimeCurrent = 0;
    


    [Header("Bar Progress")]
    public Image barFill;
    public Button harvestButton;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        harvestTimeCurrent += Time.deltaTime;
        FillBar();

    }


    void FillBar()
    {
        barFill.fillAmount = harvestTimeCurrent / harvestTimeTotal;

        if (harvestTimeCurrent / harvestTimeTotal > 1)
        {
            harvestButton.interactable = true;
        }
    }

    void ResetBar()
    {
        barFill.fillAmount = 0;
        harvestTimeCurrent = 0;

        harvestButton.interactable = false;
    }


    public void OnHover()
    {

    }

    public void Collect()
    {
        //Temp
        t_numProducts += so_farm.colectQuantity;


        //End Temp

        UI_Messages.instance.ReceiveMessage("Voce Coletou " + so_farm.colectQuantity + " " + so_farm.product.productName);

        ResetBar();
    }
}
