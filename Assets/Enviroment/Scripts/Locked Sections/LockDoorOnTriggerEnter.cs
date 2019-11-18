using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDoorOnTriggerEnter : MonoBehaviour
{
    [SerializeField] GameObject doorToLock;

    [SerializeField] GameObject disableProbes;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Tags.PLAYER)
        {
            doorToLock.SetActive(true);
            disableProbes.SetActive(false);
        }
    }
}
