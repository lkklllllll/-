using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamestart : MonoBehaviour
{
    private GameObject Player;
    private GameObject GameHud;

    private SoundServer soundServer;
    private void Awake()
    {
        Player= GameObject.Find("Player");
        Player.SetActive(false);
        GameHud = GameObject.Find("GameHud");
        if (GameHud != null)
        {
            GameHud.SetActive(false);
        }
        soundServer = GameObject.Find("SoundServer").GetComponent<SoundServer>();
    }
    public void StartGame()
    {
        gameObject.SetActive(false);
        Player.SetActive(true);
        if (GameHud != null)
        {
            GameHud.SetActive(true);
        }
        soundServer.BGMState = BGMType.MAIN;
    }
}
