using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 50f;

    public void TakeDamage (float damage)
    {
        health -= damage;

        //‚à‚µ“GHP‚ª0‚É‚È‚Á‚½‚ç
        if (health <= 0f)
        {
            //“G‚ÌŽ€–S
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
