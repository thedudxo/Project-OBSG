using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedEnemy : MonoBehaviour{

    public Transform playerTarget;
    [SerializeField] Transform playerPrefab;
    public bool isInAngle;
    public bool isClear;
    public AIState aiState;
    [SerializeField] BoxCollider fist;
    [SerializeField] float damage = 10;
    Vector3 direction;
    Vector3 rotDirection;
    Vector3 moveVec;
    Vector3 newMoveVec;
    float distance;
    [SerializeField] float strafeSpeed;
    [SerializeField] float viewRadius = 20;
    [SerializeField] float attackRadius = 6;
    [SerializeField] float minAttackRadius = 4;
    float maxDistance = 60;
    float viewAngle = 210;
    bool attacking = false;
    bool preperedAttack = false;
    int layerMask = 1 << 10;
    int lFrame = 15;
    int lFrame_counter = 0;
    int llFrame = 35;
    int llFrame_counter = 0;
    int attackFrame;
    int attackFrame_counter = 0;

    delegate void EveryFrame();
    EveryFrame everyFrame;
    delegate void LateFrame();
    LateFrame lateFrame;
    delegate void LateLateFrame();
    LateLateFrame llateFrame;
    delegate void AttackFrame();
    LateLateFrame aFrame;

    NavMeshAgent agent;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        Respawn.enemies.Add(gameObject);
        aiState = AIState.idle;
        ChangeState(AIState.idle);
        SetPrefab();
        attackFrame = Random.Range(0, 180);
    }

    public void SetPrefab() {
        playerTarget = playerPrefab;
    }

    private void Update() {
        if (GetComponent<EnemyDeathScript>().dead) { return; }
        //layerMask = ~layerMask;
        Debug.DrawRay(transform.position + Vector3.up, direction * viewRadius, Color.red);
        if (!PlayerManager.alive) {
            agent.isStopped = true;
            return;
        }
        MonitorStates();
        if (everyFrame != null)
            everyFrame();
        lFrame_counter++;
        if(lFrame_counter > lFrame) {
            if (lateFrame != null)
                lateFrame();
            lFrame_counter = 0;
        }
        llFrame_counter++;
        if (llFrame_counter > llFrame)
        {
            if (llateFrame != null)
                llateFrame();
            llFrame_counter = 0;
        }
        attackFrame_counter++;
        if(attackFrame_counter > attackFrame) {
            if (aFrame != null)
                aFrame();
            attackFrame = Random.Range(0, 180);
            attackFrame_counter = 0;
        }
    }

    void MonitorStates() {
        switch (aiState) {
            case AIState.idle:
                if (distance < viewRadius)
                    ChangeState(AIState.inRadius);
                if (distance > maxDistance)
                    ChangeState(AIState.lateIdle);
                break;
            case AIState.lateIdle:
                if (distance < maxDistance)
                    ChangeState(AIState.idle);
                break;
            case AIState.inRadius:
                if (distance > viewRadius)
                    ChangeState(AIState.idle);
                if (isClear && isInAngle)
                    ChangeState(AIState.inView);
                break;
            case AIState.inView:
                if (distance > viewRadius)
                    ChangeState(AIState.inRadius);
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
        llateFrame = null;
        switch (targetState) {
            case AIState.lateIdle:
                llateFrame = IdleBehaviours;
                break;
            case AIState.idle:
                lateFrame = IdleBehaviours;
                break;
            case AIState.inRadius:
                lateFrame = InRadiusBehaviours;
                break;
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

    void IdleBehaviours() {
        if (playerTarget == null)
            return;
        DistanceCheck(playerTarget);
    }

    void InRadiusBehaviours(){
        if (playerTarget == null)
            return;
        DistanceCheck(playerTarget);
        FindDirection(playerTarget);
        AngleCheck();
        IsClearView(playerTarget); 
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
    }

    void StopDestination() {
        agent.destination = transform.position;
    }

    void DistanceCheck(Transform target) {
        distance = Vector3.Distance(transform.position, target.position);
    }

    void IsClearView(Transform target) {
        isClear = false;
        RaycastHit hit;
        Vector3 origin = transform.position + Vector3.up;
        if (Physics.Raycast(origin, direction, out hit, viewRadius, layerMask)) {
            isClear = true;
        }
    }

    void AngleCheck() {
        isInAngle = false;
        Vector3 rotDirection = direction;
        rotDirection.y = 0;
        if (rotDirection == Vector3.zero)
            rotDirection = transform.forward;
        float angle = Vector3.Angle(transform.forward, rotDirection);
        isInAngle = (angle < viewAngle / 2);
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
    }

    void AttackTarget() {
        if (preperedAttack) {
            ChangeState(AIState.attacking);
        }
    }

    void AttackAnimation() {
        Debug.Log(distance);
        if (distance <= 2f) {
            Debug.Log("Attack");
            GetComponent<Animator>().SetBool(EnemyAnimation.ENEMY_ATTACK, true);
            agent.destination = transform.position;
        }
    }

    void Strafe() {
        if (distance < attackRadius && distance > minAttackRadius) {
            preperedAttack = true;
            agent.Move(transform.right * strafeSpeed * Time.fixedDeltaTime);
        }
    }

    void Retreat() {
        if(distance < minAttackRadius) {
            Debug.Log("Move Back");
            agent.Move(-transform.forward * strafeSpeed * Time.fixedDeltaTime);
            preperedAttack = false;
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
    }

    public enum AIState {
        idle, lateIdle, inRadius, inView, inAttackRange, attacking
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == Tags.PLAYER) {
            other.GetComponent<PlayerDeath>().DamagePlayer(damage, gameObject.transform);
        }
    }
}