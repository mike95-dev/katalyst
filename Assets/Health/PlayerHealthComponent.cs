using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerHealthComponent : HealthComponent
{

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

    }

    override public void SetCurrentHealth(float amount)
    {
        currentHealth = Mathf.Clamp(amount, minHealth, maxHealth);

        Debug.Log("New Health Value: " + currentHealth);
    }


    override public void TakeDamage(float damage)
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

    override public void Die()
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

}
