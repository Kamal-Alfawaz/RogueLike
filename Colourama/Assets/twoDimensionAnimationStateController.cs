using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class twoDimensionAnimationStateController : MonoBehaviour
{

    Animator animator;
    float velocityZ = 0.0f;
    float velocityX = 0.0f;
    public float acceleration = 2.0f;
    public float deceleration = 2.0f;
    public float maximumWalkVelocity = 0.5f;
    public float maximumRunVelocity = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool forwardPressed = Input.GetKey("w");
        bool leftPressed = Input.GetKey("a");
        bool rightPressed = Input.GetKey("d");
        bool runPressed = Input.GetKey("left shift");

        // set current maxVelocity
        float currentMaxVelocity = runPressed ? maximumRunVelocity : maximumWalkVelocity;

        // increase velocityZ
        if (forwardPressed && velocityZ < currentMaxVelocity){
            velocityZ += Time.deltaTime * acceleration;
        }

        // decrease velocityX
        if (leftPressed && velocityX > -currentMaxVelocity){
            velocityX -= Time.deltaTime * acceleration;
        }

        // increase velocityX
        if (rightPressed && velocityX < currentMaxVelocity){
            velocityX += Time.deltaTime * acceleration;
        }

        // decrease velocityZ
        if (!forwardPressed && velocityZ > 0.0f){
            velocityZ -= Time.deltaTime * deceleration;
        }

        // reset velocityZ
        if (!forwardPressed && velocityZ < 0.0f){
            velocityZ = 0.0f;
        }
        
        // increase velocityX if left is not pressed and velocityX < 0
        if (!leftPressed && velocityX < 0.0f){
            velocityX += Time.deltaTime * deceleration;
        }

        // decrease velocityX if right is not pressed and velocityX > 0
        if (!rightPressed && velocityX > 0.0f){
            velocityX -= Time.deltaTime * deceleration;
        }

        // reset velocityX
        if (!leftPressed && !rightPressed && velocityX != 0.0f && (velocityX > -0.05f && velocityX < 0.05f)){
            velocityX = 0.0f;
        }

        animator.SetFloat("Velocity Z", velocityZ);
        animator.SetFloat("Velocity X", velocityX);
    }
}
