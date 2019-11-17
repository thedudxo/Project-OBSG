using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour {

    [SerializeField] Transform 
        playerRoot, 
        lookRoot;

    [SerializeField] bool
        aimAssist;

    [SerializeField] float
        rollAngle = 10f,
        rollSpeed = 3f;

    [SerializeField] Vector2 defaultLookLimits = new Vector2(-70f, 80f);

    private Vector2 
        lookAngles, 
        currentMouseLook, 
        smoothMove;

    private float 
        currentRollAngle,
        xAxisClamp;

    private int lastLookFrame;

	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        xAxisClamp = 0;
        Application.targetFrameRate = 60;
	}
	
	void Update () {
        if (!PlayerManager.alive) { return; }
        //LockAndUnlockCursor();
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
        lookAngles.x = Input.GetAxis(MouseAxis.MOUSE_Y) * PlayerManager.sensitivity * 50 * Time.deltaTime * (PlayerManager.invert ? -1f : 1f);
        lookAngles.y = Input.GetAxis(MouseAxis.MOUSE_X) * PlayerManager.sensitivity * 50 * Time.deltaTime;

        xAxisClamp += lookAngles.x;

        if(xAxisClamp > 80) {
            xAxisClamp = 80;
            lookAngles.x = 0;
            ClampXAxis(280);
        } else if (xAxisClamp < -70) {
            xAxisClamp = -70;
            lookAngles.x = 0;
            ClampXAxis(70);
        }
        transform.Rotate(Vector3.left * lookAngles.x);
        playerRoot.Rotate(Vector3.up * lookAngles.y);
    }

    void ClampXAxis(float value) {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }
}
//        float mouseX = Input.GetAxis(MouseAxis.MOUSE_X);
//        float mouseY = Input.GetAxis(MouseAxis.MOUSE_Y);
//
//        lookAngles.y = mouseX * PlayerManager.sensitivity;
//        lookAngles.x = mouseY * PlayerManager.sensitivity * (PlayerManager.invert ? -1f : 1f);
//
//        lookAngles.x = Mathf.Clamp(lookAngles.x, defaultLookLimits.x, defaultLookLimits.y);
//
//        Vector3 targetRotCam = transform.rotation.eulerAngles;
//        Vector3 targetRotBody = playerRoot.rotation.eulerAngles;
//
//        currentRollAngle = Mathf.Lerp(currentRollAngle, Input.GetAxisRaw(MouseAxis.MOUSE_X) * rollAngle, Time.deltaTime * rollSpeed);
//
//        targetRotCam.x -= lookAngles.x;
//        targetRotCam.z = currentRollAngle;
//        targetRotBody.y += lookAngles.y;
//
//        transform.rotation = Quaternion.Euler(targetRotCam);
//        playerRoot.rotation = Quaternion.Euler(targetRotBody);

/*
        currentMouseLook = new Vector2(Input.GetAxis(MouseAxis.MOUSE_Y), Input.GetAxis(MouseAxis.MOUSE_X));
        lookAngles.x += currentMouseLook.x * PlayerManager.sensitivity * (PlayerManager.invert ? 1f : -1f);
        lookAngles.y += currentMouseLook.y * PlayerManager.sensitivity;

        lookAngles.x = Mathf.Clamp(lookAngles.x, defaultLookLimits.x, defaultLookLimits.y);


        lookRoot.localRotation = Quaternion.Euler(lookAngles.x, 0f, currentRollAngle);
        playerRoot.localRotation = Quaternion.Euler(0f, lookAngles.y, 0f);
        */