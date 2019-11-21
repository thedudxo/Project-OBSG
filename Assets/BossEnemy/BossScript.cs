using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour {

    public BossState bossState;
    Transform playerTarget;
    Vector3 direction;
    bool attacking = true;
    bool beam = false;
    public bool dead = false;
    float rangeRadius = 20;
    float distance;
    int attackFrame;
    int attackFrame_counter = 0;
    [SerializeField] List<string> longRangeAttacks = new List<string>(); 
    [SerializeField] List<string> midRangeAttacks = new List<string>();
    [SerializeField] int bossHealth;

    delegate void EveryFrame();
    EveryFrame everyFrame;
    delegate void AttackFrame();
    AttackFrame aFrame;

    private void Start() {
        bossState = BossState.longRange;
        ChangeState(BossState.longRange);
        SetPrefab();
    }

    public void SetPrefab() {
        playerTarget = GameObject.FindGameObjectWithTag(Tags.PLAYER).transform;
    }

    private void Update() {
        DistanceCheck(playerTarget);
        FindDirection(playerTarget);
        MonitorStates();
        if (!attacking) {
            attackFrame_counter++;
            RotateTowardsTarget();
            if (attackFrame_counter > attackFrame) {
                if (aFrame != null)
                    aFrame();
                attackFrame = Random.Range(200, 400);
                attackFrame_counter = 0;
            }
        } else {
        }
    }

    void MonitorStates() {
        switch (bossState) {
            case BossState.midRange:
                if (distance > rangeRadius)
                    ChangeState(BossState.longRange);
                break;
            case BossState.longRange:
                if (distance < rangeRadius)
                    ChangeState(BossState.midRange);
                break;
            default:
                break;
        }
    }

    void ChangeState(BossState targetState) {
        bossState = targetState;
        everyFrame = null;
        aFrame = null;
        switch (targetState) {
            case BossState.midRange:
                aFrame = midRange;
                break;
            case BossState.longRange:
                aFrame = longRange;
                break;
            default:
                break;
        }
    }

    void midRange() {
        AttackTarget(midRangeAttacks);
    }

    void longRange() {
        AttackTarget(longRangeAttacks);
    }

    void DistanceCheck(Transform target) {
        distance = Vector3.Distance(transform.position, target.position);
    }

    void RotateTowardsTarget() {
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        targetRotation.x = 0;
        targetRotation.z = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 20);
    }

    void FindDirection(Transform target) {
        direction = target.position - transform.position;
    }

    void AttackTarget(List<string> list) {
        attacking = true;
        int i = Random.Range(0, list.Count);
        string attack = list[i];
        GetComponent<Animator>().SetTrigger(attack);
    }

    public void DealDamage(int damage) {
        if (!dead) {
            bossHealth -= damage;
            CheckHealth();
        }
    }

    void CheckHealth() {
        if(bossHealth <= 0) {
            Dead();
            dead = true;
        }
    }

    void Dead() {
        GetComponent<Animator>().SetTrigger("Die");
    }

    public void resetBool() {
        attacking = false;
    }

    public enum BossState {
        midRange, longRange
    }
}
