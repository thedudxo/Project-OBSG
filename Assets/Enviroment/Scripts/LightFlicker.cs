using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{

    Light light;
    [SerializeField] float min;
    [SerializeField] float max;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
    }

    void Update()
    {

        Mathf.Clamp(light.intensity, min, max);

        if (Random.Range(0, 2) == 1)
            light.intensity += 0.05f;
        else
            light.intensity += -0.05f;

        
    }
}
