using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;
using static UnityEngine.UI.GridLayoutGroup;

public class DamageOnHit : MonoBehaviour
{
    //slap this script on things that do damage

    public float damage;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // check if it's player
        if (other.CompareTag("Player"))
        {
            //get health of other gameobject
            HealthComponent otherHealth = other.gameObject.GetComponent<HealthComponent>();
            //only do damage if damage can be done
            if (otherHealth != null)
            {
                // Make sure they're invincible
                if (!otherHealth.isInvincible)
                {
                    otherHealth.TurnInvincible();
                    otherHealth.TakeDamage(damage);
                }
                else
                {
                    Debug.Log("Player is invincible");
                }

            }

            //destroy obstacle
            Destroy(gameObject); // feel free to take this out if we want the obstacles to persist after dealing damage,
                                 // but we'd need to give the player invincibility frames
        }
    }
}
