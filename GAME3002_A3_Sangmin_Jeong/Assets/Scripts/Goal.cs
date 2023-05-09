using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameManager gm;
    public GameObject player;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //Clear the game
            player.SetActive(false);
            player.gameObject.GetComponent<PlayerMovement>().isAlive = false;
            gm.clearText.gameObject.SetActive(true);
        }
    }
}
