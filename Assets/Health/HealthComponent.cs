using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class HealthComponent : MonoBehaviour
{

    private const float minHealth = 0f;
    public float maxHealth;
    public float currentHealth;

    // the amount of time that should be set for invincibility
    public float invincibilityTime = 1f;
    public bool isInvincible = false;

    // Get Tutorial Object
    GameObject tutorialObject;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        // Find the "Tutorial" GameObject in the scene
        tutorialObject = GameObject.Find("Tutorial");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetCurrentHealth(float amount)
    {
        currentHealth = Mathf.Clamp(amount, minHealth, maxHealth);

        Debug.Log("New Health Value: " + currentHealth);
    }


    public void TakeDamage(float damage)
    {   
        //make sure current health can't go below zero
        currentHealth = Mathf.Clamp(currentHealth - damage, minHealth, maxHealth);

        Debug.Log("New Health Value: " + currentHealth);

        //if current health is zero, Die
        if (Mathf.Approximately(currentHealth, minHealth))
        {
            Die();
        }
    }

    public void Die()
    {
        // Find the "Tutorial" GameObject in the scene
        GameObject tutorialObject = GameObject.Find("Tutorial");

        if (tutorialObject != null)
        {
            // Get the TutorialTrackMovement component from the "Tutorial" GameObject
            TutorialTrackMovement trackMovement = tutorialObject.GetComponent<TutorialTrackMovement>();

            if (trackMovement != null)
            {
                // Call the Collision method in TutorialTrackMovement
                trackMovement.Collision();
            }
            else
            {
                Debug.LogError("No TutorialTrackMovement component found on the 'Tutorial' object!");
            }
        }
        else
        {
            Debug.LogError("'Tutorial' GameObject not found in the scene!");
        }
    }


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
