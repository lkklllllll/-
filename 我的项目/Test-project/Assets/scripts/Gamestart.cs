using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamestart : MonoBehaviour
{
    private GameObject Player;
    private void Awake()
    {
        Player= GameObject.Find("Player");
        Player.SetActive(false);
    }
    public void StartGame()
    {
        gameObject.SetActive(false);
        Player.SetActive(true);
    }
}
