using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoorOnTriggerEnter : MonoBehaviour
{
    [SerializeField] GameObject doorToUnLock;

    private void OnTriggerEnter(Collider other)
    {
        doorToUnLock.SetActive(false);
    }
}
