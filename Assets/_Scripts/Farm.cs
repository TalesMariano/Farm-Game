using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Farm : MonoBehaviour
{
    public SO_Farms so_farm;


    [Header("Temp Vars")]


    public int t_numProducts = 0;

    public CropState cropState = CropState.Empty;

    [Header("Stats")]

    //Colheita

    float harvestTimeCurrent = 0;
    


    [Header("Bar Progress")]
    public Image barFill;
    public Button harvestButton;
    public TMP_Text buttonText;


    public Transform[] fruits;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(cropState == CropState.Growing)
            harvestTimeCurrent += Time.deltaTime;


        FillBar();
        UpdBarText();
    }


    void UpdBarText()
    {
        switch (cropState)
        {
            case CropState.Empty:
            {
                buttonText.text = "Plantar";
                    harvestButton.interactable = true;

                    break;
            }

            case CropState.Growing:
            {
                buttonText.text = "Crescendo";
                    harvestButton.interactable = false;
                    break;
            }

            case CropState.Done:
            {
                buttonText.text = "Colher!";
                break;
            }
        }
    }


    public void ButtonPress()
    {
        if(cropState == CropState.Empty)
        {
            cropState = CropState.Growing;

        }

        if (cropState == CropState.Done)
        {
            cropState = CropState.Empty;
            Collect();

        }
    }

    void OnGrowComplete()
    {
        harvestButton.interactable = true;
        cropState = CropState.Done;

    }


    void FillBar()
    {
        float fillAmount = harvestTimeCurrent / so_farm.productionTime;

        fillAmount = Mathf.Clamp01(fillAmount);

        //Temp
        foreach (var item in fruits)
        {
            item.localScale = Vector3.one * fillAmount;
        }

        if (fillAmount >= 1) // Caso já se tenha treminado de crescer
        {
            OnGrowComplete();
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

    public enum CropState
    {
        Empty,
        Growing,
        Done
    }
}
