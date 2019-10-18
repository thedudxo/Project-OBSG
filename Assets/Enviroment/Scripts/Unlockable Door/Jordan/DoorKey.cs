using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class DoorKey : MonoBehaviour {

    [SerializeField] GameObject particlesObject;
    VisualEffect vfx;
    Light vfxLight;
    bool pickedup;

    private void Start()
    {
        
        vfx = particlesObject.GetComponent<VisualEffect>();
        vfxLight = particlesObject.GetComponentInChildren<Light>();
        particlesObject.transform.parent = null;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == Tags.PLAYER) {
            PlayerManager.doorKeys.Add(gameObject);
            //other.GetComponent<PlayerUI>().GetKeyImages();
            gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
            vfx.SendEvent(WeaponParticles.PICK_UP);
            pickedup = true;

            PlayerManager.keys++;
        }
    }

    private void Update()
    {
        

        if(pickedup)
        {
            if (vfxLight.intensity > 0)
            {
                float intensity = vfxLight.intensity;
                intensity -= 1f;
                vfxLight.intensity = intensity;
            }
            else
            {
                vfxLight.intensity = 0;
            }
            if (vfx.GetFloat(WeaponParticles.ALPHA) > 0)
            {
                float alpha = vfx.GetFloat(WeaponParticles.ALPHA);
                alpha -= 0.05f;
                vfx.SetFloat(WeaponParticles.ALPHA, alpha);
            }
            else
            {
                vfx.SetFloat(WeaponParticles.ALPHA, 0);
            }
            
        }
        else
        {
            vfx.SendEvent(WeaponParticles.RESET);
        }
    }
}
