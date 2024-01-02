using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;
    public Items itemDrop;

    // Start is called before the first frame update
    void Start()
    {
        item = AssignItem(itemDrop);
    }

    private void OnTriggerEnter(Collider other){
        if(other.CompareTag("Player"))
        {
            ThirdPersonShooterController player = other.GetComponent<ThirdPersonShooterController>();
            AddItem(player);
            player.CallItemOnPickup(item);
            Destroy(this.gameObject);
        }
    }

    public void AddItem(ThirdPersonShooterController player){
        foreach(ItemList i in player.items){
            if (i.name == item.GiveName()){
                i.count += 1;
                return;
            }
        }
        player.items.Add(new ItemList(item, item.GiveName(), 1));
    }

    public Item AssignItem(Items itemToAssign){
        switch(itemToAssign){
            case Items.HealingItem:
                return new HealingItem();
            case Items.FireDamageItem:
                return new FireDamageItem();
            case Items.SpeedBoost:
                return new SpeedBoost();
            case Items.FireRateItem:
                return new FireRateItem();
<<<<<<< Updated upstream
            case Items.RelativeHealItem:
                return new RelativeHeal();
=======
>>>>>>> Stashed changes
            default:
                return new HealingItem();
        }
    }
}

public enum Items{
    SpeedBoost,
    HealingItem,
    FireDamageItem,
    FireRateItem,
    RelativeHealItem
}
