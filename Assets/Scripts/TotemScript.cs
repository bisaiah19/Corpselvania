using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemScript : MonoBehaviour
{
    public bool isOriginal;
    public GameObject player;
    private float speed;
    public int health;

    // Start is called before the first frame update
    void Start()
    {
        speed = 3f;
        health = 25;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOriginal)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            if (health <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "MeleeSlash")
        {
            Debug.Log("Triggered");
            if (Indicator.weaponObtained == "none")
            {
                health -= 3;
            }
            if (Indicator.weaponObtained == "knife")
            {
                health -= 15;
            }
            if (Indicator.weaponObtained == "sword")
            {
                health -= 25;
            }
            if (Indicator.weaponObtained == "battleaxe")
            {
                health -= 35;
            }
        }
    }
    private void OnDestroy()
    {
        Indicator.gravescollected += 1;
    }
}
