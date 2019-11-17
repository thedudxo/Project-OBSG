using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButtons : MonoBehaviour {

    [SerializeField] GameObject key;
    [SerializeField] Door door;
    public bool hasKey = false;

    public void Key() {
        hasKey = true;
        door.CheckButtons();
    }

    private void OnTriggerStay(Collider other) {
        if(hasKey) { return; }
        if (other.tag == Tags.PLAYER) {
            other.GetComponent<Interact>().CheckKey(key, gameObject);
        }
    }
}