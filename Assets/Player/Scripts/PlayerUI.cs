using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {
    
    [Header("Blood Meter:")]
    [SerializeField]
    RectTransform bloodFill;

    [Header("Health:")]
    [SerializeField]
    Image redScreen;

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
        SetBloodAmount(PlayerManager.bloodMeter/PlayerManager.maxBloodMeter);
        if (PlayerManager.throwing) {
            crosshair.enabled = true;
        } else {
            crosshair.enabled = false;
        }
    }

    void SetBloodAmount(float amount) {
        bloodFill.localScale = new Vector3(amount, 1, 1);
    }

    void SetHealthAmount(float amount) {
        redScreen.color = new Color(1, 1, 1, -(amount - 1));
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