using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class SpawnManager : MonoBehaviour
{
    ARRaycastManager _rayManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();
    Pose _rayPosition;
    bool _canSpawn;

    [Header("UI")]
    [SerializeField]
    GameObject placementMarker;

    [SerializeField]
    GameObject objectToBeSpawned;
    GameObject spawnedObject = null;

    [Header("Zombie")]

    [SerializeField]
    GameObject zombieSpawner;

    private GameObject spawnedZombieSpawner;
 
    private void Awake()
    {
        // finding raycast manager from the scene
        _rayManager = FindObjectOfType<ARRaycastManager>();
    }

    private void Start()
    {
        placementMarker.SetActive(false);
    }

    private void Update()
    {
        ARRaycast();
    }

    private void ARRaycast()
    {
        // guessing the middle point of the screen
        Vector2 screenPoint = new Vector2(Screen.width / 2, Screen.height / 2);

        // shooting the ray
        if (_rayManager.Raycast(screenPoint, hits, TrackableType.PlaneWithinPolygon))
        {
            // if hit count is more then zero
            if (hits.Count > 0)
            {
                // assigning the hit position into ray position
                _rayPosition = hits[0].pose;

                // place marker only when the any object has not spawned in the scene
                if (spawnedObject == null)
                {
                    PlaceMarker();
                }
            }
            else
            {
                _canSpawn = false;
            }
        }
    }

    private void PlaceMarker()
    {
        // if marker is not active int the scene then active it
        if (!placementMarker.activeInHierarchy)
        {
            placementMarker.SetActive(true);
        }
        else
        {
            // make can spawn true
            _canSpawn = true;

            // placing the marker
            placementMarker.transform.position = _rayPosition.position;
            placementMarker.transform.rotation = _rayPosition.rotation;

            // starting placing the object
            PlaceObject();
        }
    }

    private void PlaceObject()
    {
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            // if we can place object then place a object
            if (_canSpawn)
            {
                spawnedObject = Instantiate(
                    objectToBeSpawned,
                    _rayPosition.position,
                    _rayPosition.rotation
                );

                 spawnedZombieSpawner = Instantiate(
                    zombieSpawner,
                    _rayPosition.position,
                    _rayPosition.rotation
                );

                _canSpawn = false;
                placementMarker.SetActive(false);
            }
        }
    }
}
// class
