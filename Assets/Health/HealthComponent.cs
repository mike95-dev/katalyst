using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public abstract class HealthComponent : MonoBehaviour
{

    protected const float minHealth = 0f;
    public float maxHealth;
    public float currentHealth;

    // the amount of time that should be set for invincibility
    public float invincibilityTime = 1f;
    public bool isInvincible = false;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Deal with Health & Death
    public abstract void SetCurrentHealth(float amount);
    public abstract void TakeDamage(float damage);
    public abstract void Die();


    // Make component invincible for a set amount of time
    public void TurnInvincible()
    {
        // Create coroutine to countdown
        StartCoroutine(InvincincibleEnum(invincibilityTime));
    }

    // for a limited time, make player invincible.
    IEnumerator InvincincibleEnum(float time)
    {
        isInvincible = true;
        yield return new WaitForSeconds(time);
        isInvincible = false;
    }
}
