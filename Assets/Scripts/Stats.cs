using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public int health, startHealth, maxHealth, minHealth;
    internal bool isDead;

    public void HealHealth(int amount_)
    {
        health += amount_;
        VerifyHealth();
    }

    public void HurtHealth(int amount_)
    {
        health -= amount_;
        VerifyHealth();
    }

    public void VerifyHealth()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        else if (health < minHealth)
        {            
            Die();
            return;
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }


}
