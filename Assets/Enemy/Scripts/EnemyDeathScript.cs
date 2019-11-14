﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDeathScript : MonoBehaviour {

    [SerializeField] Transform player;
    [SerializeField] Collider attackCol;
    [SerializeField] Rigidbody ragdoll;
    [SerializeField] float force;
    [SerializeField] float ragdollForce;
    [SerializeField] int bloodMeterAdd;
    [SerializeField] Material hitMat;
    private NavMeshAgent agent;
    private Vector3 direction;
    private float maxSlope = 60;
    private bool grounded = false;
    private bool hit = false;
    [HideInInspector] public bool dead = false;
    public int health = 50;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
    }

    public void DealDamage(int damage) {
        direction = player.position - transform.position;
        direction = -direction.normalized;
        health = health - damage;
        CheckHealth();
        if (!hit) {
            StartCoroutine(Force());
        }
    }

    IEnumerator Force() {
        hit = true;
        hitMat.color = new Color(255, 255, 255);
        yield return new WaitForSeconds(1);
        hitMat.color = new Color(173, 133, 97);
        hit = false;
    }

    private void CheckHealth() {
        if (health <= 0) {
            Ragdoll();
            PlayerManager.enemies.Remove(gameObject);
            //Destroy(gameObject);
        }
    }

    void Ragdoll() {
        PlayerManager.enemiesPlayerKilled++;
        if (!PlayerManager.isFull && !PlayerManager.special) {
            PlayerManager.bloodMeter = PlayerManager.bloodMeter + bloodMeterAdd;
            CheckMeter();
        }
        dead = true;
        GetComponent<Animator>().enabled = false;
        agent.enabled = true;
        attackCol.enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<EnemyAI>().enabled = false;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        foreach(Rigidbody r in GetComponentsInChildren<Rigidbody>()) {
            r.isKinematic = false;
        }
        ragdoll.AddForce(direction * ragdollForce);
    }

    void CheckMeter() {
        if(PlayerManager.bloodMeter >= PlayerManager.maxBloodMeter) {
            PlayerManager.bloodMeter = PlayerManager.maxBloodMeter;
            PlayerManager.isFull = true;
        }
    }

    private void OnCollisionExit(Collision collision) {
        grounded = false;
    }

    private void OnCollisionStay(Collision collision) {
        if (collision.gameObject.tag == Tags.PLAYER || dead) { return; }
        foreach (ContactPoint contact in collision.contacts) {
            if(Vector3.Angle(contact.normal, Vector3.up) < maxSlope) {
                grounded = true;
            }
        }
        if (hit)
            return;
        else
            agent.enabled = true;
    }
}
