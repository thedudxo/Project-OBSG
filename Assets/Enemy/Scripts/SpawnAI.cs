using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnAI : MonoBehaviour {

    public Transform playerTarget;
    public AIState aiState;
    [SerializeField] BoxCollider fist;
    [SerializeField] float damage = 10;
    Vector3 direction;
    Vector3 rotDirection;
    float distance;
    [SerializeField] float strafeSpeed;
    [SerializeField] float viewRadius = 50;
    [SerializeField] float attackRadius = 6;
    [SerializeField] float minAttackRadius = 4;
    float maxDistance = 120;
    bool attacking = false;
    bool preperedAttack = false;
    int layerMask = 1 << 10;
    int lFrame = 15;
    int lFrame_counter = 0;
    int attackFrame;
    int attackFrame_counter = 0;

    delegate void EveryFrame();
    EveryFrame everyFrame;
    delegate void LateFrame();
    LateFrame lateFrame;
    delegate void AttackFrame();
    AttackFrame aFrame;

    NavMeshAgent agent;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        Respawn.enemies.Add(gameObject);
        aiState = AIState.inView;
        ChangeState(AIState.inView);
        SetPrefab();
        attackFrame = Random.Range(0, 300);
        foreach (Rigidbody r in GetComponentsInChildren<Rigidbody>()) {
            r.isKinematic = true;
        }
    }

    public void SetPrefab() {
        playerTarget = GameObject.FindGameObjectWithTag(Tags.PLAYER).transform;
    }

    private void Update() {
        if (GetComponent<EnemyDeathScript>().dead) { return; }
        //layerMask = ~layerMask;
        if (!PlayerManager.alive){
            agent.isStopped = true;
            return;
        }
        MonitorStates();
        if (everyFrame != null)
            everyFrame();
        lFrame_counter++;
        if (lFrame_counter > lFrame) {
            if (lateFrame != null)
                lateFrame();
            lFrame_counter = 0;
        }
        attackFrame_counter++;
        if (attackFrame_counter > attackFrame) {
            if (aFrame != null)
                aFrame();
            attackFrame = Random.Range(0, 180);
            attackFrame_counter = 0;
        }
        if (aiState != AIState.inAttackRange) {
            GetComponent<Animator>().SetFloat(EnemyAnimation.IDLE_BLEND, agent.velocity.magnitude);
        }
    }

    void MonitorStates() {
        switch (aiState) {
            case AIState.inView:
                if (distance < attackRadius)
                    ChangeState(AIState.inAttackRange);
                break;
            case AIState.inAttackRange:
                if (distance > attackRadius)
                    ChangeState(AIState.inView);
                break;
            default:
                break;
        }
    }

    public void ChangeState(AIState targetState) {
        aiState = targetState;
        everyFrame = null;
        lateFrame = null;
        aFrame = null;
        switch (targetState) {
            case AIState.inView:
                lateFrame = InRadiusBehaviours;
                everyFrame = InViewBehaviours;
                break;
            case AIState.inAttackRange:
                lateFrame = InRadiusBehaviours;
                everyFrame = InAttackRangeBehaviours;
                aFrame = AttackTarget;
                break;
            case AIState.attacking:
                everyFrame = AttackingBehaviours;
                break;
            default:
                break;
        }
    }

    void InRadiusBehaviours() {
        if (playerTarget == null)
            return;
        DistanceCheck(playerTarget);
        FindDirection(playerTarget);
    }

    void InViewBehaviours() {
        if (playerTarget == null)
            return;
        FindDirection(playerTarget);
        RotateTowardsTarget();
        MoveToPosition(playerTarget.position);
    }

    void InAttackRangeBehaviours() {
        RotateTowardsTarget();
        FindDirection(playerTarget);
        StopDestination();
        Strafe();
        Retreat();
    }

    void AttackingBehaviours() {
        MoveToPosition(playerTarget.position);
        RotateTowardsTarget();
        FindDirection(playerTarget);
        DistanceCheck(playerTarget);
        AttackAnimation();
        if (attacking) {
            StopDestination();
        }
    }

    void StopDestination() {
        agent.isStopped = true;
    }

    void DistanceCheck(Transform target){
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
        rotDirection = direction;
        rotDirection.y = 0;
    }

    void MoveToPosition(Vector3 playerPosition) {
        agent.SetDestination(playerPosition);
        agent.isStopped = false;
    }

    void AttackTarget() {
        if (preperedAttack) {
            ChangeState(AIState.attacking);
        }
    }

    void AttackAnimation() {
        if (distance <= 2f && !attacking) {
            GetComponent<Animator>().SetBool(EnemyAnimation.ENEMY_ATTACK, true);
            agent.destination = transform.position;
            attacking = true;
        }
    }

    void Strafe() {
        if (distance < attackRadius && distance > minAttackRadius) {
            preperedAttack = true;
            agent.Move(transform.right * strafeSpeed * Time.fixedDeltaTime);
            GetComponent<Animator>().SetFloat(EnemyAnimation.IDLE_BLEND, 5);
        }
    }

    void Retreat() {
        if (distance < minAttackRadius) {
            agent.Move(-transform.forward * strafeSpeed * Time.fixedDeltaTime);
            GetComponent<Animator>().SetFloat(EnemyAnimation.IDLE_BLEND, -5);
            preperedAttack = false;
            if(distance <= 2) {
                ChangeState(AIState.attacking);
            }
        }
    }

    void AgentContinue() {
        agent.isStopped = false;
        ChangeState(AIState.inAttackRange);
        attacking = false;
        GetComponent<Animator>().SetBool(EnemyAnimation.ENEMY_ATTACK, false);
    }

    void ActivateFist() {
        fist.enabled = true;
    }

    void DeactivateFist() {
        fist.enabled = false;
        agent.isStopped = true;
    }

    public void AISpawn() {
        Debug.Log("Reset AI");
        ChangeState(AIState.inView);
        SetPrefab();
        FindDirection(playerTarget);
        transform.rotation = Quaternion.LookRotation(direction);
    }

    public enum AIState {
        inView, inAttackRange, attacking
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == Tags.PLAYER) {
            other.GetComponent<PlayerDeath>().DamagePlayer(damage, gameObject.transform);
        }
    }
}