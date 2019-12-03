using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnScript : MonoBehaviour {
    public GameObject boss;

    public void Disappear() {
        boss.GetComponent<BossScript>().BeamBurnStack.Push(gameObject);
        gameObject.SetActive(false);
    }
}
