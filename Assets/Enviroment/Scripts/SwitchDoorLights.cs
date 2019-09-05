using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchDoorLights : MonoBehaviour
{
    [SerializeField] GameObject lightDisable;
    [SerializeField] GameObject lightEnable;


    private void OnTriggerEnter(Collider other)
    {
        lightDisable.SetActive(false);
        lightEnable.SetActive(true);
    }
}
