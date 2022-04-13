using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int startHealth = 1;
    [SerializeField] private float damageCooldown = 1;

    private int healthVar;
    private float damageTimer;

    public delegate void HealthUpdate(int health);
    public event HealthUpdate onHealthUpdate = delegate { };

    public int health
    {
        get => healthVar;
        set
        {
            healthVar = value;
            onHealthUpdate(healthVar);
            damageTimer = Time.time + damageCooldown;
            if (healthVar <= 0) Died();
        }
    }

    public void Died()
    {

    }

    private void Start()
    {
        damageTimer = Time.time;
        health = startHealth;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Death"))
        if (Time.time > damageTimer) health--;
    }
}
