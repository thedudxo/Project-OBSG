using UnityEngine;

public class TranslateSinEachFrame : MonoBehaviour
{
    [SerializeField] Vector3 speed = new Vector3();
    [SerializeField] Vector3 intensity = new Vector3();
    Vector3 startPosition;
    Vector3 sin = new Vector3();

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        sin += speed;

        transform.position = new Vector3(
            startPosition.x + Mathf.Sin(sin.x) * intensity.x,
            startPosition.y + Mathf.Sin(sin.y) * intensity.y,
            startPosition.z + Mathf.Sin(sin.z) * intensity.z
            );
    }
}
