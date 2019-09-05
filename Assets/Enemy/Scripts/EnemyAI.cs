using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour{

    [SerializeField] AIState aiState;
    [SerializeField] Transform playerTarget;
    [SerializeField] BoxCollider fist;
    [SerializeField] float damage = 10;
    Vector3 direction;
    Vector3 rotDirection;
    bool isInAngle;
    bool isClear;
    float distance;
    float viewRadius = 20;
    float maxDistance = 60;
    float viewAngle = 110;
    int lFrame = 15;
    int lFrame_counter = 0;
    int llFrame = 35;
    int llFrame_counter = 0;

    delegate void EveryFrame();
    EveryFrame everyFrame;
    delegate void LateFrame();
    LateFrame lateFrame;
    delegate void LateLateFrame();
    LateLateFrame llateFrame;

    NavMeshAgent agent;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        aiState = AIState.idle;
        ChangeState(AIState.idle);
    }

    private void Update() {
        if (GetComponent<EnemyDeathScript>().dead) { return; }
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
            case AIState.idle:
                lateFrame = IdleBehaviours;
                break;
            case AIState.lateIdle:
                llateFrame = IdleBehaviours;
                break;
            case AIState.inRadius:
                lateFrame = InRadiusBehaviours;
                break;
            case AIState.inView:
                lateFrame = InRadiusBehaviours;
                everyFrame = InViewBehaviours;
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
        AttackTarget();
    }

    void DistanceCheck(Transform target) {
        distance = Vector3.Distance(transform.position, target.position);
    }

    void IsClearView(Transform target) {
        isClear = false;
        RaycastHit hit;
        Vector3 origin = transform.position + Vector3.up;
        if (Physics.Raycast(origin, direction, out hit, viewRadius)) {
            if (hit.transform.CompareTag(Tags.PLAYER)) {
                isClear = true;
            }
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
        if(agent.remainingDistance <= 1.7f) {
            Debug.Log(agent.remainingDistance);
            GetComponent<Animator>().SetTrigger(EnemyAnimation.ENEMY_ATTACK);
        }
    }

    void ActivateFist() {
        fist.enabled = true;
    }

    void DeactivateFist() {
        fist.enabled = false;
    }

    public enum AIState {
        idle, lateIdle, inRadius, inView
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == Tags.PLAYER) {
            other.GetComponent<PlayerDeath>().DamagePlayer(damage);
        }
    }
}
