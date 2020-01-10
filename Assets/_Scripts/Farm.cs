using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Farm : MonoBehaviour
{
    public SO_Farms so_farm;    // Scriptable Object with farm info


    [Header("Temp Vars")]


    public int t_numProducts = 0;

    

    [Header("Stats")]
    public CropState cropState = CropState.Empty;
    //Colheita

    float harvestTimeCurrent = 0;   // Save


    [Header("Level System")]
    public int currentLevel = 1;
    const int maxLevel = 3;



    [Header("UI Elements")]
    public Image barFill;
    public Button harvestButton;
    public TMP_Text buttonText;
    public Image treeImage;
    public TMP_Text levelText;


    public Transform[] fruits;
    [ContextMenu("Awake")]
    void Awake()
    {
        SetupTreeNFruits();
        
    }

    // Update is called once per frame
    void Update()
    {
        float growSpeed = 1 + ((float)currentLevel * so_farm.productionTimeImprov);


        if(cropState == CropState.Growing)
            harvestTimeCurrent += Time.deltaTime * growSpeed;


        FillBar();
        UpdBarText();
    }



    void SaveInfo()
    {

    }

    void LoadInfo()
    {

    }


    public void LevelUp()
    {
        if (currentLevel < maxLevel) {
            int cost = (currentLevel + 1) * so_farm.levelCost;

            if (!GameManager.instance.CheckGold(cost))  // if there is not enouth gold, dont select seed
                return;


            currentLevel++;

            GameManager.instance.LoseGold(cost);

            // Update level Text;
            if (currentLevel == maxLevel)
                levelText.text = "Lv. Max" ;
            else
                levelText.text = "Lv. " + currentLevel; 
        }
    }

    /// <summary>
    /// Make all Fruits get image from SO and color tree
    /// </summary>
    [ContextMenu("Setup Fruits")]
    void SetupTreeNFruits()
    {
        treeImage.color = so_farm.treeColor; // paint tree acording S.O.

        // Set Fruit sprites
        foreach (var item in fruits)
        {
            item.GetComponent<Image>().sprite = so_farm.product.sprite;
        }
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

                    FruitDance();
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
            cropState = CropState.Growing;
            Collect();

        }
    }


    /// <summary>
    /// Visual effect for when the tree is ready to harvest
    /// Fruits rodate side to side
    /// </summary>
    void FruitDance()
    {
        //calculate angle
        float multiValue = Mathf.Cos(Time.timeSinceLevelLoad * 3);

        const float angle = 10;

        float zAngle = angle * multiValue;

        foreach (var item in fruits)
        {
            item.localRotation = Quaternion.Euler(0, 0, zAngle);
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

        barFill.fillAmount = fillAmount;

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

        UI_Messages.instance.ReceiveMessage("Voce vendeu " + so_farm.colectQuantity + " " + so_farm.product.productName + " e faturou " + so_farm.colectQuantity * 10 + " ouros!");

        GameManager.instance.GainGold(so_farm.colectQuantity * 10);

        ResetBar();
    }

    public enum CropState
    {
        Empty,
        Growing,
        Done
    }
}
