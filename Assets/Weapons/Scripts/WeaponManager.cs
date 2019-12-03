using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class WeaponManager : MonoBehaviour {

    [SerializeField] readonly bool bloodMeterDebug = false;

    [SerializeField] Attack[] weapons;
    [SerializeField] BoxCollider AttackCollider;
    [SerializeField] Camera cam;
    [SerializeField] VisualEffect specialEffect;
    [SerializeField] List<Material> specialMats = new List<Material>();
    int currentWeaponIndex;
    int newWeaponIndex;

    float bloodMeterDecrease = 10f;

    private void Start() {
        foreach (Material m in specialMats) {
            m.SetFloat("Vector1_6E014F53", 0);
        }
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
        if (Input.GetKeyDown(KeyCode.Q)) {
            if (PlayerManager.special) {
                StopAllCoroutines();
                StartCoroutine(AnimateMaterials(0, specialMats[0].GetFloat("Vector1_6E014F53")));
                specialEffect.SendEvent("Exit");
                PlayerManager.special = false;
            } else {
                specialEffect.SendEvent("Start");
                StopAllCoroutines();
                StartCoroutine(AnimateMaterials(2, specialMats[0].GetFloat("Vector1_6E014F53")));
                PlayerManager.special = true;
            }
        }
        if (PlayerManager.special) {
            if (cam.fieldOfView < 65) {
                cam.fieldOfView += 0.1f;
            } else {
                cam.fieldOfView = 65;
            }
            //if (!bloodMeterDebug)
            //{
            //    PlayerManager.bloodMeter -= Time.deltaTime * bloodMeterDecrease;
            //    if (PlayerManager.bloodMeter <= 0)
            //    {
            //        PlayerManager.bloodMeter = 0;
            //        PlayerManager.special = false;
            //    }
            //}
        } else {
            if (cam.fieldOfView > 60) {
                cam.fieldOfView -= 0.1f;
            } else {
                cam.fieldOfView = 60;
            }
        }
    }

    IEnumerator AnimateMaterials(float to, float from) {
        float lerp = 0;
        while (lerp <= 1) {
            foreach(Material m in specialMats) {
                m.SetFloat("Vector1_6E014F53", Mathf.Lerp(from, to, lerp));
            }
            lerp += Time.deltaTime * 0.8f;
            yield return lerp;
        }
        foreach (Material m in specialMats) {
            m.SetFloat("Vector1_6E014F53", to);
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
