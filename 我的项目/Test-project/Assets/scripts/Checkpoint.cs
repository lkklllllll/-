using System.Collections;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Transform flag;
    [SerializeField] private float raisedHeight;
    [SerializeField] private float raiseSpeed;

    private float loweredY;
    private float raisedY;
    private bool activated;

    private SoundServer soundServer;

    private void Awake()
    {
        if (flag == null)
        {
            return;
        }

        loweredY = flag.localPosition.y;
        raisedY = loweredY + raisedHeight;
        soundServer = GameObject.Find("SoundServer").GetComponent<SoundServer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (activated)
        {
            return;
        }

        if (!other.CompareTag("Player"))
        {
            return;
        }

        Playerdeath death = other.GetComponentInParent<Playerdeath>();
        if (death == null)
        {
            return;
        }
        if(death.IsDead)
        {
            return;
        }
        death.SetSpawnPoint(transform.position);
        activated = true;

        if (flag != null)
        {
            soundServer.ApplySoundCallOneShot(transform.position, "Sounds/Reach CheckPoint");
            StartCoroutine(RaiseFlag());
        }
    }

    private IEnumerator RaiseFlag()
    {
        Vector3 position = flag.localPosition;

        while (position.y < raisedY)
        {
            position.y = Mathf.MoveTowards(position.y, raisedY, raiseSpeed * Time.deltaTime);
            flag.localPosition = position;
            yield return null;
        }
    }
}
