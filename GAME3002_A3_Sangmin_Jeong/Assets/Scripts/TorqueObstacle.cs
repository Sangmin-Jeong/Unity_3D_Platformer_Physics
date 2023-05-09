using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorqueObstacle : MonoBehaviour
{
    private Rigidbody rb;
    public Vector3 torqueForce = Vector3.zero;

    public GameObject player;
    private PlayerMovement pm;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if(player)
        {
            pm = player.gameObject.transform.GetComponent<PlayerMovement>();
        }
    }

    void FixedUpdate()
    {
        rb.angularVelocity = torqueForce;
        //rb.AddTorque(torqueForce);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            rb.isKinematic = true;
            pm.Die();
        }
    }
}
