using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 50f;

    public void TakeDamage (float damage)
    {
        health -= damage;

        //もし敵HPが0になったら
        if (health <= 0f)
        {
            //敵の死亡
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
