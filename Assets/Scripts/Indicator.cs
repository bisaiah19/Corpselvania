using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Indicator : MonoBehaviour
{
    public static int health;
    public static int maxhealth;
    public static float stamina;
    public static float maxstamina;
    public static int gravestone;
    public static int gravescollected;
    public static int graveliquid;
    public static string checkpointLocation;

    public static bool isOnConverterUI;
    public static bool isOnCraftingTableUI;
    public static bool isOnPauseUI;
    public static int weaponcost;
    public static string weaponObtained;
    public static int healthcost;
    public static int healthlevel;
    public static int staminacost;
    public static int staminalevel;

    public static float damageTime;
    public static bool gotDamaged;
    public static float resetLevelTime;
    public static int sceneLevel;
    public static float deathX;
    public static float deathY;
    public static bool hasDiedBefore;

    public GameObject player;

    private AudioSource source;
    public AudioClip music1;
    // Start is called before the first frame update
    void Start()
    {
        health = 35;
        maxhealth = 35;
        stamina = 35;
        maxstamina = 35;
        gravestone = 0;
        gravescollected = 0;
        graveliquid = 0;
        checkpointLocation = "Checkpoint0";

        isOnConverterUI = false;
        isOnCraftingTableUI = false;
        isOnPauseUI = false;
        weaponcost = 10;
        weaponObtained = "none";
        healthcost = 5;
        healthlevel = 0;
        staminacost = 5;
        staminalevel = 0;

        damageTime = 0;
        gotDamaged = false;
        hasDiedBefore = false;

        resetLevelTime = 2;
        sceneLevel = -1;
        deathX = 0;
        deathY = 0;

        source = GetComponent<AudioSource>();
        if (source.isPlaying == false)
        {
            source.PlayOneShot(music1);
        }
    }
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (sceneLevel == 0 || sceneLevel == 1)
            { 
                isOnPauseUI = true; 
            }
            
        }
        if (source.isPlaying == false)
        {
            source.PlayOneShot(music1);
        }
        if (stamina <= maxstamina)
        {
            stamina += Time.deltaTime * (4 + (Indicator.staminalevel*3f));
        }
        else if (stamina >= maxstamina)
        {
            stamina = maxstamina;
        }

        if (health < 0)
        {
            health = 0;
        }

        if (gotDamaged)
        {
            damageTime -= Time.deltaTime;
        }
        if (damageTime <= 0)
        {
            gotDamaged = false;
        }
    }
}
