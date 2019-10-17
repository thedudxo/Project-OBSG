using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class WeaponHandler : MonoBehaviour {
    
    VisualEffect vfx;
    Light vfxLight;
    [SerializeField] bool pickup;
    [SerializeField] GameObject particlesObject;
    [SerializeField] int weaponHoldIndex;
    public GameObject special;

    private void Start() {
        particlesObject.transform.parent = null;
        vfx = particlesObject.GetComponent<VisualEffect>();
        vfxLight = particlesObject.GetComponentInChildren<Light>();
    }

    private void Update() {
        if (pickup) {
            if (vfxLight.intensity > 0) {
                float intensity = vfxLight.intensity;
                intensity -= 1f;
                vfxLight.intensity = intensity;
            } else {
                vfxLight.intensity = 0;
            }
            if (vfx.GetFloat(WeaponParticles.ALPHA) > 0) {
                float alpha = vfx.GetFloat(WeaponParticles.ALPHA);
                alpha -= 0.05f;
                vfx.SetFloat(WeaponParticles.ALPHA, alpha);
            } else {
                vfx.SetFloat(WeaponParticles.ALPHA, 0);
            }
            if(vfx.GetFloat(WeaponParticles.ALPHA) == 0 && vfxLight.intensity == 0) {
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == Tags.PLAYER) {
            other.GetComponent<WeaponManager>().UnlockWeapon(weaponHoldIndex);
            gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
            vfx.SendEvent(WeaponParticles.PICK_UP);
            pickup = true;
        }
    }//Trigger enter
}
