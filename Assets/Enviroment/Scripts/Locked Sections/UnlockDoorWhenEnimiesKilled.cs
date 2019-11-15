using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoorWhenEnimiesKilled : MonoBehaviour
{

    [SerializeField] GameObject[] enemiesToKill;
    [SerializeField] GameObject thingToActivate;
    [SerializeField] bool enableThing = false;

    // Update is called once per frame
    void Update()
    {
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
        }
    }
}
