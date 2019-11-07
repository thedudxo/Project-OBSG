using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTrigger : MonoBehaviour {

    public int damage;

    private void Start() {
        damage = PlayerManager.fistDamage;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == Tags.ENEMY) {
            var enemy = other.gameObject;
            enemy.GetComponent<EnemyDeathScript>().DealDamage(damage);
        }
    }
}
