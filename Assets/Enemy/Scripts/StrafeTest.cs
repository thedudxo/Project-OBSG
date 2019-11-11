using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrafeTest : MonoBehaviour {

    [SerializeField] GameObject player;
    [SerializeField] float speed = 5000;
    Vector3 direction;
    Vector3 rotDirection;
    Vector3 moveVec;
    Vector3 newMoveVec;

    void Start() {
        
    }
    
    void Update() {
        RotateTowardsTarget();
        FindDirection(player.transform);
        Strafe();
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

    void Strafe() {
        var perpendicularVec = Vector3.Cross(Vector3.up, player.transform.position);
        moveVec = perpendicularVec.normalized;
        moveVec.y = 0;
        Debug.Log(moveVec);
        newMoveVec = moveVec * speed;
        transform.Translate(newMoveVec * Time.fixedDeltaTime);
    }
}
