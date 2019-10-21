using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    public GameObject special;
    public int damage;
    public bool canUse = false;
    [SerializeField] int specialIndex;
    float samples = 45;
    float fps;
    bool clickWait = false;
    bool clicked = false;
    bool initialAttack = true;

    private void Update() {
//        fps = 1 / Time.deltaTime;
//        if(fps < samples) {
//            GetComponent<Animator>().speed = fps / samples;
//        } else {
//            GetComponent<Animator>().speed = 1;
//        }
        if (clickWait) {
            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                clicked = true;
            }
        }
        if (initialAttack && Input.GetKeyDown(KeyCode.Mouse0)) {
            GetComponent<Animator>().SetTrigger(PlayerAnimation.ATTACK);
            initialAttack = false;
        }
        if (Input.GetKeyDown(KeyCode.Q)) {
            if (PlayerManager.special) {
                PlayerManager.special = false;
            } else {
                PlayerManager.special = true;
            }
        }
    }

    //Animation Events
    public void Special(float addRot) {
        if (PlayerManager.special) {
            SpecialsManager.Instance.SpawnSpecial(specialIndex, addRot);
        }
    }

    public void CheckMouse() {
        clickWait = true;
        clicked = false;
    }

    public void Unequip() {
        GetComponentInParent<WeaponManager>().Unequip();
    }

    public void HasClicked() {
        if (!clicked) {
            GetComponent<Animator>().SetTrigger(PlayerAnimation.STOP_ATTACK);
            initialAttack = true;
        }
        clickWait = false;
    }
}
