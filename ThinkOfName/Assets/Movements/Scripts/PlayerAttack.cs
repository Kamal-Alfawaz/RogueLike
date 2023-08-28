using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    AnimatorManager animatorManager;

    private void Awake(){
        animatorManager = GetComponent<AnimatorManager>();
    }

    public void HandleMainAttack(){
        animatorManager.PlayTargetAnimation("FirstSlash", true);
    }

}
