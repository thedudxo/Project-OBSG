using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldAttack : MonoBehaviour {

    private Pickup pickupScript;
    private PlayerMovement movementScript;
    [SerializeField] Collider leftAttackCol;
    [SerializeField] Collider rightAttackCol;
    public int fistDamage;
    public int rightDamage;
    public int leftDamage;

	void Awake () {
        pickupScript = GetComponent<Pickup>();
        movementScript = GetComponent<PlayerMovement>();
        rightDamage = leftDamage = fistDamage;
	}

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            GetComponent<Animator>().SetTrigger(PlayerAnimation.LEFT_PUNCH);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1)) {
            GetComponent<Animator>().SetTrigger(PlayerAnimation.RIGHT_PUNCH);
        }
    }

    void CheckAttack (GameObject enemy) {
        /*if (Input.GetKeyDown(KeyCode.Mouse0)) {//Left Click
            if (movementScript.frontDash) {
                Debug.Log("Punch");
                StartCoroutine(DashPunch(leftDamage * 2));
            } else {
                AttackEnemy(leftDamage, enemy);
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse1)) {//RightClick
            if (movementScript.frontDash) {
                Debug.Log("Punch");
                StartCoroutine(DashPunch(rightDamage * 2));
            } else {
                AttackEnemy(rightDamage, enemy);
            }
        }*/
	}

    public void ResetDamage() {
        rightDamage = leftDamage = fistDamage;
    }

    void AttackEnemy(int damage, GameObject enemy) {
        enemy.GetComponent<EnemyDeathScript>().DealDamage(damage);
        
        /*float x = Screen.width / 2;
        float y = Screen.height / 2;
        Ray ray = MainCamera.ScreenPointToRay(new Vector3(x, y));
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit)) {
            if (hit.distance <= attackDist) {
                if (hit.collider.tag == Tags.ENEMY) {
                    hit.collider.gameObject.GetComponent<EnemyDeathScript>().DealDamage(damage);
                }
            }
        }*/
    }

    IEnumerator DashPunch(int damage) {
        yield return new WaitUntil(() => movementScript.frontDash == false);
        Debug.Log("Dash Punch");
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == Tags.ENEMY) {
            CheckAttack(other.gameObject);
        }
        if (movementScript.frontDash) { }
    }
    
    /*public void AttackTriggerOn(Collider trigger) {
        trigger.enabled = true;
    }

    public void AttackTriggerOff(Collider trigger) {
        trigger.enabled = false;
    }*/
}
