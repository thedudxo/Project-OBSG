using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamTrigger : MonoBehaviour {

    [SerializeField] float damage = 0.5f;
    [SerializeField] Transform enemy;
    bool inTrigger;

    private void OnTriggerEnter(Collider other) {
        Debug.Log(other.tag);
        if (other.tag == Tags.PLAYER) {
            PlayerDeath pd = other.GetComponent<PlayerDeath>();
            inTrigger = true;
            StartCoroutine(DotDamage(pd));
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == Tags.PLAYER) {
            inTrigger = false;
        }
    }

    IEnumerator DotDamage(PlayerDeath pd) {
        yield return new WaitForEndOfFrame();
        if (inTrigger) {
            pd.DamagePlayer(damage, enemy);
            StartCoroutine(DotDamage(pd));
        }
    }
}
