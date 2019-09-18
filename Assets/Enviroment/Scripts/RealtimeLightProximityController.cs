using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealtimeLightProximityController : MonoBehaviour
{

    [SerializeField] float maxDistance = 10;
    [SerializeField] GameObject thingToDisable;
    [SerializeField] GameObject debugText;
    [SerializeField] bool showDebugText = true;

    private void Start()
    {
        debugText.SetActive(showDebugText);
    }

    void Update()
    {
        float distance = (PlayerManager.player.transform.position - transform.position).magnitude;
        if (distance > maxDistance)
        {
            thingToDisable.SetActive(false);
        }
        else {
            thingToDisable.SetActive(true);
        }
        
    }
}
