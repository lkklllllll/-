using System.Collections;
using UnityEngine;

public class Playerdeath : MonoBehaviour
{ 
    [SerializeField] private LayerMask spike;
    private Rigidbody2D body;
    private BoxCollider2D box;
    private float defaultGravityScale;
    private Vector2 spawnPosition;
    private SoundServer SoundServe;
    private Animator animator;
    public bool IsDead { get; private set; }
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        box=GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        GameObject spawnPoint = GameObject.Find("SpawnPoint");
        if (spawnPoint != null)
        {
            spawnPosition = spawnPoint.transform.position;
        }
        else
        {
            spawnPosition = transform.position;
        }
        SoundServe = GameObject.Find("SoundServer").GetComponent<SoundServer>();
        defaultGravityScale=body.gravityScale;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if((spike & (1 << other.gameObject.layer)) != 0)
        {
            StartCoroutine(Die());
        }
    }
    private IEnumerator Die()
    {
        IsDead = true;
        SoundServe.ApplySoundCallOneShot(transform.position, "Sounds/Player Death");
        animator.SetBool("Death", true);
        body.velocity = Vector2.zero;
        body.gravityScale = defaultGravityScale;
        animator.SetFloat("Jump", 0f);
        animator.SetFloat("speed", 0f);
        box.enabled = false;
        yield return new WaitForSecondsRealtime(1);
        body.position = spawnPosition;
        body.velocity = Vector2.zero;
        animator.SetBool("Death", false);
        IsDead = false;
        box.enabled = true;
    }

    public void SetSpawnPoint(Vector2 position)
    {
        spawnPosition = position;
    }
}
