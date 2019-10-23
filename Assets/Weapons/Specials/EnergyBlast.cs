using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class EnergyBlast : MonoBehaviour {

    [SerializeField] int damage;

    private void Start() {
        GetComponentInChildren<VisualEffect>().SendEvent("OnPlay");
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Disable");
        GetComponentInChildren<VisualEffect>().SendEvent("Hit");
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        GetComponent<Special>().active = false;
        GetComponentInChildren<Light>().intensity = 0;
        if (other.tag == Tags.ENEMY) {
            other.GetComponent<EnemyDeathScript>().DealDamage(damage);
        }
        StartCoroutine(Disable());
    }

    IEnumerator Disable() {
        yield return new WaitForSeconds(1);
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
        SpecialsManager.Instance.FistSpecial.Push(gameObject);
        GetComponentInChildren<Light>().intensity = 500;
        gameObject.SetActive(false);
    }

}
