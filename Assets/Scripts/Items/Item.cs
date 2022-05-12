using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
   public string _itemName;
   public string _itemDescription;
   public int _itemCost;
   public bool _itemRotate;
   public float _amount;
   
   public enum _rarity { legendary, rare, uncommon, common };
   public _rarity _itemRarity;

   public Sprite _itemSprite;
   protected Inventroy _inventory;
   SpriteRenderer _itemRend;

   private void Awake()
   {
       _inventory = FindObjectOfType<Inventroy>();
       _itemRend = GetComponent<SpriteRenderer>();
       _itemRend.sprite = _itemSprite;
   }
   public virtual void Use()
   {
       Debug.Log("base use");
   }

   public virtual void Remove()
   {
       _inventory.RemoveItem(this);
   }
}

