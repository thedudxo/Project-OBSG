using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {
    
    public int damage;
    public bool canUse = false;
    public Vector3 colliderSize;
    [SerializeField] int specialIndex;
    [SerializeField] Collider AttackCollider;
    float samples = 45;
    float fps;
    bool clickWait = false;
    bool clicked = false;
    bool initialAttack = true;
    List<GameObject> enemies = new List<GameObject>();

    private void Update() {
        if (clickWait) {
            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                clicked = true;
            }
        }
        if (initialAttack && Input.GetKeyDown(KeyCode.Mouse0)) {
            GetComponent<Animator>().SetTrigger(PlayerAnimation.ATTACK);
            initialAttack = false;
        }
        if (Input.GetKeyDown(KeyCode.Q)) {
            if (PlayerManager.special) {
                PlayerManager.special = false;
            } else {
                PlayerManager.special = true;
            }
        }
    }

    //Animation Events
    public void Special(float addRot) {
        if (PlayerManager.special) {
            SpecialsManager.Instance.SpawnSpecial(specialIndex, addRot);
        } else {
            foreach (GameObject e in enemies) {
                e.GetComponent<EnemyDeathScript>().DealDamage(damage);
            }
        }
    }

    public void CheckMouse() {
        clickWait = true;
        clicked = false;
        if(!PlayerManager.special)
            AttackCollider.enabled = true;
    }

    public void Unequip() {
        GetComponentInParent<WeaponManager>().Unequip();
    }

    public void HasClicked() {
        if (!clicked) {
            GetComponent<Animator>().SetTrigger(PlayerAnimation.STOP_ATTACK);
            initialAttack = true;
        }
        clickWait = false;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == Tags.ENEMY) {
            enemies.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == Tags.ENEMY) {
            enemies.Remove(other.gameObject);
        }
    }
}