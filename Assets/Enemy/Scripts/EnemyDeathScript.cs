using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDeathScript : MonoBehaviour {

    [SerializeField] Transform player;
    [SerializeField] GameObject ragdoll;
    [SerializeField] float force;
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
        agent.enabled = false;
        GetComponent<Rigidbody>().AddForce(direction.x * force, force / 2, direction.z * force);
        yield return new WaitUntil(() => grounded == false);
        yield return new WaitUntil(() => grounded == true);
        hit = false;
    }

    private void CheckHealth() {
        if (health <= 0) {
            PlayerManager.enemiesPlayerKilled++;
            Ragdoll();
            //Destroy(gameObject);
        }
    }

    void Ragdoll() {
        dead = true;
        GetComponent<Animator>().SetBool("Dead", true);
        agent.enabled = true;
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<EnemyAI>().enabled = false;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        ragdoll.SetActive(true);
        ragdoll.GetComponent<Rigidbody>().AddForce(direction * 20000);
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
