using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {

    [SerializeField] Attack[] weapons;
    [SerializeField] BoxCollider AttackCollider;
    int currentWeaponIndex;
    int newWeaponIndex;

    private void Start() {
        currentWeaponIndex = 0;
        weapons[currentWeaponIndex].gameObject.SetActive(true);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            SelectWeapon(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            SelectWeapon(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            SelectWeapon(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            SelectWeapon(3);
        }
    }

    public void Unequip() {
        weapons[currentWeaponIndex].gameObject.SetActive(false);
        weapons[newWeaponIndex].gameObject.SetActive(true);
        currentWeaponIndex = newWeaponIndex;
    }

    void SelectWeapon(int weaponIndex) {
        if (weapons[weaponIndex].canUse) {
            newWeaponIndex = weaponIndex;
            weapons[currentWeaponIndex].gameObject.GetComponent<Animator>().SetTrigger(PlayerAnimation.UNEQUIP);
            //AttackCollider.size = weapons[weaponIndex].colliderSize;
            //Debug.Log("Equip " + s);
        } else {
            //Debug.Log("Player does not have " + s);
        }
    }

    public void UnlockWeapon(int i) {
        weapons[i].canUse = true;
        SelectWeapon(i);
    }
}
