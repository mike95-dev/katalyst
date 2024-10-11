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

    private void OnCollisionEnter(Collision other)
    {
        //get health of other gameobject
        HealthComponent otherHealth = other.gameObject.GetComponent<HealthComponent>();
        //only do damage if damage can be done
        if (otherHealth != null)
        {
            otherHealth.TakeDamage(damage);
        }

        //destroy obstacle
        Destroy(gameObject); // feel free to take this out if we want the obstacles to persist after dealing damage,
                             // but we'd need to give the player invincibility frames
    }
}
