using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public string type;
    public Text myText;

    private void Update()
    {
        if (type == "knifetext" && Indicator.weaponObtained == "none")
        {
            myText.text = "1: Obtain knife\n" + Indicator.weaponcost;
        }
        if ((type == "swordtext" || type == "knifetext" || type == "battleaxetext") && Indicator.weaponObtained == "knife")
        {
            type = "swordtext";
        }
        if (type == "swordtext")
        {
            myText.text = "1: Obtain sword\n" + Indicator.weaponcost;
        }
        if ((type == "swordtext" || type == "knifetext" || type == "battleaxetext") && Indicator.weaponObtained == "sword")
        {
            type = "battleaxetext";
        }
        if (type == "battleaxetext")
        {
            myText.text = "1: Obtain battleaxe\n" + Indicator.weaponcost;
        }
        if ((type == "swordtext" || type == "knifetext" || type == "battleaxetext") && Indicator.weaponObtained == "battleaxe")
        {
            type = "maxweapontext";
        }
        if (type == "maxweapontext")
        {
            myText.text = "Max tier reached";
        }
        if (type == "healthpotiontext")
        {
            myText.text = "2: Obtain health potion\n Gives you +15 Max HP\n" + Indicator.healthcost;
        }
        if (type == "staminapotiontext")
        {
            myText.text = "3: Obtain health potion\n Gives you +15 Max HP\n" + Indicator.staminacost;
        }
    }
    private void OnMouseEnter()
    {
        if (type == "startgame" || type == "settings" || type == "exitgame" || type == "backtotitle")
        {
            myText.color = Color.yellow;
        }
    }

    private void OnMouseExit()
    {
        if (type == "startgame" || type == "settings" || type == "exitgame" || type == "backtotitle")
        {
            myText.color = Color.white;
        }
    }

    private void OnMouseDown()
    {
        if (type == "startgame")
        {
            SceneManager.LoadScene("TheGraveyardBeginning");
            Indicator.sceneLevel = 0;
        }
        if (type == "exitgame")
        {
            Application.Quit();
        }
        if (type == "backtotitle")
        {
            Indicator.health = 35;
            Indicator.maxhealth = 35;
            Indicator.stamina = 35;
            Indicator.maxstamina = 35;
            Indicator.gravestone = 0;
            Indicator.gravescollected = 0;
            Indicator.graveliquid = 0;
            Indicator.checkpointLocation = "Checkpoint0";

            Indicator.weaponcost = 10;
            Indicator.weaponObtained = "none";
            Indicator.healthcost = 5;
            Indicator.healthlevel = 0;
            Indicator.staminacost = 5;
            Indicator.staminalevel = 0;

            Indicator.hasDiedBefore = false;

            Indicator.resetLevelTime = 2;
            Indicator.sceneLevel = -1;
            SceneManager.LoadScene("TitleScreen");
        }
    }
}
