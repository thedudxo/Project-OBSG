using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class EnergyBlast : MonoBehaviour {

    [SerializeField] int damage;
    float intensity;
    Light light;

    private void Start() {
        GetComponentInChildren<VisualEffect>().SendEvent("OnPlay");
        light = GetComponentInChildren<Light>();
        intensity = light.intensity;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == Tags.PLAYER || other.tag == Tags.RESPAWN) { return; }
        GetComponentInChildren<VisualEffect>().SendEvent("Hit");
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        GetComponent<Special>().active = false;
        GetComponentInChildren<Light>().intensity = 0;

        if (other.tag == Tags.ENEMY) {
            other.GetComponent<EnemyDeathScript>().DealDamage(damage);
            int i = Random.Range(0, 2);
            other.GetComponent<EnemyAudioManager>().Play("EnergyHit" + i);
        } else if(other.tag == Tags.BOSS) {
            other.GetComponentInParent<BossScript>().DealDamage(damage);
        }
        StartCoroutine(Disable());
    }

    IEnumerator Disable() {
        yield return new WaitForSeconds(1);
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
        SpecialsManager.Instance.FistSpecial.Push(gameObject);
        GetComponentInChildren<Light>().intensity = intensity;
        gameObject.SetActive(false);
    }

}
