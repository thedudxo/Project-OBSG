using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Attack : MonoBehaviour {
    
    public int damage;
    public int baseDamage;
    public int specialDamage;
    public bool canUse = false;
    public Vector3 colliderSize;
    [SerializeField] int specialIndex;
    [SerializeField] float bloodMeterDecrease = 5;
    float samples = 45;
    float fps;
    [SerializeField] bool clickWait = false;
    [SerializeField]bool clicked = false;
    [SerializeField]bool initialAttack = true;
    bool leftClick = false;
    [SerializeField] string hitSound;

    [SerializeField] PlayableDirector test;

    private void Update() {
        if (clickWait) {
            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                clicked = true;
                leftClick = true;
            } else if (Input.GetKeyDown(KeyCode.Mouse1)) {
                clicked = true;
                leftClick = false;
            }
        }
        if (initialAttack && (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1))) {
            //test.Play();
            GetComponent<Animator>().SetTrigger(PlayerAnimation.ATTACK);
            initialAttack = false;
            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                leftClick = true;
            } else if (Input.GetKeyDown(KeyCode.Mouse1)) {
                leftClick = false;
            }
        }
    }

    //Animation Events
    public void Special(float addRot) {
        int i = Random.Range(0, 2);        AudioManager.instance.Play("SwordWhoosh" + i);
        if (PlayerManager.special) {
            if (leftClick) {
                damage = specialDamage;
            } else {
                SpecialsManager.Instance.SpawnSpecial(specialIndex, addRot);
                Debug.Log("Spawn Special");
            }
        } else {
            damage = baseDamage;
        }
        Debug.Log(damage);
        foreach (GameObject e in PlayerManager.enemies) {
            e.GetComponent<EnemyDeathScript>().DealDamage(damage);
            AudioManager.instance.Play(hitSound + i);
        }
//        if (PlayerManager.special) {
//            SpecialsManager.Instance.SpawnSpecial(specialIndex, addRot);
//        } else {
//            foreach (GameObject e in PlayerManager.enemies) {
//                e.GetComponent<EnemyDeathScript>().DealDamage(damage);
//            }
//        }
    }

    public void CheckMouse() {
        clickWait = true;
        clicked = false;
    }

    public void Unequip() {
        GetComponentInParent<WeaponManager>().Unequip();
    }

    public void HasClicked() {
        if (!clicked) {
            //test.Stop();
            GetComponent<Animator>().SetTrigger(PlayerAnimation.STOP_ATTACK);
            initialAttack = true;
        }
        clickWait = false;
    }

    public void BugFix() {
        clickWait = false;
        initialAttack = true;
        clicked = false;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == Tags.ENEMY) {
            PlayerManager.enemies.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == Tags.ENEMY) {
            if(PlayerManager.enemies.Contains(other.gameObject))
                PlayerManager.enemies.Remove(other.gameObject);
        }
    }
}