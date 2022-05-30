using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScript : MonoBehaviour
{
    private float duration;
    // Start is called before the first frame update
    void Start()
    {
        duration = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        duration -= Time.deltaTime;
        if (duration <= 0)
        {
            SceneManager.LoadScene("TitleScreen");
        }
    }
}
