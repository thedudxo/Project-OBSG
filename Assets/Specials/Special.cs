using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class Special : MonoBehaviour
{

    //----------------------Debug------------------------//
    /*
    float resetTime;
    float resetTimer = 2;
    GameObject effect;
    Vector3 position;
    
    private void Start() {
        resetTime = Time.time;
        position = transform.position;
        effect = gameObject;
        GetComponentInChildren<VisualEffect>().SendEvent("OnPlay");
    }

    private void Update() {
        if(resetTime + resetTimer <= Time.time) {
            transform.position = position;
            resetTime = Time.time;
            GetComponentInChildren<VisualEffect>().SendEvent("OnPlay");
        }
        transform.Translate(-Vector3.forward * Time.deltaTime * speed);
    }
    */

    [SerializeField] float speed = 5f;
    [SerializeField] int damage = 25;
    public bool active = false;

    private void Start() {
        gameObject.GetComponentInChildren<VisualEffect>().SendEvent("Hit");
    }

    private void Update() {
        if(active)
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
    private void OnTriggerEnter(Collider other) {
        if(other.tag == Tags.PLAYER) { return; }
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.GetComponentInChildren<VisualEffect>().SendEvent("Hit");
        active = false;
        if (other.tag == Tags.ENEMY) {
            other.GetComponent<EnemyDeathScript>().DealDamage(damage);
        }
    }
}
