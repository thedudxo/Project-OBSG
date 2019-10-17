using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class Attack : MonoBehaviour {

    [SerializeField] Collider rightTrigger;
    [SerializeField] DamageTrigger rightFist;
    [SerializeField] Collider leftTrigger;
    [SerializeField] DamageTrigger leftFist;
    [SerializeField] float bloodMeterDecrease = 2;
    [SerializeField] GameObject effect;
    [SerializeField] Transform effectSpawn;
    [SerializeField] bool special = false;
    public bool initialAttack = false;

    void Update() {
        CheckAttack();
        CheckSpecial();
        if (special) {
            PlayerManager.bloodMeter -= Time.deltaTime * bloodMeterDecrease;
            if (PlayerManager.bloodMeter <= 0) {
                PlayerManager.bloodMeter = 0;
                //special = false;
            }
        }
    }

    void CheckAttack() {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !initialAttack) {
            foreach(Animator anim in PlayerManager.animators) {
                anim.SetTrigger("Attack");
            }
            initialAttack = true;
        }
    }

    void CheckSpecial() {
        if(PlayerManager.isFull && Input.GetKeyDown(KeyCode.Q)) {
            special = true;
        }
    }

    void SpecialLeft() {
        if (special) {
            effect.transform.position = effectSpawn.position;
            effect.transform.localRotation = effectSpawn.rotation;
            effect.GetComponent<ParticleSystem>().Play();
            effect.GetComponent<Collider>().enabled = true;
            effect.GetComponent<Special>().active = true;
            effect.GetComponentInChildren<VisualEffect>().SendEvent("OnPlay");
        }
    }
}