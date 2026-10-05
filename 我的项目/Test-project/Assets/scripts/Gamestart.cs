using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamestart : MonoBehaviour
{
    private GameObject Player;
    private GameObject GameHud;
    private void Awake()
    {
        Player= GameObject.Find("Player");
        Player.SetActive(false);
        GameHud = GameObject.Find("GameHud");
        if (GameHud != null)
        {
            GameHud.SetActive(false);
        }
    }
    public void StartGame()
    {
        gameObject.SetActive(false);
        Player.SetActive(true);
        if (GameHud != null)
        {
            GameHud.SetActive(true);
        }
    }
}
