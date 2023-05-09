using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Timer
    [HideInInspector]
    public float currentTime = 120.0f;
    float minTime = 0.0f;

    //TEXT
    public TMP_Text timeText;
    public GameObject player;

    bool isDead = false;

    // Key
    [HideInInspector]
    public bool hasKey = false;
    public Image keyImage;

    // Game condition
    public TMP_Text gameoverText;
    public TMP_Text clearText;
    public TMP_Text keyCheckerText;

    private void Awake()
    {
        currentTime = 120.0f;
        isDead = false;
    }

    void Start()
    {
    }


    void Update()
    {
        if (!player.gameObject.GetComponent<PlayerMovement>().isAlive) {return; }

        //Check Key
        if(hasKey)
        { 
            keyImage.material.color = Color.green;
        }
        else
        {
            keyImage.material.color = Color.black;
        }

        if(currentTime > minTime)
        {
            currentTime -= Time.deltaTime;
        }
        else
        {
            currentTime = 0.0f;
            isDead = true;

        }

        //GameOver
        if (isDead)
        {
            isDead = false;
            player.gameObject.GetComponent<PlayerMovement>().isAlive = false;
            player.gameObject.GetComponent<Animator>().SetTrigger("Dying");
        }

        //Diplay Time
        if (currentTime > 100.0f)
        timeText.text = currentTime.ToString("000.00");
        else if(currentTime < 100.0f && currentTime > 10.0f)
        timeText.text = currentTime.ToString("00.00");
        else
        timeText.text = currentTime.ToString("0.00");
    }
}
