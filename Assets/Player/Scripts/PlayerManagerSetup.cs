using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagerSetup : MonoBehaviour {

    //sets up the playermanager class with the right shtuffs

	// Use this for initialization
	void Awake () {

        PlayerManager.playerAnimations = GetComponent<PlayerAnimations>();
        PlayerManager.playerMovement = GetComponent<PlayerMovement>();
        PlayerManager.camera = GetComponentInChildren<Camera>();
        PlayerManager.player = gameObject;
        PlayerManager.respawnPoint = transform.position;
        PlayerManager.playerDeath = GetComponent<PlayerDeath>();
    }

    private void Update()
    {
        PlayerManager.SecondsPlayerAlive += Time.deltaTime;
    }
}
