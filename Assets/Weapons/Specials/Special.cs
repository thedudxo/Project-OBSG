using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class Special : MonoBehaviour
{

    //----------------------Debug------------------------//
    
//   float resetTime;
//   float resetTimer = 2;
//   GameObject effect;
//   Vector3 position;
//   [SerializeField] float speed = 5f;
//   
//   private void Start() {
//       resetTime = Time.time;
//       position = transform.position;
//       effect = gameObject;
//       GetComponentInChildren<VisualEffect>().SendEvent("OnPlay");
//   }
//
//   private void Update() {
//       if(resetTime + resetTimer <= Time.time) {
//           transform.position = position;
//           resetTime = Time.time;
//           GetComponentInChildren<VisualEffect>().SendEvent("OnPlay");
//       }
//       transform.Translate(Vector3.forward * Time.deltaTime * speed);
//   }
    
    //---------------------------Gameplay------------------------------//
    
    [SerializeField] float speed = 5f;
    public bool active = false;

    private void Start() {
    }

    private void Update() {
        if (active) {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
    }
}
