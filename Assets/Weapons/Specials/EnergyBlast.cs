﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class EnergyBlast : MonoBehaviour {

    [SerializeField] int damage;

    private void Start() {
        GetComponentInChildren<VisualEffect>().SendEvent("OnPlay");

    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == Tags.PLAYER || other.tag == Tags.RESPAWN) { return; }
        GetComponentInChildren<VisualEffect>().SendEvent("Hit");
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        GetComponent<Special>().active = false;
        GetComponentInChildren<Light>().intensity = 0;
        int i = Random.Range(0, 2);
        GetComponent<EnemyAudioManager>().Play("EnergyHit" + i);

        if (other.tag == Tags.ENEMY) {
            other.GetComponent<EnemyDeathScript>().DealDamage(damage);
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
        GetComponentInChildren<Light>().intensity = 500;
        gameObject.SetActive(false);
    }

}
