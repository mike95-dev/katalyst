using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public float force;
    // Start is called before the first frame update
    void Start()
    {
        GameObject newAsteroid = Instantiate(asteroidPrefab, transform.position, transform.rotation) as GameObject;
        Rigidbody rb = newAsteroid.GetComponent<Rigidbody>();
        if (rb)
        {
            rb.AddForce(this.transform.forward * force); //aim the asteroid by rotating the game object this script is attached to
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
