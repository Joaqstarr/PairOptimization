using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utility
{
    public class RandomOrbitalSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject _objectToSpawn;
        [SerializeField]
        private float _radius;
        [SerializeField]
        private AnimationCurve _spawnRateOverTime;
        private float _timeElapsed = 0;
        private float _timer;
        [SerializeField]
        private bool _spawnOnStart = true;
        
        private void Start()
        {
            if (_spawnOnStart)
            {
                SpawnObject();
            }
        }
        
        private void Update()
        {
            _timeElapsed += Time.deltaTime;
            float spawnRate = _spawnRateOverTime.Evaluate(_timeElapsed);
            _timer += Time.deltaTime;

            if (_timer >= spawnRate)
            {
                SpawnObject();
                _timer = 0f;
            }
        }

        private void SpawnObject()
        {
            if (_objectToSpawn != null)
            {
                Vector3 randomCircle = Random.insideUnitSphere.normalized * _radius;
                Vector3 spawnPosition = transform.position + new Vector3(randomCircle.x, 0, randomCircle.y);
                Instantiate(_objectToSpawn, spawnPosition, Quaternion.identity);
            }
            
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}