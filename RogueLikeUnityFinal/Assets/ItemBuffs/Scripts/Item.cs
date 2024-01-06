using System.Collections;
using StarterAssets;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

[System.Serializable]

public abstract class Item
{
    public abstract Sprite GetSprite();

    public abstract string GiveName();

    public virtual void OnHeal(ThirdPersonShooterController player, HealthBar healthBar, int count){

    }

    public virtual void OnPickupDamage(ThirdPersonShooterController player){

    }

    public virtual void OnJump(ThirdPersonShooterController player, int count){

    }

    public virtual void OnPickup(ThirdPersonController player){
        
    }

    public virtual void OnDamage(ThirdPersonShooterController player, HealthBar healthBar, int count){

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

    public override void OnHeal(ThirdPersonShooterController player, HealthBar healthBar, int count)
    {
        if (player.health > player.maxHealth)
        {
            player.health = player.maxHealth;
        }else{
            player.health += 3 + (2 * count);
            healthBar.UpdateHealthBar(player.health, player.maxHealth);
        }
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

    public override void OnPickupDamage(ThirdPersonShooterController player){
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

    public override void OnPickupDamage(ThirdPersonShooterController player){
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

    public override void OnPickup(ThirdPersonController player){
        player.MoveSpeed += 1;
        player.SprintSpeed += 1;
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

    public override void OnPickupDamage(ThirdPersonShooterController player) {
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

    public override void OnDamage(ThirdPersonShooterController player, HealthBar healthBar, int count){
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



