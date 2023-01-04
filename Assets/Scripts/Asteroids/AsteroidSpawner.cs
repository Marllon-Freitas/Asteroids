using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    [SerializeField] private Asteroid asteroidPrefab;
    [SerializeField] private float spawnRate = 2.0f;
    [SerializeField] private float trajectoryVarient = 15.0f;
    [SerializeField] private float spawnDistance = 15.0f;
    [SerializeField] private int spawnAmount = 1;
    void Start()
    {
        InvokeRepeating(nameof(SpawnAsteroid), this.spawnRate, this.spawnRate);
    }

    private void SpawnAsteroid() {
        for (int i = 0; i < this.spawnAmount; i++)
        {
            Vector3 spawnDirection = Random.insideUnitCircle.normalized * this.spawnDistance;
            Vector3 spawnPosition = this.transform.position + spawnDirection;

            float variance = Random.Range(-this.trajectoryVarient, this.trajectoryVarient);
            Quaternion spawnRotation = Quaternion.AngleAxis(variance, Vector3.forward);

            Asteroid asteroid = Instantiate(this.asteroidPrefab, spawnPosition, spawnRotation);
            asteroid.asteroidSize = Random.Range(asteroid.minAsteroidSize, asteroid.maxAsteroidSize);
            asteroid.SetProjectory(spawnRotation * -spawnDirection);
        }
    }
}
