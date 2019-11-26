using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBall : MonoBehaviour {

    Transform player;
    Vector3 direction;
    Rigidbody rb;
    [SerializeField] float speed = 5;
    [SerializeField] float damage = 20;
    [HideInInspector] public BossScript boss;

    public void ResetPrefab() {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER).transform;
        rb = GetComponent<Rigidbody>();
        StartCoroutine(Timer());
    }
    
    void Update() {
        RotateTowardsPlayer();
        FindDirection();
        MoveForward();
    }

    void RotateTowardsPlayer() {
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 20);
    }

    void FindDirection() {
        var pos = player.position;
        pos.y = pos.y + 1;
        direction = pos - transform.position;
    }

    void MoveForward() {
        rb.velocity = transform.forward * speed;
    }

    void Die() {
        StopAllCoroutines();
        gameObject.SetActive(false);
        boss.EnergyWaveStack.Push(gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == Tags.PLAYER) {
            player.GetComponent<PlayerDeath>().DamagePlayer(damage, boss.transform);
            Die();
        }
    }

    IEnumerator Timer() {
        yield return new WaitForSeconds(5);
        Die();
    }
}