using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    [SerializeField] Collider rightTrigger;
    [SerializeField] DamageTrigger rightFist;
    [SerializeField] Collider leftTrigger;
    [SerializeField] DamageTrigger leftFist;
    [SerializeField] float bloodMeterDecrease = 2;
    bool special = false;

    void Update() {
        CheckAttack();
        CheckSpecial();
        if (special) {
            PlayerManager.bloodMeter -= Time.deltaTime * bloodMeterDecrease;
            if (PlayerManager.bloodMeter <= 0) {
                PlayerManager.bloodMeter = 0;
                special = false;
            }
        }
    }

    void CheckAttack() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            GetComponent<Animator>().SetTrigger(PlayerAnimation.LEFT_PUNCH);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1)) {
            GetComponent<Animator>().SetTrigger(PlayerAnimation.RIGHT_PUNCH);
        }
    }

    void CheckSpecial() {
        if(PlayerManager.isFull && Input.GetKeyDown(KeyCode.Q)) {
            special = true;
        }
    }

    
    public void TiggerOnLeft() {
        leftTrigger.enabled = true;
    }
    
    public void TiggerOffLeft() {
        leftTrigger.enabled = false;
    }

    public void TiggerOnRight() {
        rightTrigger.enabled = true;
    }
    
    public void TiggerOffRight() {
        rightTrigger.enabled = false;
    }
}