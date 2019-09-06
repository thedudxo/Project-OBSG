using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {

    [SerializeField] Transform 
        playerRoot, 
        lookRoot;

    [SerializeField] bool 
        invert, 
        can_Unlock = true,
        aimAssist;

    [SerializeField] float 
        sensitivity = 5f, 
        smoothWeight = 0.4f, 
        rollAngle = 10f,
        rollSpeed = 3f;

    [SerializeField] int smoothSteps = 10;

    [SerializeField] Vector2 defaultLookLimits = new Vector2(-70f, 80f);

    private Vector2 
        lookAngles, 
        currentMouseLook, 
        smoothMove;

    private float currentRollAngle;

    private int lastLookFrame;

	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        Application.targetFrameRate = 60;
	}
	
	void Update () {
        if (!PlayerManager.alive) { return; }
        LockAndUnlockCursor();
        if (Cursor.lockState == CursorLockMode.Locked) {
            LookAround();
        }
    }

    void LockAndUnlockCursor() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (Cursor.lockState == CursorLockMode.Locked) {
                Cursor.lockState = CursorLockMode.None;
            } else {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    void LookAround() {
        currentMouseLook = new Vector2(Input.GetAxis(MouseAxis.MOUSE_Y), Input.GetAxis(MouseAxis.MOUSE_X));
        lookAngles.x += currentMouseLook.x * sensitivity * (invert ? 1f : -1f);
        lookAngles.y += currentMouseLook.y * sensitivity;

        lookAngles.x = Mathf.Clamp(lookAngles.x, defaultLookLimits.x, defaultLookLimits.y);

        currentRollAngle = Mathf.Lerp(currentRollAngle, Input.GetAxisRaw(MouseAxis.MOUSE_X) * rollAngle, Time.deltaTime * rollSpeed);

        lookRoot.localRotation = Quaternion.Euler(lookAngles.x, 0f, currentRollAngle);
        playerRoot.localRotation = Quaternion.Euler(0f, lookAngles.y, 0f);
    }
}
