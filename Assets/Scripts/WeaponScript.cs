using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public GameObject weapon;
    public GameObject slash;
    public GameObject player;
    public SpriteRenderer weaponrender;
    public Sprite knife;
    public Sprite sword;
    public Sprite battleaxe;
    public SpriteRenderer slashrender;
    public BoxCollider2D slashcollider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetWeaponSprite();
        if (PlayerMovement.direction == 0)
        {
            this.transform.position = new Vector2(player.transform.position.x + 32f, player.transform.position.y);
            if (Indicator.weaponObtained == "knife")
            {
                slash.transform.position = new Vector2(weapon.transform.position.x + .2f, weapon.transform.position.y);
                slash.transform.localScale = new Vector2(1.913876f, 1.913876f);
            }
            if (Indicator.weaponObtained == "sword")
            {
                slash.transform.position = new Vector2(weapon.transform.position.x + .3f, weapon.transform.position.y);
                slash.transform.localScale = new Vector2(2.5f, 2.5f);
            }
            if (Indicator.weaponObtained == "battleaxe")
            {
                slash.transform.position = new Vector2(weapon.transform.position.x + .35f, weapon.transform.position.y);
                slash.transform.localScale = new Vector2(3f, 3f);
            }
            slashrender.flipX = false;
        }
        if (PlayerMovement.direction == 1)
        {
            this.transform.position = new Vector2(player.transform.position.x + 30.5f, player.transform.position.y);
            slash.transform.position = new Vector2(weapon.transform.position.x - .2f, weapon.transform.position.y);
            slashrender.flipX = true;
        }

        if (!PlayerMovement.isAttacking)
        {
            if (PlayerMovement.direction == 0)
            {
                weapon.transform.rotation = Quaternion.Euler(0, 0, -44.33f);
            }
            if (PlayerMovement.direction == 1)
            {
                weapon.transform.rotation = Quaternion.Euler(0, 0, 54.128f);
            }
        }
        if (PlayerMovement.isAttacking)
        {
            if (PlayerMovement.direction == 0)
            {
                weapon.transform.rotation = Quaternion.Euler(0, 0, this.transform.rotation.z - 2f);
            }
            if (PlayerMovement.direction == 1)
            {
                weapon.transform.rotation = Quaternion.Euler(0, 0, this.transform.rotation.z + 2f);
            }
        }
        if (PlayerMovement.isAttacking == true && PlayerMovement.attackTime <= .15f)
        {
            slashrender.enabled = true;
            slashcollider.enabled = true;
        }
        if (PlayerMovement.isAttacking == false)
        {
            slashrender.enabled = false;
            slashcollider.enabled = false;
        }
    }

    private void SetWeaponSprite()
    {
        if (Indicator.weaponObtained == "none")
        {
            weaponrender.enabled = false;
        }
        if (Indicator.weaponObtained == "knife")
        {
            weaponrender.enabled = true;
            weaponrender.sprite = knife;
        }
        if (Indicator.weaponObtained == "sword")
        {
            weaponrender.enabled = true;
            weaponrender.sprite = sword;
        }
        if (Indicator.weaponObtained == "battleaxe")
        {
            weaponrender.enabled = true;
            weaponrender.sprite = battleaxe;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Totem")
        {

        }
    }
}
