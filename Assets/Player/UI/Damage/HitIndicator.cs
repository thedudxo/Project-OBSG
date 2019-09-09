using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitIndicator : MonoBehaviour {

    [Header("Damage:")]
    public Transform target;
    [SerializeField]
    Transform arrow;
    [SerializeField]
    Transform player;
    float angle;

    private void Update() {
        Indicator();
    }
    void Indicator() {
        angle = GetHitAngle(player, (player.position - target.position).normalized);
        arrow.rotation = Quaternion.Euler(0, 0, -angle);
    }

    float GetHitAngle(Transform player, Vector3 incomingDir) {
        var otherDir = new Vector3(-incomingDir.x, 0f, -incomingDir.z);
        var playerFwd = Vector3.ProjectOnPlane(player.forward, Vector3.up);
        var angle = Vector3.SignedAngle(playerFwd, otherDir, Vector3.up);
        return angle;
    }
}
