using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class HealthComponent : MonoBehaviour
{

    private const float minHealth = 0f;
    public float currentHealth;
    public float maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {   //make sure current health can't go below zero
        currentHealth = Mathf.Clamp(currentHealth - damage, minHealth, maxHealth);

        //if current health is zero, Die
        if (Mathf.Approximately(currentHealth, minHealth))
        {
            Destroy(gameObject);
        }
    }
}
