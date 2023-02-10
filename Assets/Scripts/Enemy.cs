using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 50f;

    public void TakeDamage (float damage)
    {
        health -= damage;

        //�����GHP��0�ɂȂ�����
        if (health <= 0f)
        {
            //�G�̎��S
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}