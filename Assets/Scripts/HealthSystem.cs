using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int startHealth = 1;
    [SerializeField] private float damageCooldown = 1;
    [SerializeField] private float deathEffectDelay = 0.1f;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject deathScreen;

    private int healthVar;
    private float damageTimer;

    public delegate void HealthUpdate(int health);
    public event HealthUpdate onHealthUpdate = delegate { };

    public static bool death = false;

    public int health
    {
        get => healthVar;
        set
        {
            if(value < healthVar) animator.SetTrigger("Damage");
            healthVar = value;
            onHealthUpdate(healthVar);
            damageTimer = Time.time + damageCooldown;
            if (healthVar <= 0) Died();
        }
    }

    public void Died()
    {
        death = true;
        StartCoroutine(DiedEffect());
    }

    private IEnumerator DiedEffect()
    {
        yield return new WaitForSeconds(deathEffectDelay);
        deathScreen.SetActive(true);
        animator.enabled = false;
    }

    private void Start()
    {
        damageTimer = Time.time;
        health = startHealth;
        death = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (HealthSystem.death) return;
        if (collision.gameObject.CompareTag("Death"))
            if (Time.time > damageTimer) health--;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (HealthSystem.death) return;
        if (collision.gameObject.CompareTag("Death"))
            if (Time.time > damageTimer) health--;
    }
}
