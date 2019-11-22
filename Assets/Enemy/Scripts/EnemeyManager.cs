using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeyManager : MonoBehaviour {

    [SerializeField] GameObject enemyPrefab;

    private Stack<GameObject> enemy = new Stack<GameObject>();
    public Stack<GameObject> Enemy {
        get { return enemy; }
        set { enemy = value; }
    }
    
    private static EnemeyManager instance;
    public static EnemeyManager Instance {
        get {
            if (instance == null) {
                instance = GameObject.FindObjectOfType<EnemeyManager>();
            }
            return EnemeyManager.instance;
        }
    }
    void Start() {
        CreateEnemy(30);
    }

    void CreateEnemy(int amount) {
        for (int i = 0; i < amount; i++) {
            enemy.Push(Instantiate(enemyPrefab));
            enemy.Peek().name = "Base Enemy";
            enemy.Peek().SetActive(false);
        }
    }

    public void SpawnEnemy(Transform location, LockDoorOnTriggerEnter script) {
        GameObject tmp = enemy.Pop();
        tmp.transform.position = location.position;
        tmp.SetActive(true);
        tmp.transform.parent = null;
        tmp.GetComponent<SpawnAI>().AISpawn();
        tmp.GetComponent<EnemyDeathScript>().dead = false;
        tmp.GetComponent<EnemyDeathScript>().health = 50;
        tmp.GetComponent<EnemyDeathScript>().spawned = script;
        tmp.GetComponent<Animator>().enabled = true;
        tmp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        tmp.GetComponent<CapsuleCollider>().enabled = true;
        foreach (Rigidbody r in tmp.GetComponentsInChildren<Rigidbody>()) {
            r.isKinematic = true;
        }
        script.enemies.Add(tmp.GetComponent<SpawnAI>());
    }
}
