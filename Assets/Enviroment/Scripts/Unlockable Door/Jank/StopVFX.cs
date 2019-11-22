using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class StopVFX : MonoBehaviour {

    [SerializeField] VisualEffect leftDoor;
    [SerializeField] VisualEffect rightDoor;

    public void VFXOff() {
        leftDoor.SendEvent("Stop");
        rightDoor.SendEvent("Stop");
    }

    public void VFXOn() {
       leftDoor.SendEvent("Opening");
        rightDoor.SendEvent("Opening");
    }
}
