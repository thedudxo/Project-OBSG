using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {

    [SerializeField] List<GameObject> enable = new List<GameObject>();
    [SerializeField] List<GameObject> disable = new List<GameObject>();

    private void OnTriggerEnter(Collider other) {
        foreach(GameObject g in enable) {
            g.SetActive(true);
        }
        foreach (GameObject g in disable) {
            g.SetActive(false);
        }
    }
}
