using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateSinEachFrame : MonoBehaviour
{
    [SerializeField] Vector3 speed = new Vector3(0, 0, 0);
    [SerializeField] Vector3 intensity = new Vector3(0, 0, 0);
    Vector3 position;
    float sin = 0;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        sin += speed.x;

        transform.position = new Vector3(
            position.x + Mathf.Sin(sin) * intensity.x,
            position.y, position.z);
    }
}
