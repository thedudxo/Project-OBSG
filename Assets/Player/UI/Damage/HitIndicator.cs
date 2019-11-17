using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitIndicator : MonoBehaviour {

    [Header("Damage:")]
    public Transform target;
    [SerializeField]
    Transform arrow;
    [SerializeField]
    Transform player;
    [SerializeField]
    float transitionSpeed;
    float timer = 1;
    Image image;
    Color visible = new Color(1, 1, 1, 1);
    Color invisible = new Color(1, 1, 1, 0);
    float startTimer = 0;
    float angle;
    bool damage = false;

    private void Awake() {
        image = gameObject.GetComponent<Image>();
    }

    private void Update() {
        if (damage) {
            Indicator();
        }
    }
    void Indicator() {
        angle = GetHitAngle(player, (player.position - target.position).normalized);
        arrow.localRotation = Quaternion.Euler(0, 0, -angle);
    }

    float GetHitAngle(Transform player, Vector3 incomingDir) {
        var otherDir = new Vector3(-incomingDir.x, 0f, -incomingDir.z);
        var playerFwd = Vector3.ProjectOnPlane(player.forward, Vector3.up);
        var angle = Vector3.SignedAngle(playerFwd, otherDir, Vector3.up);
        return angle;
    }

    public IEnumerator TransitionOut() {
        damage = true;
        startTimer = 0;
        while (startTimer <= timer) {
            image.color = Color.Lerp(visible, invisible, startTimer);
            startTimer += Time.deltaTime * transitionSpeed;
            yield return startTimer;
        }
        DamageManager.Instance.Indicator.Push(gameObject);
        gameObject.SetActive(false);
        damage = false;
    }
}
