using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{

    Light light;
    [SerializeField] float minIntensity;
    [SerializeField] float maxIntensity;

    [SerializeField] float minRange;
    [SerializeField] float maxRange;

    [SerializeField] float heightBobble;
    float startHeight;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        startHeight = light.gameObject.transform.position.y;
    }

    void Update()
    {
        //intensity
        Mathf.Clamp(light.intensity, minIntensity, maxIntensity);

        if (Random.Range(0, 2) == 1)
            light.intensity += 0.05f;
        else
            light.intensity += -0.05f;


        //range
        Mathf.Clamp(light.range, minRange, maxRange);

        if (Random.Range(0, 2) == 1)
            light.range += 0.05f;
        else
            light.range += -0.05f;


        //height
        
        Vector3 pos = light.gameObject.transform.position;

        if (Random.Range(0, 2) == 1)
            pos.y += 0.001f;
        else
            pos.y += -0.001f;

        Mathf.Clamp(pos.y, startHeight + heightBobble, startHeight - heightBobble);

        light.gameObject.transform.position = pos;
    }

}



