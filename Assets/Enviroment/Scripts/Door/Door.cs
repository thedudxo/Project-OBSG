using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    [SerializeField] List<GameObject> buttons = new List<GameObject>();

    public void CheckButtons() {
        Debug.Log(buttons.TrueForAll(HasButton));
        if (buttons.TrueForAll(HasButton)) {
            GetComponent<Animator>().SetBool("DoorOpen", true);
        }
    }

    static bool HasButton(GameObject g) {
        return g.GetComponent<DoorButtons>().hasKey;
    }
}
