using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public float force;
    public Boolean isRepeated;
    public float spawnTime;
    public float timeToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        GameObject newAsteroid = Instantiate(asteroidPrefab, transform.position, transform.rotation) as GameObject;
        Rigidbody rb = newAsteroid.GetComponent<Rigidbody>();
        
        if (rb)
        {
            rb.AddForce(this.transform.forward * force); //aim the asteroid by rotating the game object this script is attached to
        }
        timeToSpawn = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRepeated) //if the asteroid respawns
        {
            timeToSpawn += Time.deltaTime; // add time to spawn timer
            if (timeToSpawn > spawnTime) { //when the timer reaches the spawn time
                GameObject newAsteroid = Instantiate(asteroidPrefab, transform.position, transform.rotation) as GameObject;
                Rigidbody rb = newAsteroid.GetComponent<Rigidbody>();

                if (rb)
                {
                    rb.AddForce(this.transform.forward * force); //aim the asteroid by rotating the game object this script is attached to
                }
                timeToSpawn = 0; //begin the cycle anew
            }
        }
    }
}
