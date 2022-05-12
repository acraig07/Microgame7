using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float _speed;
    Rigidbody2D _bulletRigidBody;
    RangedWeapon _colt;

    private void Awake()
    {
        _colt = FindObjectOfType<RangedWeapon>();
        _bulletRigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _bulletRigidBody.AddForce(transform.up * _speed);
        Invoke("Disable", 4f);
    }

    void Disable()
    {
        Destroy(gameObject);
        //gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collison)
    {
        if(collison.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Projectile OnTriggerEnter2D");
            collison.GetComponent<EnemyController>().Damaage(_colt._amount);
            Invoke("Disable", 0.01f);
        }
    }
    /*private void OnDisable()
    {
        CancelInvoke();
    }*/
}
