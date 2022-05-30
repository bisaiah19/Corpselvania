using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    public string type;
    public Text myText;
    public RectTransform textPos;
    public GameObject uiobject;
    public SpriteRenderer spritemeter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (type == "healthamt")
        {
            myText.text = Indicator.health.ToString();
            textPos.localPosition = new Vector2(-327.5f + (3.12285714286f * Indicator.maxhealth), -225);
        }
        if (type == "energyamt")
        {
            myText.text = Mathf.RoundToInt(Indicator.stamina).ToString();
            textPos.localPosition = new Vector2(-327.5f + (3.12285714286f * Indicator.maxstamina), -189);
        }
        if (type == "gravestoneamt")
        {
            myText.text = "x " + Indicator.gravestone.ToString();
        }
        if (type == "totemamt")
        {
            myText.text = "x " + Indicator.gravescollected.ToString();
        }
        if (type == "liquidamt")
        {
            myText.text = "x " + Indicator.graveliquid.ToString();
        }
        if (type == "healthmeterfill")
        {
            uiobject.transform.localScale = new Vector3(2.952f * Indicator.health, 19.74525f, 48f);
            if (Indicator.health <= Indicator.maxhealth / 4)
            {
                spritemeter.color = Color.red;
            }
            if (Indicator.health <= Indicator.maxhealth / 2 && Indicator.health > Indicator.maxhealth / 4)
            {
                spritemeter.color = Color.yellow;
            }
            if (Indicator.health > Indicator.maxhealth / 2)
            {
                spritemeter.color = Color.green;
            }
        }
        if (type == "energymeterfill")
        {
            uiobject.transform.localScale = new Vector3(2.952f * Indicator.stamina, 19.74525f, 48f);
            if (Indicator.stamina <= Indicator.maxstamina / 4)
            {
                spritemeter.color = Color.red;
            }
            if (Indicator.stamina <= Indicator.maxstamina / 2 && Indicator.stamina > Indicator.maxstamina / 4)
            {
                spritemeter.color = Color.yellow;
            }
            if (Indicator.stamina > Indicator.maxstamina / 2)
            {
                spritemeter.color = Color.green;
            }
        }
        if (type == "healthmeter")
        {
            spritemeter.size = new Vector2(.02098105714f * Indicator.maxhealth, 0.1441703f);
        }
        if (type == "energymeter")
        {
            spritemeter.size = new Vector2(.02098105714f * Indicator.maxstamina, 0.1441703f);
        }
        if (type == "converterui")
        {
            if (Indicator.isOnConverterUI)
            {
                uiobject.SetActive(true);
            }
            else if (!Indicator.isOnConverterUI)
            {
                uiobject.SetActive(false);
            }
        }
        if (type == "craftingtableui")
        {
            if (Indicator.isOnCraftingTableUI)
            {
                uiobject.SetActive(true);
            }
            else if (!Indicator.isOnCraftingTableUI)
            {
                uiobject.SetActive(false);
            }
        }

        if (type == "interactui")
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (Indicator.isOnCraftingTableUI)
                {
                    if (Indicator.gravestone >= Indicator.weaponcost)
                    {
                        Indicator.weaponObtained = "knife";
                        Indicator.gravescollected -= Indicator.weaponcost;
                        Indicator.weaponcost += 5;
                    }
                }
                if (Indicator.isOnConverterUI)
                {
                    if (Indicator.gravescollected >= 1)
                    {
                        Indicator.gravestone += 16;
                        Indicator.gravescollected -= 1;
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (Indicator.isOnCraftingTableUI)
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
                if (Indicator.isOnConverterUI)
                {
                    if (Indicator.gravestone >= 6)
                    {
                        Indicator.graveliquid += 5;
                        Indicator.gravestone -= 6;
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (Indicator.isOnCraftingTableUI)
                {
                    if (Indicator.graveliquid >= Indicator.staminacost && Indicator.staminalevel < 4)
                    {
                        Indicator.staminalevel += 1;
                        Indicator.maxstamina += 15;
                        Indicator.graveliquid -= Indicator.staminacost;
                        Indicator.staminacost += 3;
                    }
                }
                if (Indicator.isOnConverterUI)
                {
                    Indicator.isOnConverterUI = false;
                }
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                if (Indicator.isOnCraftingTableUI)
                {
                    Indicator.isOnCraftingTableUI = false;
                }
            }
        }
        
    }
}
