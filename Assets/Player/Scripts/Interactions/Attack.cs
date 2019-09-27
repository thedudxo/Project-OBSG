using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    public int damage;
    Collider trigger;

    void Start() {
        damage = PlayerManager.fistDamage;
        trigger = GetComponent<Collider>();
    }

    void Update() {
        CheckAttack();
    }

    void CheckAttack() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            GetComponent<Animator>().SetTrigger(PlayerAnimation.LEFT_PUNCH);
        } if (Input.GetKeyDown(KeyCode.Mouse1)) {
            GetComponent<Animator>().SetTrigger(PlayerAnimation.RIGHT_PUNCH);
        }
    }

    public void AttackTriggerOn() {
        trigger.enabled = true;
    }

    public void AttackTriggerOff() {
        trigger.enabled = false;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == Tags.ENEMY) {
            var enemy = other.gameObject;
            enemy.GetComponent<EnemyDeathScript>().DealDamage(damage);
        }
    }
}
