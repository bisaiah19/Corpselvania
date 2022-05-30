using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public int enemyhealth;
    private float timeToJump;
    public float rangedAttack;
    private float whenToAttack;
    private bool canJump;
    public bool isRanged;
    private bool isAtBarrier;
    private bool isAtLeftBarrier;

    public Transform playerpos;
    public GameObject bullet;
    private Rigidbody2D rb;
    private BoxCollider2D collision;
    private Animator anim;
    private float directX = 0f;
    private SpriteRenderer spriterend;
    public PlayerDetector detector;
    public PlayerDetector closedetector;

    public SpriteRenderer head;
    public SpriteRenderer body;
    public SpriteRenderer arm1;
    public SpriteRenderer arm2;
    public SpriteRenderer leg1;
    public SpriteRenderer leg2;

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

    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private LayerMask jumpableGround2;
    [SerializeField] private float movingspeed = 3f;
    [SerializeField] private float jumpforce = 8f;
    private Vector2 direction;

    private float deathtransparency;
    private AudioSource source;
    public AudioClip shoot;
    public AudioClip damaged;
    public AudioClip playerdamage;
    public AudioClip playerdead;
    public ParticleSystem particles;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collision = GetComponent<BoxCollider2D>();
        spriterend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        deathtransparency = 0;
        timeToJump = 15f;
        whenToAttack = rangedAttack;
        canJump = false;
        isAtBarrier = false;
        isAtLeftBarrier = false;
        direction = (this.transform.position - playerpos.transform.position).normalized;
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!Indicator.isOnPauseUI && !Indicator.isOnConverterUI && !Indicator.isOnCraftingTableUI)
        {
            if (playerpos.transform.position.x < this.transform.position.x && detector.canSeePlayer)
            {
                if (!isRanged)
                {
                    directX = -1f;
                }
                if (isRanged)
                {
                    if (!closedetector.canSeePlayer)
                    {
                        directX = -1f;
                    }
                    if (closedetector.canSeePlayer)
                    {
                        directX = 1f;
                    }
                }
            }
            if (playerpos.transform.position.x > this.transform.position.x && detector.canSeePlayer)
            {
                if (!isRanged)
                {
                    directX = 1f;
                }
                if (isRanged)
                {
                    if (!closedetector.canSeePlayer)
                    {
                        directX = 1f;
                    }
                    if (closedetector.canSeePlayer)
                    {
                        directX = -1f;
                    }
                }
            }
            rb.velocity = new Vector2(directX * movingspeed, rb.velocity.y);
            whenToAttack -= Time.deltaTime;
            if (whenToAttack <= 0 && isRanged && enemyhealth > 0)
            {
                source.PlayOneShot(shoot);
                var bulletobj = Instantiate(bullet, (new Vector3(transform.position.x, transform.position.y + .5f, transform.position.z)), Quaternion.identity);
                bulletobj.gameObject.GetComponent<EnemyBulletScript>().isOriginal = false;
                bulletobj.gameObject.GetComponent<EnemyBulletScript>().speed = 12f;

                whenToAttack = rangedAttack;
            }
            timeToJump -= Time.deltaTime;
            if (timeToJump <= 0)
            {
                canJump = true;
                timeToJump = 15f;
            }
            if (canJump && isOnGround() && detector.canSeePlayer)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpforce);
                canJump = false;
            }
            Animate();
            if (transform.position.y <= -20)
            {
                enemyhealth = 0;
            }
            if (enemyhealth <= 0)
            {
                Die();
            }
        }
    }

    private void Animate()
    {
        if (enemyhealth > 0)
        {
            if (directX > 0f)
            {
                state = MovementState.running;
                head.flipX = false;
                body.flipX = false;
                arm1.sortingOrder = 1;
                arm2.sortingOrder = -1;
            }
            else if (directX < 0f)
            {
                state = MovementState.running;
                head.flipX = true;
                body.flipX = true;
                arm1.sortingOrder = -1;
                arm2.sortingOrder = 1;
            }
            else
            {
                state = MovementState.idle;
            }

            if (rb.velocity.y > .1f)
            {
                state = MovementState.jumping;
            }
            else if (rb.velocity.y < -.1f)
            {
                state = MovementState.falling;
            }
        }
        else if (enemyhealth <= 0)
        {
            state = MovementState.dead;
        }


        anim.SetInteger("state", (int)state);
    }

    private bool isOnGround()
    {
        return Physics2D.BoxCast(collision.bounds.center, collision.bounds.size, 0f, Vector2.down, .1f, jumpableGround, jumpableGround2);
    }

    private void Die()
    {
        movingspeed = 0f;
        jumpforce = 0f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "MeleeSlash")
        {
            if (Indicator.weaponObtained == "none")
            {
                enemyhealth -= 3;
                var particle = Instantiate(particles, (new Vector3(transform.position.x, transform.position.y + .2f, transform.position.z)), Quaternion.identity);
                particle.gameObject.GetComponent<ParticleScript>().isOriginal = false;
                source.PlayOneShot(damaged);
            }
            if (Indicator.weaponObtained == "knife")
            {
                enemyhealth -= 15;
                var particle = Instantiate(particles, (new Vector3(transform.position.x, transform.position.y + .2f, transform.position.z)), Quaternion.identity);
                particle.gameObject.GetComponent<ParticleScript>().isOriginal = false;
                source.PlayOneShot(damaged);
            }
            if (Indicator.weaponObtained == "sword")
            {
                enemyhealth -= 25;
                var particle = Instantiate(particles, (new Vector3(transform.position.x, transform.position.y + .2f, transform.position.z)), Quaternion.identity);
                particle.gameObject.GetComponent<ParticleScript>().isOriginal = false;
                source.PlayOneShot(damaged);
            }
            if (Indicator.weaponObtained == "battleaxe")
            {
                enemyhealth -= 40;
                var particle = Instantiate(particles, (new Vector3(transform.position.x, transform.position.y + .2f, transform.position.z)), Quaternion.identity);
                particle.gameObject.GetComponent<ParticleScript>().isOriginal = false;
                source.PlayOneShot(damaged);
            }
            this.transform.Translate(direction * .5f);
        }
        if (collision.gameObject.tag == "Player" && !Indicator.gotDamaged && enemyhealth > 0)
        {
            Indicator.health -= 25;
            var particle = Instantiate(particles, (new Vector3(playerpos.position.x, playerpos.position.y + .2f, playerpos.position.z)), Quaternion.identity);
            particle.gameObject.GetComponent<ParticleScript>().isOriginal = false;
            source.PlayOneShot(playerdamage);
            Indicator.gotDamaged = true;
            Indicator.damageTime = 1f;
            if (Indicator.health <= 0)
            {
                source.PlayOneShot(playerdead);
                Indicator.hasDiedBefore = true;
            }
        }
    }
}