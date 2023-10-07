using StarterAssets;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

[System.Serializable]

public abstract class Item
{
    public abstract string GiveName();

    public virtual void OnHeal(ThirdPersonShooterController player, int count){

    }

    public virtual void OnHit(ThirdPersonShooterController player, Enemy enemy, int count){

    }

    public virtual void OnJump(ThirdPersonShooterController player, int count){

    }

    public virtual void JumpAndSpeed(ThirdPersonController player){
        
    }
}

public class HealingItem : Item
{
    public override string GiveName()
    {
        return "Healing Item";
    }

    public override void OnHeal(ThirdPersonShooterController player, int count)
    {
        player.health += 3 + (2 * count);
    }
}

public class FireDamageItem : Item
{
    public override string GiveName(){
        return "Fire Damage Item";
    }

    public override void OnHit(ThirdPersonShooterController player, Enemy enemy, int count){
        enemy.health -= 10 * count;
    }
}

public class HealingArea : Item{

    GameObject effect;

    public override string GiveName(){
        return "Healing Area";
    }

    public override void OnJump(ThirdPersonShooterController player, int count){
        if(effect == null) effect = (GameObject)Resources.Load("Item Effects/HealingArea", typeof(GameObject));
        GameObject healingArea = GameObject.Instantiate(effect, player.transform.position, Quaternion.Euler(Vector3.zero));
    }
}

public class SpeedBoost : Item{
    
    public override string GiveName(){
        return "Speed Boost";
    }

    public override void JumpAndSpeed(ThirdPersonController player){
        player.MoveSpeed += 1;
        player.SprintSpeed += 1;
    }
} 

