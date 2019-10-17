using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHold : MonoBehaviour {

    public GameObject special;
    public int damage;
    public bool canUse = false;
    bool clickWait = false;
    bool clicked = false;

    private void Update() {
        if (clickWait) {
            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                clicked = true;
            }
        }
    }

    public void CheckMouse() {
        clickWait = true;
        clicked = false;
    }

    public void HasClicked() {
        if (!clicked) {
            foreach (Animator anim in PlayerManager.animators) {
                anim.SetTrigger("StopAttack");
            }
            GetComponentInParent<Attack>().initialAttack = false;
        }
        clickWait = false;
    }
}
