using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GameManager gm;
    public GameObject debugPoint;

    AudioSource AS;

    private void Start()
    {
        AS = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            StartCoroutine(ForAudio());  
        }
    }

    IEnumerator ForAudio()
    {
        AS.Play();
        gm.hasKey = true;
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}

