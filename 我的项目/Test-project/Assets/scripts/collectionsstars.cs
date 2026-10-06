using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectionsstars : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Collider2D triggerCollider;
    private SoundServer soundServer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        triggerCollider = GetComponent<Collider2D>();
        soundServer = GameObject.Find("SoundServer").GetComponent<SoundServer>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            PlayerDash dash = other.GetComponent<PlayerDash>();
            dash.RefreshDash();
            soundServer.ApplySoundCallOneShot(transform.position, "Sounds/Pickup Gem");
            StartCoroutine(Refresh());
        }
    }
    private IEnumerator Refresh()
    {
        spriteRenderer.enabled = false;
        triggerCollider.enabled = false;
        yield return new WaitForSecondsRealtime(2);
        spriteRenderer.enabled = true;
        triggerCollider.enabled = true;
    }
}
