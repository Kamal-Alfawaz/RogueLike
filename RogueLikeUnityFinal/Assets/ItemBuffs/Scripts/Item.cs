using System.Collections;
using StarterAssets;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]

public abstract class Item
{
    public abstract Sprite GetSprite();

    public abstract string GiveName();

    public virtual void OnHeal(ThirdPersonShooterController player, HealthBar healthBar, int count){

    }

    public virtual void OnPickup(ThirdPersonShooterController player){

    }

    public virtual void OnJump(ThirdPersonShooterController player, int count){

    }

    public virtual void OnDamage(ThirdPersonShooterController player, HealthBar healthBar, int count, RaycastHit hitInfo){

    }

}

public class HealingItem : Item
{
    public override Sprite GetSprite()
    {
        return (Sprite)Resources.Load("ability images/health", typeof(Sprite));
    }

    public override string GiveName()
    {
        return "Healing Item";
    }

    public override void OnPickup(ThirdPersonShooterController player)
    {
        player.maxHealth += player.maxHealth * 0.1f;
    }
}

public class FireDamageItem : Item
{

    public override Sprite GetSprite()
    {
        return (Sprite)Resources.Load("ability images/damage", typeof(Sprite));
    }

    public override string GiveName(){
        return "Fire Damage Item";
    }

    public override void OnPickup(ThirdPersonShooterController player){
        player.damage += 10;
    }
}

public class FireRateItem : Item
{

    public override Sprite GetSprite()
    {
        return (Sprite)Resources.Load("ability images/fireRate", typeof(Sprite));
    }

    public override string GiveName(){
        return "Fire Rate Item";
    }

    public override void OnPickup(ThirdPersonShooterController player){
        player.fireRate += 1f;
    }
}

public class HealingArea : Item{

    GameObject effect;

    public override Sprite GetSprite()
    {
        return (Sprite)Resources.Load("ability images/healingArea", typeof(Sprite));
    }

    public override string GiveName(){
        return "Healing Area";
    }

    public override void OnJump(ThirdPersonShooterController player, int count){
        if(effect == null) effect = (GameObject)Resources.Load("Item Effects/HealingArea", typeof(GameObject));
        GameObject healingArea = GameObject.Instantiate(effect, player.transform.position, Quaternion.Euler(Vector3.zero));
    }
}

public class SpeedBoost : Item{

    public override Sprite GetSprite()
    {
        return (Sprite)Resources.Load("ability images/speed", typeof(Sprite));
    }
    
    public override string GiveName(){
        return "Speed Boost";
    }

    public override void OnPickup(ThirdPersonShooterController player){
        player.thirdPersonController.MoveSpeed += 1;
        player.thirdPersonController.SprintSpeed += 1;
    }
}

public class DoubleDamageItem: Item{
    public override Sprite GetSprite()
    {
        return (Sprite)Resources.Load("ability images/doubleDamage", typeof(Sprite));
    }

    public override string GiveName(){
        return "Double Damage Item";
    }

    public override void OnPickup(ThirdPersonShooterController player) {
        player.StartDoubleDamageCoroutine();
    }

}

public class LifeStealItem : Item
{
    public override Sprite GetSprite()
    {
        return (Sprite)Resources.Load("ability images/lifeSteal", typeof(Sprite));
    }

    public override string GiveName()
    {
        return "Life Steal Item";
    }

    public override void OnDamage(ThirdPersonShooterController player, HealthBar healthBar, int count, RaycastHit hitInfo){
        float healAmount = player.damage * (count * 0.02f);

        if (player.health == player.maxHealth)
        {
            return;
        }

        else if (player.health + healAmount > player.maxHealth)
        {
            healAmount = player.maxHealth - player.health;
        }
        player.health += healAmount;
        healthBar.UpdateHealthBar(player.health, player.maxHealth);
    }
}

public class ExplosiveItem : Item
{ 
    GameObject effect; 

    public override Sprite GetSprite()
    {
        return (Sprite)Resources.Load("ability images/explosive", typeof(Sprite));
    }

    public override string GiveName()
    {
        return "Explosive Item";
    }

    public override void OnDamage(ThirdPersonShooterController player, HealthBar healthBar, int count, RaycastHit hitInfo)
    {
        float explosionRadius = count * 0.4f;

        if(effect == null) effect = (GameObject)Resources.Load("Item Effects/Explosion", typeof(GameObject));
        Vector3 explosionPosition = hitInfo.point + hitInfo.normal * 0.1f;
        GameObject Explosion = GameObject.Instantiate(effect, explosionPosition, Quaternion.Euler(Vector3.zero));
        Explosion.transform.localScale *= explosionRadius;
        GameObject.Destroy(Explosion, 0.4f);

        // Get all colliders within the explosion radius
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);

        foreach (Collider hit in colliders)
        {
            if (hit.CompareTag("Enemy"))
            {
                // Apply the explosion damage to the enemy
                hit.GetComponent<Enemy>().takeDamage(player.damage);
            }
        }
    }
}



