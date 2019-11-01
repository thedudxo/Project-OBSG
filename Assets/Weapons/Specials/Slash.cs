using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slash : MonoBehaviour {

    [SerializeField] int damage;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == Tags.ENEMY) {
            other.GetComponent<EnemyDeathScript>().DealDamage(damage);
        }
        if (other.gameObject.layer == 12) {
            StartCoroutine(Disable());
        }
    }

    IEnumerator Disable() {
        yield return new WaitForSeconds(1.5f);
        Debug.Log("Stop Special");
        SpecialsManager.Instance.SwordSpecial.Push(gameObject);
        gameObject.SetActive(false);
    }

}
