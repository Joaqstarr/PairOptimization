using System;
using UnityEngine;

namespace Utility
{
    public class Lifetime : MonoBehaviour
    {
        [SerializeField] private float _lifetime = 2;
        [SerializeField]private AnimationCurve _scaleOverLifetime = AnimationCurve.Linear(0,1,1,0);
        float _timeAlive = 0;
        private void Start()
        {
            Destroy(gameObject, _lifetime);
        }

        private void Update()
        {
            _timeAlive += Time.deltaTime;
            float lifePercent = _timeAlive / _lifetime;
            float scale = _scaleOverLifetime.Evaluate(lifePercent);
            transform.localScale = Vector3.one * scale;
        }
    }
}