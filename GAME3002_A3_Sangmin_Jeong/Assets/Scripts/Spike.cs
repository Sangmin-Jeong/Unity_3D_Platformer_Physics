using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public GameObject player;
    private PlayerMovement pm;

    private float speed = 2.0f;
    private const float MAX_Y = -11.0f;
    private const float MIN_Y = -15.0f;
    private bool isGoingDown = true;
    
    private void Start()
    {
        if(player)
        {
            pm = player.gameObject.transform.GetComponent<PlayerMovement>();
        }
    }

    private void FixedUpdate()
    {

        if(transform.position.y <= MIN_Y)
        {
            isGoingDown = false;
        }
        
        if(transform.position.y >= MAX_Y)
        {
            isGoingDown = true;
        }
        

        if(isGoingDown)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - (speed * Time.deltaTime), transform.position.z);
        }
        else
        { 
            gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + (speed * Time.deltaTime), transform.position.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            pm.Die();
        }
    }
}
