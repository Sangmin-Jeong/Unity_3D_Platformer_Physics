using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpZone : MonoBehaviour
{
    public GameObject player;
    private PlayerMovement pm;
    
    private void Start()
    {
        if(player)
        {
            pm = player.gameObject.transform.GetComponent<PlayerMovement>();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            pm.speed = pm.MAX_SPEED;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            pm.speed = pm.BASE_SPEED;
        }
    }
}
