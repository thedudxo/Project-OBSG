using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour {

    int damage;

    private void OnTriggerEnter(Collider other) {
        if(other.tag == Tags.ENEMY) {
            other.GetComponent<EnemyDeathScript>().DealDamage(damage);
        }
        if(other.gameObject.layer == 12) {

        }
    }
}
