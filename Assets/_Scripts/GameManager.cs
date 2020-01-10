using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public int currentGold = 0;


    public static GameManager instance;

    [Header("Ui Elements")]
    public TMP_Text goldText;


    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        goldText.text = "Ouro = " + currentGold;

        if (Input.GetKeyDown(KeyCode.P))
        {
            GainGold(100);
        }
    }


    public void LoseGold(int numGold)
    {
        currentGold -= numGold;
    }

    public void GainGold(int numGold)
    {
        currentGold += numGold;
    }



    public bool CheckGold(int numGold)
    {
        if(numGold<= currentGold)
        {
            return true;
        }
        else
        {
            UI_Messages.instance.ReceiveMessage("Sem Ouro Suficiente");
            return false;
        }
    }
}
