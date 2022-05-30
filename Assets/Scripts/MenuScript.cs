using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public string type;
    public Text myText;

    public void Start()
    {
        if (type == "knifetext" && Indicator.weaponObtained == "knife")
        {
            type = "swordtext";
        }
        if (type == "swordtext" && Indicator.weaponObtained == "sword")
        {
            type = "battleaxetext";
        }
        if (type == "battleaxetext" && Indicator.weaponObtained == "battleaxe")
        {
            type = "maxweapontext";
        }
        if ((type == "knifetext" || type == "swordtext" || type == "battleaxetext") && (Indicator.weaponObtained == "pistol" || Indicator.weaponObtained == "rifle" || Indicator.weaponObtained == "slugger"))
        {
            type = "shieldtext";
        }
        if ((type == "pistoltext" || type == "rifletext" || type == "sluggertext") && (Indicator.weaponObtained == "knife" || Indicator.weaponObtained == "sword" || Indicator.weaponObtained == "battleaxe"))
        {
            type = "shieldtext";
        }
        if (type == "pistoltext" && Indicator.weaponObtained == "pistol")
        {
            type = "rifletext";
        }
        if (type == "rifletext" && Indicator.weaponObtained == "rifle")
        {
            type = "sluggertext";
        }
        if (type == "sluggertext" && Indicator.weaponObtained == "slugger")
        {
            type = "maxweapontext";
        }
    }
    private void OnMouseEnter()
    {
        if (type == "startgame" || type == "settings" || type == "exitgame" || type == "converterpiecetext" || type == "converterliquidtext" || type == "converterbacktext"
            || type == "knifetext" || type == "swordtext" || type == "battleaxetext" || type == "healthpotiontext" || type == "staminapotiontext" || type == "craftingtablebacktext")
        {
            myText.color = Color.yellow;
        }
    }

    private void OnMouseExit()
    {
        if (type == "startgame" || type == "settings" || type == "exitgame" || type == "converterpiecetext" || type == "converterliquidtext" || type == "converterbacktext"
            || type == "knifetext" || type == "swordtext" || type == "battleaxetext" || type == "healthpotiontext" || type == "staminapotiontext" || type == "craftingtablebacktext")
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
        if (type == "converterpiecetext")
        {
            if (Indicator.gravescollected >= 1)
            {
                Indicator.gravestone += 16;
                Indicator.gravescollected -= 1;
            }
        }
        if (type == "converterliquidtext")
        {
            if (Indicator.gravestone >= 6)
            {
                Indicator.graveliquid += 5;
                Indicator.gravestone -= 6;
            }
        }
        if (type == "converterbacktext")
        {
            Indicator.isOnConverterUI = false;
        }
        if (type == "knifetext")
        {
            if (Indicator.gravestone >= Indicator.weaponcost)
            {
                Indicator.weaponObtained = "knife";
                Indicator.gravescollected -= Indicator.weaponcost;
                Indicator.weaponcost += 5;
            }
        }
        if (type == "swordtext")
        {
            if (Indicator.gravestone >= Indicator.weaponcost)
            {
                Indicator.weaponObtained = "sword";
                Indicator.gravescollected -= Indicator.weaponcost;
                Indicator.weaponcost += 5;
            }
        }
        if (type == "battleaxetext")
        {
            if (Indicator.gravestone >= Indicator.weaponcost)
            {
                Indicator.weaponObtained = "battleaxe";
                Indicator.gravescollected -= Indicator.weaponcost;
                Indicator.weaponcost += 5;
            }
        }
        if (type == "healthpotiontext")
        {
            if (Indicator.graveliquid >= Indicator.healthcost && Indicator.healthlevel < 5)
            {
                Indicator.healthlevel += 1;
                Indicator.maxhealth += 15;
                Indicator.health += 15;
                Indicator.graveliquid -= Indicator.healthcost;
                Indicator.healthcost += 3;
            }
        }
        if (type == "staminapotiontext")
        {
            if (Indicator.graveliquid >= Indicator.staminacost && Indicator.staminalevel < 4)
            {
                Indicator.staminalevel += 1;
                Indicator.maxstamina += 15;
                Indicator.graveliquid -= Indicator.staminacost;
                Indicator.staminacost += 3;
            }
        }
        if (type == "craftingtablebacktext")
        {
            Indicator.isOnCraftingTableUI = false;
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
