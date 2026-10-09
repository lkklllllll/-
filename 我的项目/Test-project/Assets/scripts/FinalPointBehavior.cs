using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalPointBehavior : MonoBehaviour
{
    //private ParticleSystem particle_System;

    private SoundServer soundServer;
    private void Start()
    {
        //particle_System = GetComponent<ParticleSystem>();
        //particle_System.Stop();
        soundServer = GameObject.Find("SoundServer").GetComponent<SoundServer>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            enabled = false;
            BoxCollider2D box = GetComponent<BoxCollider2D>();
            box.enabled = false;
            soundServer.BGMWhenEnd();
            //particle_System.Play();
            StartCoroutine(END());
        }

    }
    private IEnumerator END()
    {
        yield return new WaitForSecondsRealtime(4.5f);
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
