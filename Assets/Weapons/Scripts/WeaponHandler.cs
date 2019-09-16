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
    }

    private void OnCollisionEnter(Collision collision) {
        if (thrown) {
            if (collision.gameObject.tag == Tags.ENEMY) {
                collision.gameObject.GetComponent<EnemyDeathScript>().DealDamage(damage);
            }
        }
    }

    public IEnumerator ThrowWeapon() {
        thrown = true;
        gameObject.transform.localScale = resetScale;
        rb.constraints = RigidbodyConstraints.None;
        rb.useGravity = true;
        //yield return new WaitForEndOfFrame();
        transform.GetChild(1).GetComponent<Collider>().enabled = true;
        yield return null;
    }
    
    IEnumerator ResetWeapon() {
        resetting = true;
        yield return new WaitForSeconds(1);
        var pos = transform.position;
        pos.y = 1.4f;
        transform.position = pos;
        transform.eulerAngles = resetRot;
        rb.useGravity = false;
        GetComponent<Collider>().enabled = true;
        transform.GetChild(1).GetComponent<Collider>().enabled = false;
        thrown = false;
        resetting = false;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == Tags.PLAYER) {
            switch (weaponType) {
                case WeaponType.ONE_HANDED:
                    if (other.GetComponent<Pickup>().rightHandWeapon == null || other.GetComponent<Pickup>().leftHandWeapon == null) {
                        other.GetComponent<Pickup>().PickUpObject(gameObject);
                    }
                    break;
                case WeaponType.TWO_HANDED:
                    if (other.GetComponent<Pickup>().rightHandWeapon == null && other.GetComponent<Pickup>().leftHandWeapon == null) {
                        other.GetComponent<Pickup>().PickUpObject(gameObject);
                    }
                    break;
            }
        }
    }//Trigger enter
}
