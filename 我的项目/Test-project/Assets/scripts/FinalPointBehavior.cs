using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalPointBehavior : MonoBehaviour
{
    private string[] cast = {"企划\n朱家骏", "美术\n何永清", "声效\n郑力铭", "代码\n朱家骏\n王俞澳", "地图与玩法设计\n曹宇成" };
    private int cast_index = 0;
    private bool cast_displaying = false;
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
            //enabled = false;
            BoxCollider2D box = GetComponent<BoxCollider2D>();
            box.enabled = false;
            soundServer.BGMWhenEnd();
            soundServer.Invoke(nameof(soundServer.BGMSetTest), 5f);
            Invoke(nameof(CastDisplay), 5f);
            //particle_System.Play();
            //StartCoroutine(END());
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
    private void Update()
    {
        if (cast_displaying)
        {
            RectTransform rectTransform = GetComponent<RectTransform>();
            rectTransform.position += Vector3.up * Time.deltaTime;
            TextMeshPro textMeshPro = GetComponent<TextMeshPro>();
            Color color = textMeshPro.color;
            color.a -= 0.001f;
            textMeshPro.color = color;
        }
    }
    public void CastDisplay()
    {
        cast_displaying = true;
        TextMeshPro textMeshPro = GetComponent<TextMeshPro>();
        textMeshPro.enabled = true;
        textMeshPro.color = Color.white;
        textMeshPro.text = cast[cast_index];
        transform.position = new Vector3(163.99f + Random.Range(0f, 10f), 103.04f - Random.Range(2f, 4f), 0f);
        cast_index++;
        if (cast_index >= cast.Length)
        {
            cast_index = 0;
        }
        if (enabled)
        {
            Invoke(nameof(CastDisplay), 3.1168f);
        }
    }
}
