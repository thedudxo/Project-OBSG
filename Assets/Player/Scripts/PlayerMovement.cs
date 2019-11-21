using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class PlayerMovement : MonoBehaviour {

    [SerializeField] private float accel = 1000;
    [SerializeField] private float airAccel = 0.1f;
    [SerializeField] private float deccel = 5;
    [SerializeField] private float maxSpeed = 20;
    //[SerializeField] private float jumpForce = 800;
    [SerializeField] private float maxSlope = 60;
    [SerializeField] private float dashSpeed = 6000;
    [SerializeField] private List<Animator> animator = new List<Animator>();
    [SerializeField] private VisualEffect dust;
    bool dashing = false;
    public bool frontDash = false;
    private bool stopDash = false;
    private Rigidbody rb;
    private Vector2 horizontalMovement;
    public bool grounded = false;
    private float deccelX = 0;
    private float deccelZ = 0;


    void Start() {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Physics.IgnoreLayerCollision(10, 9);
    }

    private void FixedUpdate() {
        if (dashing || !PlayerManager.alive) { return; }
        //Max Speed
        horizontalMovement = new Vector2(rb.velocity.x, rb.velocity.z);
        if (horizontalMovement.magnitude > maxSpeed)  {
            horizontalMovement = horizontalMovement.normalized * maxSpeed;
        }
        rb.velocity = new Vector3(horizontalMovement.x, rb.velocity.y, horizontalMovement.y);
        //Decceleration
        var vel = rb.velocity;
        if (grounded) {
            vel.x = Mathf.SmoothDamp(vel.x, 0, ref deccelX, deccel);
            vel.z = Mathf.SmoothDamp(vel.z, 0, ref deccelZ, deccel);
            rb.velocity = vel;
        }
        //Movement
        if (grounded) {
            rb.AddRelativeForce(Input.GetAxis(Axis.HORIZONTAL) * accel * Time.deltaTime, 0, Input.GetAxis(Axis.VERTICAL) * accel * Time.deltaTime, ForceMode.VelocityChange);
        } else {
            rb.AddRelativeForce(Input.GetAxis(Axis.HORIZONTAL) * accel * airAccel * Time.deltaTime, 0, Input.GetAxis(Axis.VERTICAL) * accel * airAccel * Time.deltaTime, ForceMode.Force);
        }
        //Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && grounded && !stopDash) {
            StartCoroutine(Dash(new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0, Input.GetAxis(Axis.VERTICAL))));
        }
    }

    private IEnumerator Dash(Vector3 direction) {
        dashing = true;
        stopDash = true;
        if (direction == Vector3.forward)
            frontDash = true;
        rb.AddRelativeForce(direction * dashSpeed * Time.deltaTime, ForceMode.VelocityChange);
        yield return new WaitForSeconds(0.2f);
        dashing = false;
        frontDash = false;
        yield return new WaitForSeconds(0.1f);
        stopDash = false;
    }

    private void Update() {
        if (dashing || !PlayerManager.alive) { return; }
        //Animation
        foreach (Animator anim in animator) {
            if (anim.enabled) {
                anim.SetFloat(PlayerAnimation.WALK_BLEND, horizontalMovement.magnitude);
            }
        }
        //GetComponentInChildren<Animator>().SetFloat(PlayerAnimation.WALK_BLEND, horizontalMovement.magnitude);
        //Jump
        //if (Input.GetButtonDown(Axis.JUMP) && grounded) {
        //    rb.AddForce(0, jumpForce, 0);
        //}
    }

    private void OnCollisionStay(Collision collision) {
        foreach (ContactPoint contact in collision.contacts) {
            if (Vector3.Angle(contact.normal, Vector3.up) < maxSlope) {
                grounded = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision) {
        grounded = false;
    }

    private void OnTriggerEnter(Collider other) {
        var vel = rb.velocity;
        if(other.tag == Tags.UP_STAIR) {
            if (grounded && Vector3.Angle(rb.velocity, other.transform.forward) < 90) {
                if (rb.velocity.y > 0) {
                    Debug.Log("UpTrigger");
                    vel.y = 0;
                    rb.velocity = vel;
                }
            }
        }
        if (other.transform.tag == Tags.DOWN_STAIR) {
            if (grounded && Vector3.Angle(rb.velocity, other.transform.forward) < 90) {
                Debug.Log("DownTrigger");
                rb.AddForce(0, -1000, 0);
            }
        }
    }
}
