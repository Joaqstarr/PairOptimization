using System;
using UnityEngine;
using Random = System.Random;

namespace Enemies
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject _prefabToSpawn;
        [SerializeField]
        private float _spawnInterval = 2f;
        
        [SerializeField]
        private float _randomVariation = 2f;
        private float _spawnTimer;

        private void Start()
        {
            _spawnTimer = UnityEngine.Random.Range(-_randomVariation, 0); // Start with a negative timer to allow for immediate spawning
        }

        private void Update()
        {
            _spawnTimer += Time.deltaTime;
            if (_spawnTimer >= _spawnInterval)
            {
                SpawnEnemy();
                _spawnTimer = 0f;
            }
        }

        private void SpawnEnemy()
        {
            if (_prefabToSpawn != null)
            {
                Instantiate(_prefabToSpawn, transform.position, transform.rotation);
            }
        }
    }
}