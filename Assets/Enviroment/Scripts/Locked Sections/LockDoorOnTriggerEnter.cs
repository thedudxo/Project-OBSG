using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockDoorOnTriggerEnter : MonoBehaviour
{
    [SerializeField] GameObject doorToLock;

    private void OnTriggerEnter(Collider other)
    {
        doorToLock.SetActive(true);
    }
}
