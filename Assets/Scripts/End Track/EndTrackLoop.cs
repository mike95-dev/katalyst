using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrackLoop : MonoBehaviour
{
    public GameObject trackPrefab; // Get prefab for end track
    private GameObject trackRef;    // Get a reference to the spawned track
    private bool hasSpawned = false;    // Flag so only 1 section spawns at a time

    public void OnTriggerEnter(Collider other)
    {
        // If the object that triggered this is the player
        if (hasSpawned == false && other.CompareTag("Player"))
        {
            // Get a spawn location for the next section - offset to be in front of the next section
            // Since there is no great way to move the origin of a prefab, use about 800 units
            Vector3 spawn = new Vector3(transform.position.x, 0, transform.position.z + 800);

            // Get a reference to the parent objects transform
            // The parent will be the moving track
            // ** This can be removed once the track stays in place **
            GameObject parentObj = GameObject.FindWithTag("LevelTrack");
            Transform parent = parentObj.transform;

            // Spawn a new tunnel and keep a reference of this section
            trackRef = Instantiate(trackPrefab, spawn, Quaternion.identity, parent);

            // Update the flag
            hasSpawned = true;

            // Set to destroy track and trigger after some time
            DestroyTrack();
        }
    }

    public void DestroyTrack()
    {
        // Destroy this section of track after some time
        Destroy(trackRef, 45);
    }
}
