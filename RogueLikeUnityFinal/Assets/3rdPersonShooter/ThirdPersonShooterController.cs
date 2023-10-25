using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.Collections;
using UnityEditor.PackageManager;

public class ThirdPersonShooterController : MonoBehaviour
{
    public static ThirdPersonShooterController Instance;

    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private LayerMask aimColliderLayerMask;
    
    // related to the InputSystem, i.e anything input related you refer to these variables
    private StarterAssetsInputs starterAssetsInputs;
    private ThirdPersonController thirdPersonController;

    // related to the character's gun's fire-rate and damage.
    public float impactForce = 30f;
    public float damage = 1f;
    public float range = 100f;
    public float fireRate = 15f;
    private float nextTimeToFire = 0f;

    //related to character's Health
    public float health = 100f;

    public List<ItemList> items = new List<ItemList>();

    private void Start(){
        StartCoroutine(CallItemUpdate());
        // if(Instance != null){
        //     Destroy(this.gameObject);
        //     return;
        // }
        // Instance = this;
        // GameObject.DontDestroyOnLoad(this.gameObject);
    }

    // Awake Method only gets called as soon as the scene starts, i.e when the game/level starts
    private void Awake(){
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();
    }

    // update method gets called on every frame of the game, i.e use this method when u want to make stuff happen during the game
    private void Update(){
        Vector3 mouseWorldPosition = Vector3.zero;

        //RayCast to handle the shooting
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        Transform hitTransform = null;
        if(Physics.Raycast(ray, out RaycastHit raycastHit, range, aimColliderLayerMask)){
            mouseWorldPosition = raycastHit.point;
            hitTransform = raycastHit.transform;
        }

        // Boolean Check for when the person is clicking shoot button
        if (starterAssetsInputs.shoot){
            // this part handles the character's rotation towards where the player is aiming 
            thirdPersonController.SetRotationOnMove(false);

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget- transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
            // this part handles the shooting mechanics, i.e dealing damage to enemies, generating the hit effect, handling firerate
            if(Time.time >= nextTimeToFire){
                nextTimeToFire = Time.time + 1f / fireRate;

                Enemy target = hitTransform.GetComponent<Enemy>();
                if(target != null){
                    CallItemOnHit(target);
                    target.takeDamage(damage);
                }
            }
        }else{
            thirdPersonController.SetRotationOnMove(true);
        }

        // Boolean check for when the person is clicking sprint button
        if (starterAssetsInputs.sprint){
            aimVirtualCamera.gameObject.SetActive(true);
        }else{
            aimVirtualCamera.gameObject.SetActive(false);
        }

        if(starterAssetsInputs.jump){
            CallItemOnJump();
        }

        if(starterAssetsInputs.grenade){
            Debug.Log("throwing grenade");
            starterAssetsInputs.grenade = false;
        }

        if(starterAssetsInputs.slide){
            Debug.Log("sliding");
            starterAssetsInputs.slide = false;
        }

    }
    
    //Related to the player's items
    IEnumerator CallItemUpdate(){
        foreach(ItemList i in items){
            i.item.OnHeal(this, i.count);
        }
        yield return new WaitForSeconds(1);
        StartCoroutine(CallItemUpdate());
    }

    public void CallItemOnHit(Enemy enemy){
        foreach(ItemList i in items){
            i.item.OnHit(this, enemy, i.count);
        }
    }

    public void CallItemOnJump(){
        foreach(ItemList i in items){
            i.item.OnJump(this, i.count);
        }   
    }

    public void CallItemOnPickup(){
        foreach(ItemList i in items){
            i.item.JumpAndSpeed(thirdPersonController);
        }
    }
}
