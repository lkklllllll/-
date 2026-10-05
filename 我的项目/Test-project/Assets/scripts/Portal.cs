using UnityEngine;

public class Portal : MonoBehaviour
{
    public Portal targetportal;
    public float freeze = 0.5f;
    private float ignoretime = 0;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Time.time < ignoretime) return;

        if (!other.CompareTag("Player")) return;

        Rigidbody2D playerBody = other.GetComponent<Rigidbody2D>();
        if (playerBody == null) return;

        Vector3 newPosition = targetportal.transform.position;
        newPosition.z = other.transform.position.z;
        PlayerDash dash=other.GetComponent<PlayerDash>();
        dash.dashTimer = -1f;
        playerBody.position = newPosition;
        playerBody.velocity = Vector2.zero;
        targetportal.ignoretime = Time.time + freeze;
    }
}