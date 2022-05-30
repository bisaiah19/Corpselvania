using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    private bool isReached;
    public SpriteRenderer checkpointrenderer;

    // Start is called before the first frame update
    void Start()
    {
        isReached = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isReached)
        {
            checkpointrenderer.color = Color.white;
        }
        if (!isReached)
        {
            checkpointrenderer.color = Color.gray;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isReached = true;
        }
    }
}
