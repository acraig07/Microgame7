using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Item
{
    Animator _anim;
    public LayerMask _enemyLayer;
    public float _rayLength;
    

    public override void Use()
    {
        base.Use();
        Debug.Log("Use MeleeWeapon");
        FindObjectOfType<Animator>().SetTrigger("strike");

        /*RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, _rayLength, _enemyLayer);
        if(hit.collider != null && hit.collider.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("hit " + hit.collider.name);
        }
        Debug.DrawRay(transform.position, Vector2.right * _rayLength);*/
    }

    public override void Remove()
    {
        base.Remove();
        Debug.Log("Remove MeleeWeapon");
    }

    private void OnTriggerEnter2D(Collider2D collison)
    {
        if(collison.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Sword Hit Enemy");
            collison.GetComponent<EnemyController>().Damaage(_amount);
        }
    }
}
