using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectionsdiamonds : MonoBehaviour
{
    private SoundServer soundServer;
    private void Start()
    {
        soundServer = GameObject.Find("SoundServer").GetComponent<SoundServer>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerDash dash = other.GetComponent<PlayerDash>();
            dash.addmaxdashcount();
            soundServer.ApplySoundCallOneShot(transform.position, "Sounds/Pickup Health");
            gameObject.SetActive(false);
        }

    }
}
