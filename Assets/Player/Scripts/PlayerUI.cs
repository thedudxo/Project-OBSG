using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {
    
    [Header("Health:")]
    [SerializeField]
    RectTransform healthFill;

    [Header("Keys:")]
    [SerializeField]
    bool stop;//ignore this
    [SerializeField]
    Image 
        blueKey,
        redKey,
        yellowKey;
    [SerializeField]
    GameObject
        bluePrefab,
        redPrefab,
        yellowPrefab;

    [Header("Throw:")]
    [SerializeField]
    Image crosshair;

    private void Update() {
        SetHealthAmount(PlayerManager.health/PlayerManager.maxHealth);
        if (PlayerManager.throwing) {
            crosshair.enabled = true;
        } else {
            crosshair.enabled = false;
        }
    }

    void SetHealthAmount(float amount) {
        healthFill.localScale = new Vector3(amount, 1, 1);
    }

    public void GetKeyImages() {
        if (PlayerManager.doorKeys.Contains(bluePrefab)) {
            blueKey.enabled = true;
        }

        if (PlayerManager.doorKeys.Contains(redPrefab)) {
            redKey.enabled = true;
        }

        if (PlayerManager.doorKeys.Contains(yellowPrefab)) {
            yellowKey.enabled = true;
        }
    }
}