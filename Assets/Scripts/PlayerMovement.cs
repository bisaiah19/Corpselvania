using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D collision;
    private Animator anim;
    private float directX = 0f;
    private SpriteRenderer spriterend;

    public SpriteRenderer peterhead;
    public SpriteRenderer peterbody;
    public SpriteRenderer peterarm1;
    public SpriteRenderer peterarm2;
    public SpriteRenderer peterleg1;
    public SpriteRenderer peterleg2;

    [SerializeField] private BoxCollider2D headbc;
    [SerializeField] private BoxCollider2D bodybc;
    [SerializeField] private BoxCollider2D arm1bc;
    [SerializeField] private BoxCollider2D arm2bc;
    [SerializeField] private BoxCollider2D leg1bc;
    [SerializeField] private BoxCollider2D leg2bc;
    [SerializeField] private Rigidbody2D headrb;
    [SerializeField] private Rigidbody2D bodyrb;
    [SerializeField] private Rigidbody2D arm1rb;
    [SerializeField] private Rigidbody2D arm2rb;
    [SerializeField] private Rigidbody2D leg1rb;
    [SerializeField] private Rigidbody2D leg2rb;
    [SerializeField] private SpriteRenderer deathcolor;
    

    private enum MovementState { idle, running, jumping, falling, attacking, attackingLeft, dead };
    private MovementState state = MovementState.idle;
    [SerializeField] public static bool isAttacking = false;
    public static float attackTime = 0f;
    public static int direction = 0;

    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private float movingspeed = 6f;
    [SerializeField] private float jumpforce = 8f;

    private float damageTime = 0f;

    private float deathtransparency;

    public GameObject totem;

    private AudioSource source;
    public AudioClip slash;
    public AudioClip swing;
    public AudioClip jump;
    public AudioClip damaged;
    public AudioClip death;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collision = GetComponent<BoxCollider2D>();
        spriterend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        deathtransparency = 0;
        Indicator.isOnConverterUI = false;
        Indicator.isOnCraftingTableUI = false;
        Indicator.isOnPauseUI = false;
        Indicator.resetLevelTime = 2;
        if (Indicator.hasDiedBefore)
        {
            Indicator.gravescollected -= 1;
        }
        if (Indicator.checkpointLocation == "Checkpoint1")
        {
            this.transform.position = new Vector2(40.42f, -2.4f);
        }
        if (Indicator.checkpointLocation == "Checkpoint2")
        {
            this.transform.position = new Vector2(40.42f, -2.4f);
        }
        if (Indicator.checkpointLocation == "Checkpoint3")
        {
            this.transform.position = new Vector2(77.21f, -2.4f);
        }
        if (Indicator.hasDiedBefore)
        {
            var totemspawn = Instantiate(totem, new Vector3(Indicator.deathX, Indicator.deathY, 0), Quaternion.identity);
            totemspawn.GetComponent<TotemScript>().isOriginal = false;
        }
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!Indicator.isOnPauseUI && !Indicator.isOnConverterUI && !Indicator.isOnCraftingTableUI)
        {
            directX = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(directX * movingspeed, rb.velocity.y);
            if (Input.GetButtonDown("Jump") && isOnGround())
            {
                source.PlayOneShot(jump);
                rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            }
            Animate();
            if (transform.position.y <= -20)
            {
                Indicator.health = 0;
            }
            if (Indicator.health <= 0)
            {
                Die();
            }
        }
    }

    private void Animate()
    {
        if (Indicator.health > 0)
        {
            if (directX > 0f)
            {
                if (!isAttacking)
                {
                    state = MovementState.running;
                }
                else if (isAttacking)
                {
                    state = MovementState.attacking;
                }
                direction = 0;
                peterhead.flipX = false;
                peterbody.flipX = false;
                peterarm1.sortingOrder = 1;
                peterarm2.sortingOrder = -1;
            }
            else if (directX < 0f)
            {
                if (!isAttacking)
                {
                    state = MovementState.running;
                }
                else if (isAttacking)
                {
                    state = MovementState.attackingLeft;
                }
                direction = 1;
                peterhead.flipX = true;
                peterbody.flipX = true;
                peterarm1.sortingOrder = -1;
                peterarm2.sortingOrder = 1;
            }
            else
            {
                if (!isAttacking)
                {
                    state = MovementState.idle;
                }
                else if (isAttacking)
                {
                    if (direction == 0)
                    {
                        state = MovementState.attacking;
                    }
                    if (direction == 1)
                    {
                        state = MovementState.attackingLeft;
                    }
                }
            }

            if (rb.velocity.y > .1f)
            {
                if (!isAttacking)
                {
                    state = MovementState.jumping;
                }
                else if (isAttacking)
                {
                    if (directX > 0f)
                    {
                        state = MovementState.attacking;
                    }
                    else if (directX < 0f)
                    {
                        state = MovementState.attackingLeft;
                    }
                }
            }
            else if (rb.velocity.y < -.1f)
            {
                if (!isAttacking)
                {
                    state = MovementState.falling;
                }
                else if (isAttacking)
                {
                    if (directX > 0f)
                    {
                        state = MovementState.attacking;
                    }
                    else if (directX < 0f)
                    {
                        state = MovementState.attackingLeft;
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Z) && Indicator.stamina >= 0)
            {
                isAttacking = true;
                if (Indicator.weaponObtained == "none")
                {
                    source.PlayOneShot(swing);
                    Indicator.stamina -= 7;
                }
                if (Indicator.weaponObtained == "knife")
                {
                    source.PlayOneShot(slash);
                    Indicator.stamina -= 15;
                }
                if (Indicator.weaponObtained == "sword")
                {
                    source.PlayOneShot(slash);
                    Indicator.stamina -= 37;
                }
                if (Indicator.weaponObtained == "battleaxe")
                {
                    source.PlayOneShot(slash);
                    Indicator.stamina -= 50;
                }
                attackTime = .3f;
            }
            if (attackTime >= -.5f)
            {
                attackTime -= Time.deltaTime;
            }
            if (isAttacking == true && attackTime <= 0f)
            {
                isAttacking = false;
            }
        }
        else if (Indicator.health <= 0)
        {
            state = MovementState.dead;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool isOnGround()
    {
        return Physics2D.BoxCast(collision.bounds.center, collision.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private void Die()
    {
        movingspeed = 0f;
        jumpforce = 0f;
        Indicator.hasDiedBefore = true;
        ReloadScene();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            source.PlayOneShot(damaged);
            if (Indicator.health <= 0)
            {
                source.PlayOneShot(death);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Converter" && Input.GetKeyDown(KeyCode.C))
        {
            Indicator.isOnConverterUI = true;
        }
        if (collision.gameObject.tag == "CraftingTable" && Input.GetKeyDown(KeyCode.C))
        {
            Indicator.isOnCraftingTableUI = true;
        }
        if (collision.gameObject.tag == "Door" && Input.GetKeyDown(KeyCode.C) && Indicator.sceneLevel == 1)
        {
            Indicator.hasDiedBefore = false;
            Indicator.sceneLevel = 2;
            Indicator.checkpointLocation = "Checkpoint0";
            SceneManager.LoadScene("Ending");
        }
        if (collision.gameObject.tag == "Door" && Input.GetKeyDown(KeyCode.C) && Indicator.sceneLevel == 0)
        {
            Indicator.hasDiedBefore = false;
            Indicator.sceneLevel = 1;
            Indicator.checkpointLocation = "Checkpoint0";
            SceneManager.LoadScene("TheGraveyardMiddle");
        }
        if (collision.gameObject.name == "Checkpoint1")
        {
            Indicator.checkpointLocation = "Checkpoint1";
        }
        if (collision.gameObject.name == "Checkpoint2")
        {
            Indicator.checkpointLocation = "Checkpoint2";
        }
        if (collision.gameObject.name == "Checkpoint3")
        {
            Indicator.checkpointLocation = "Checkpoint3";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Converter" && Input.GetKeyDown(KeyCode.C))
        {
            Indicator.isOnConverterUI = false;
        }
        if (collision.gameObject.tag == "CraftingTable" && Input.GetKeyDown(KeyCode.C))
        {
            Indicator.isOnCraftingTableUI = false;
        }
    }

    private void ReloadScene()
    {
        if (Indicator.health <= 0)
        {
            Indicator.resetLevelTime -= Time.deltaTime;
            if (Indicator.resetLevelTime <= 0)
            {
                Indicator.deathX = this.transform.position.x;
                Indicator.deathY = this.transform.position.y;
                Indicator.health = Indicator.maxhealth;
                if (Indicator.sceneLevel == 0)
                {
                    SceneManager.LoadScene("TheGraveyardBeginning");
                }
                if (Indicator.sceneLevel == 1)
                {
                    SceneManager.LoadScene("TheGraveyardMiddle");
                }
            }
        }
    }
}