using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour {

    public void CheckKey(GameObject key, GameObject button) {
        if (PlayerManager.doorKeys.Contains(key)) {
            CheckInteract(key, button);
        } else {
            Debug.Log("Need " + key);
        }
    }

    void CheckInteract(GameObject key, GameObject button) {
        if (Input.GetKeyDown(KeyCode.E)) {
            key.GetComponent<Collider>().enabled = false;
            key.GetComponent<DoorKey>().enabled = false;
            button.GetComponent<DoorButtons>().Key();
            var pos = button.transform.position;
            pos.y += 0.5f;
            key.transform.position = pos;
            key.SetActive(true);
        }
    }
}
