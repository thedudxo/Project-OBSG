using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeBeam : MonoBehaviour {

    Vector3 direction;
    [SerializeField] LineRenderer leftEye;
    [SerializeField] LineRenderer rightEye;
    public Quaternion rot;
    Transform player;

    private void Start() {
        player = GameObject.FindGameObjectWithTag(Tags.PLAYER).transform;
        rot = transform.localRotation;
    }

    private void Update() {
        FindDirection(player);
    }

    public void RotateTowardsPlayer() {
        direction.y = direction.y + 1.5f;
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 0.5f);
    }

    public void UpdateLength(RaycastHit hit) {
        leftEye.SetPosition(1, new Vector3(0, 0, hit.distance + 20));
        rightEye.SetPosition(1, new Vector3(0, 0, hit.distance + 20));
    }

    void RotateEyes(RaycastHit hit) {
        transform.localRotation = Quaternion.LookRotation(hit.point);
    }

    void FindDirection(Transform target) {
        direction = target.position - transform.position;
    }
}
