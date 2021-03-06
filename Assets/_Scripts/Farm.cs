﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Farm : MonoBehaviour
{
    public SO_Farms so_farm;    // Scriptable Object with farm info

    [Header("Stats")]
    public CropState cropState = CropState.Empty;

    float harvestTimeCurrent = 0;   /// current time of fruit grown


    [Header("Level System")]
    public int currentLevel = 0;    // Current level of tree, the bigger the level better the tree performs
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

    void Update()
    {
        float growSpeed = 1 + ((float)currentLevel * so_farm.productionTimeImprov); // How fast the fruit grow, based on tree level

        if(cropState == CropState.Growing)      // Only grow fruit if tree is the growing stage
            harvestTimeCurrent += Time.deltaTime * growSpeed;


        FillBar();
        UpdBarText();
    }


    public void LevelUp()
    {
        if (currentLevel < maxLevel) {
            int cost = (currentLevel + 1) * so_farm.levelCost;  // Calculate the cost in gold of leveling up

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
    /// Fruits rotate side to side
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

    /// <summary>
    /// Fill status bar and scale fruit for visual feedback
    /// </summary>
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

        foreach (var item in fruits)
        {
            item.localRotation = Quaternion.Euler(0, 0, 0);
        }

        harvestButton.interactable = false;
    }

    /// <summary>
    /// Collect fruit and reward the player based on fruit ammount and value
    /// </summary>
    public void Collect()
    {
        int collectQuantity = Mathf.FloorToInt(so_farm.colectQuantity *  (1+ (currentLevel * so_farm.colectQuantityImprov))); 
        int collectReward = collectQuantity * so_farm.product.value;

        UI_Messages.instance.ReceiveMessage("Voce vendeu " + collectQuantity + " " + so_farm.product.productName + " e faturou " + collectReward + " ouros!");

        GameManager.instance.GainGold(collectReward);

        ResetBar();
    }

    public enum CropState
    {
        Empty,
        Growing,    // fruit growing
        Done        // Fruit ready to harvest
    }
}
