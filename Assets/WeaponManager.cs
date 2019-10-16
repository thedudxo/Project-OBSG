using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour {

    [SerializeField] WeaponHandler[] weapons;
    int currentWeaponIndex;

    private void Start() {
        //currentWeaponIndex = 0;
        //weapons[currentWeaponIndex].gameObject.SetActive(true);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            SelectWeapon(0, "fist");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            SelectWeapon(1, "Sword");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            SelectWeapon(2, "Spear");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            SelectWeapon(3, "club");
        }
    }

    void SelectWeapon(int weaponIndex, string s) {
        //weapons[currentWeaponIndex].gameObject.SetActive(false);
        //weapons[weaponIndex].gameObject.SetActive(true);
        //currentWeaponIndex = weaponIndex;
        Debug.Log(s);
    }
}
