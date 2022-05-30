using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    public ParticleSystem particles;
    public bool isOriginal;

    // Start is called before the first frame update
    void Start()
    {
        particles.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (particles.isStopped && !isOriginal)
        {
            Destroy(this.gameObject);
        }
    }
}
