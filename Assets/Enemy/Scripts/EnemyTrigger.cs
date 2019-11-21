using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour {

    [SerializeField] float damage = 10;
    [SerializeField] Transform enemy;

    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.gameObject);
        if (other.tag == Tags.PLAYER) {
            Debug.Log("DamagePlayer");
            other.GetComponent<PlayerDeath>().DamagePlayer(damage, enemy);
        }
    }
}
