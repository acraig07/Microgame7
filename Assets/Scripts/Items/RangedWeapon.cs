using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Item
{
    public GameObject _projectile;

    public override void Use()
    {
        base.Use();
        //Debug.Log("Use RangedWeapon");
        if(_inventory.transform.localScale.x == 1)
            Instantiate(_projectile, transform.position, transform.rotation * Quaternion.Euler(0,0,-90));
        if(_inventory.transform.localScale.x == -1)
            Instantiate(_projectile, transform.position, transform.rotation * Quaternion.Euler(0,0,90));
    }

    public override void Remove()
    {
        base.Remove();
        //Debug.Log("Remove RangedWeapon");
    }
}
