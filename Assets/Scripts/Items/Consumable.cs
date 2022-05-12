using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : Item
{
    public int _uses;

    public override void Use()
    {
        base.Use();
        Debug.Log("Use Consumable");

        if (_uses > 0)
        {
            _uses--;
            FindObjectOfType<PlayerController>().Heal(_amount);
        }
        else 
        {
            Remove();
        }
    }

    public override void Remove()
    {
        base.Remove();
        Debug.Log("Remove Consumable");
    }
}
