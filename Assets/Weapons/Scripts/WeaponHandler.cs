using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType {
    ONE_HANDED,
    TWO_HANDED
}

public class WeaponHandler : MonoBehaviour {

    private Rigidbody rb;
    bool resetting = false;
    bool thrown = false;
    bool reset = true;
    [SerializeField] Vector3 resetRot;
    [SerializeField] Vector3 resetScale;
    public Vector3 holdPosition;
    public Vector3 holdRotation;
    public Vector3 holdScale;
    public int damage;
    public bool throwable;
    public WeaponType weaponType;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        if(resetting) { return; }
        if (thrown) {
            if(rb.velocity == Vector3.zero)
                StartCoroutine(ResetWeapon());
        }
        if (reset) {
            transform.Rotate(0, 60 * Time.deltaTime, 0, Space.World);
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if (thrown) {
            if (collision.gameObject.tag == Tags.ENEMY) {
                collision.gameObject.GetComponent<EnemyDeathScript>().DealDamage(damage);
            }
        }
    }

    public void ThrowWeapon() {
        thrown = true;
        gameObject.transform.localScale = resetScale;
        rb.constraints = RigidbodyConstraints.None;
        rb.useGravity = true;
        transform.localEulerAngles = Vector3.zero;
        rb.angularVelocity = rb.transform.right * 10;
        transform.GetChild(1).GetComponent<Collider>().enabled = true;
    }
    
    IEnumerator ResetWeapon() {
        resetting = true;
        yield return new WaitForSeconds(1);
        if (rb.velocity == Vector3.zero) {
            var pos = transform.position;
            pos.y = pos.y + 1.3f;
            transform.position = pos;
            transform.eulerAngles = resetRot;
            rb.useGravity = false;
            GetComponent<Collider>().enabled = true;
            transform.GetChild(1).GetComponent<Collider>().enabled = false;
            thrown = false;
            resetting = false;
            reset = true;
        } else {
            StartCoroutine(ResetWeapon());
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == Tags.PLAYER) {
            switch (weaponType) {
                case WeaponType.ONE_HANDED:
                    if (other.GetComponent<Pickup>().rightHandWeapon == null || other.GetComponent<Pickup>().leftHandWeapon == null) {
                        other.GetComponent<Pickup>().PickUpObject(gameObject);
                        reset = false;
                    }
                    break;
                case WeaponType.TWO_HANDED:
                    if (other.GetComponent<Pickup>().rightHandWeapon == null && other.GetComponent<Pickup>().leftHandWeapon == null) {
                        other.GetComponent<Pickup>().PickUpObject(gameObject);
                        reset = false;
                    }
                    break;
            }
        }
    }//Trigger enter
}
