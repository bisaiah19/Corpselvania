using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    public float speed;
    private float restTime;

    public GameObject player;
    private float initPlayerX;
    private float initPlayerY;
    public bool isOriginal;
    private bool isReflected;

    private Vector3 direction;

    private AudioSource source;
    public AudioClip playerdamage;
    public AudioClip playerdead;
    // Start is called before the first frame update
    void Start()
    {
        restTime = 4f;
        isReflected = false;
        initPlayerX = player.transform.position.x;
        initPlayerY = player.transform.position.y;
        direction = (player.transform.position - this.transform.position).normalized;
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOriginal && (!Indicator.isOnPauseUI && !Indicator.isOnConverterUI && !Indicator.isOnCraftingTableUI))
        {
            restTime -= Time.deltaTime;
            if (isReflected)
            {
                this.transform.position -= direction * speed * Time.deltaTime;
            }
            if (!isReflected)
            {
                this.transform.position += direction * speed * Time.deltaTime;
            }
            if (restTime <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Indicator.health -= 10;
            source.PlayOneShot(playerdamage);
            Indicator.gotDamaged = true;
            Indicator.damageTime = 1f;
            if (Indicator.health <= 0)
            {
                source.PlayOneShot(playerdead);
                Indicator.hasDiedBefore = true;
            }
            Destroy(this.gameObject);
        }
        if (collision.gameObject.name == "MeleeSlash")
        {
            isReflected = true;
        }
        if (collision.gameObject.layer == 9)
        {
            Destroy(this.gameObject);
        }
    }
}
