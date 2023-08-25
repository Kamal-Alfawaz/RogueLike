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

    // performance increase
    int VelocityZHash;
    int VelocityXHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        // increase performance
        VelocityZHash = Animator.StringToHash("Velocity Z");
        VelocityXHash = Animator.StringToHash("Velocity X");
    }

    void changeVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, bool runPressed, float currentMaxVelocity){
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

        // increase velocityX if left is not pressed and velocityX < 0
        if (!leftPressed && velocityX < 0.0f){
            velocityX += Time.deltaTime * deceleration;
        }

        // decrease velocityX if right is not pressed and velocityX > 0
        if (!rightPressed && velocityX > 0.0f){
            velocityX -= Time.deltaTime * deceleration;
        }
    }

    void lockOrResetVelocity(bool forwardPressed, bool leftPressed, bool rightPressed, bool runPressed, float currentMaxVelocity){
        // reset velocityZ
        if (!forwardPressed && velocityZ < 0.0f){
            velocityZ = 0.0f;
        }

        // if (!leftPressed && !rightPressed && velocityX != 0.0f && (velocityX > -0.05f && velocityX < 0.05f)){
        //     velocityX = 0.0f;
        // }

        // lock forward
        if (forwardPressed && runPressed && velocityZ > currentMaxVelocity)
        {
            velocityZ = currentMaxVelocity;
        }

        // decelerate to the maximum walk velocity
        else if (forwardPressed && velocityZ > currentMaxVelocity){
            velocityZ -= Time.deltaTime * deceleration;
            // round to the currentMaxVelocity if within offset
            if (velocityZ > currentMaxVelocity && velocityZ < (currentMaxVelocity + 0.05f))
            {
                velocityZ = currentMaxVelocity;
            }
        }

        else if (forwardPressed && velocityZ < currentMaxVelocity && velocityZ > (currentMaxVelocity - 0.05f))
        {
            velocityZ = currentMaxVelocity;
        }

        // Locking left
        if (leftPressed && runPressed && velocityX < -currentMaxVelocity)
        {
            velocityX = -currentMaxVelocity;
        }

        // decelerate to the maximum walk velocity
        else if (leftPressed && velocityX < -currentMaxVelocity){
            velocityX += Time.deltaTime * deceleration;
            // round to the currentMaxVelocity if within offset
            if (velocityX < -currentMaxVelocity && velocityX > (-currentMaxVelocity - 0.05f))
            {
                velocityX = -currentMaxVelocity;
            }
        }

        else if (leftPressed && velocityX > -currentMaxVelocity && velocityX < (-currentMaxVelocity + 0.05f))
        {
            velocityX = -currentMaxVelocity;
        }

        // lock right
        if (rightPressed && runPressed && velocityX > currentMaxVelocity)
        {
            velocityX = currentMaxVelocity;
        }

        // decelerate to the maximum walk velocity
        else if (rightPressed && velocityX > currentMaxVelocity){
            velocityX -= Time.deltaTime * deceleration;
            // round to the currentMaxVelocity if within offset
            if (velocityX > currentMaxVelocity && velocityX < (currentMaxVelocity + 0.05f))
            {
                velocityX = currentMaxVelocity;
            }
        }

        else if (forwardPressed && velocityZ < currentMaxVelocity && velocityZ > (currentMaxVelocity - 0.05f))
        {
            velocityX = currentMaxVelocity;
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.D);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);

        // set current maxVelocity
        float currentMaxVelocity = runPressed ? maximumRunVelocity : maximumWalkVelocity;

        // handles changes in velocity
        changeVelocity(forwardPressed, leftPressed, rightPressed, runPressed, currentMaxVelocity);
        lockOrResetVelocity(forwardPressed, leftPressed, rightPressed, runPressed, currentMaxVelocity);

        animator.SetFloat(VelocityZHash, velocityZ);
        animator.SetFloat(VelocityXHash, velocityX);
    }
}
