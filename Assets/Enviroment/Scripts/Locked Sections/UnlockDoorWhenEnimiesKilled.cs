using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoorWhenEnimiesKilled : MonoBehaviour
{

    [SerializeField] GameObject[] enemiesToKill;
    [SerializeField] GameObject thingToActivate;
    [SerializeField] bool enableThing = false;

    bool done = false;

    // Update is called once per frame
    void Update()
    {
        if (done) { return; }

        int deadEnemies = 0;
        foreach (GameObject enemy in enemiesToKill){
            if (enemy.GetComponent<EnemyDeathScript>().dead)
            {
                deadEnemies++;
            }
        }

        if(deadEnemies >= enemiesToKill.Length)
        {
            thingToActivate.SetActive(enableThing);
            Debug.Log("unlocking" + thingToActivate);
            done = true;
        }
    }
}
