using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    #region AnimationFlags
    private bool IsRunning = false;
    private bool IsWalking = false;
    private int MovementDirection = 0;
    private bool IsFire = false;
    #endregion



    private float RunSpeed = 7.0f;
    private float WalkingSpeed = 3.0f;
    public float PlayerSpeed = 20.0f;
    private Rigidbody Rigidbody;
    private Animator Animator;
    public float Friction = 4.0f;
    public float MaxSpeed = 7.0f;
    // Start is called before the first frame update
    void Start() {
        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void ApplyAnimation() {
        Animator.SetBool("IsWalking", IsWalking);
        Animator.SetBool("IsRunning", IsRunning);
        Animator.SetInteger("Direction", MovementDirection);
        Animator.SetBool("IsFire", IsFire);
    }
    int GetMovementDirection(Vector3 velocity) {
        int movementDirection = 0;
        Vector3 dirVector = transform.forward;
        float angle = Vector3.SignedAngle(velocity, dirVector, new Vector3(0.0f, 1.0f, 0.0f));
        if (angle >= -22.5f) {
            movementDirection = Mathf.FloorToInt((angle + 22.5f) / 45.0f);
        }
        else {
            angle = -1 * angle;
            movementDirection = Mathf.FloorToInt((angle + 22.5f) / 45.0f);
            movementDirection = 8 - movementDirection;
        }
        Debug.Log(angle);
        Debug.Log(movementDirection);
        return movementDirection;
    }
    void FixedUpdate() {
        var horizontalInput = Input.GetAxis("Horizontal");
        var verticalInput = Input.GetAxis("Vertical");
        var horizontalForce = Rigidbody.velocity.x;
        var verticalForce = Rigidbody.velocity.z;
        IsRunning = Input.GetKey(KeyCode.LeftShift);
        MaxSpeed = IsRunning ? RunSpeed : WalkingSpeed;
        IsFire = (Input.GetButtonDown("Fire1"));

        var movementSpeedX = horizontalInput * PlayerSpeed;
        var movementSpeedZ = verticalInput * PlayerSpeed;

        if (horizontalInput == 0.0f) {
            if (horizontalForce != 0.0f) {
                //Debug.Log("Hor_0");
                movementSpeedX = (-1 * horizontalForce * 2) * Friction;
            }
        }
        if (verticalInput == 0.0f) {
            if (verticalForce != 0.0f) {
                //Debug.Log("Ver_0");
                movementSpeedZ = (-1 * verticalForce * 2) * Friction;
            }
        }
        if (Mathf.Abs(horizontalForce) > MaxSpeed) {
            movementSpeedX = 0.0f;
        }
        if (Mathf.Abs(verticalForce) > MaxSpeed) {
            movementSpeedZ = 0.0f;
        }
        if (horizontalInput == 0.0f && verticalInput == 0.0f) {
            IsWalking = false;
        }
        else {
            IsWalking = true;
        }
        MovementDirection = GetMovementDirection(Rigidbody.velocity);
        Vector3 force = new Vector3(movementSpeedX, 0.0f, movementSpeedZ);
        Rigidbody.AddForce(force);
        ApplyAnimation();
        //Debug.Log(horizontalInput);
        //Debug.Log(verticalInput);
        //Debug.Log(horizontalForce);
    }
}
