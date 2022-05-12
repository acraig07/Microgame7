using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeeleCollider : MonoBehaviour
{
    public float _attack;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().Damaage(_attack);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().Damaage(_attack);
        }
    }
    
}
