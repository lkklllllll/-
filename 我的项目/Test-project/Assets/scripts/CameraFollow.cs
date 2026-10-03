using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("目标")]
    [SerializeField] private Transform target;
    [SerializeField] private Vector2 followOffset;

    [Header("平滑")]
    [SerializeField] private float smoothTime;

    private Playerdeath death;
    private Vector3 velocity;

    private void Start()
    {
        if (target == null)
        {
            Debug.LogError("CameraFollow: 没有指定 target");
            enabled = false;
            return;
        }

        death = target.GetComponent<Playerdeath>();
        velocity = Vector3.zero;
        transform.position = GetDesiredPosition();
    }

    private void LateUpdate()
    {
        if (death != null && death.IsDead)
        {
            velocity = Vector3.zero;
            return;
        }

        transform.position = Vector3.SmoothDamp(
            transform.position,
            GetDesiredPosition(),
            ref velocity,
            smoothTime);
    }

    private Vector3 GetDesiredPosition()
    {
        Vector3 pos = target.position;
        pos.x += followOffset.x;
        pos.y += followOffset.y;
        pos.z = transform.position.z;
        return pos;
    }
}
