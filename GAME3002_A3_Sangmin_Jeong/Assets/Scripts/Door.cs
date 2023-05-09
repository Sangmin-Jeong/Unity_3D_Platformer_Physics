using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameManager gm;
    public GameObject player;
    public GameObject door;


    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //If Player has key
            if(gm.hasKey == true)
            {
                door.GetComponent<Rigidbody>().freezeRotation = false;
            }
            else
            {
                // Display warnning
                gm.keyCheckerText.gameObject.SetActive(true);
                door.GetComponent<Rigidbody>().freezeRotation = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            gm.keyCheckerText.gameObject.SetActive(false);
        }

    }

}