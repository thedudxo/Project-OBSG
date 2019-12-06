using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCanvas : MonoBehaviour
{

    [SerializeField] Animator dead;

    public void DeathScreen() {
        dead.SetTrigger("Dead");
    }
}
