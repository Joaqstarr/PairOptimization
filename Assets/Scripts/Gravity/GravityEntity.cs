using System.Collections.Generic;
using UnityEngine;

namespace GravityZones
{
    public class GravityEntity : MonoBehaviour
    {
        private readonly HashSet<BaseGravityZone> _zones = new HashSet<BaseGravityZone>();
        public IReadOnlyCollection<BaseGravityZone> zones => _zones;

        private void OnTriggerEnter(Collider other)
        {
            TryAddZone(other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            TryRemoveZone(other.gameObject);
        }
        /*
        // Collision-based detection as a fallback if zones use non-trigger colliders.
        private void OnCollisionEnter(Collision collision)
        {
            TryAddZone(collision.gameObject);
        }

        private void OnCollisionExit(Collision collision)
        {
            TryRemoveZone(collision.gameObject);
        }*/

        private void TryAddZone(GameObject obj)
        {
            var zone = obj.GetComponentInParent<BaseGravityZone>();
            if (zone == null) return;
            _zones.Add(zone);
        }

        private void TryRemoveZone(GameObject obj)
        {
            var zone = obj.GetComponentInParent<BaseGravityZone>();
            if (zone == null) return;
            _zones.Remove(zone);
        }

        // Returns the sum of gravity vectors provided by all active zones at this entity's position.
        public Vector3 GetCombinedGravity()
        {
            CleanNulls();
            Vector3 result = Vector3.zero;
            foreach (var z in _zones)
            {
                if (z == null) continue;
                result += z.GetGravity(transform.position);
            }
            return result;
        }

        // Remove any destroyed/collected zones from the set.
        private void CleanNulls()
        {
            _zones.RemoveWhere(z => z == null);
        }

        // Clear tracked zones when the entity is disabled to avoid retaining stale references
        private void OnDisable()
        {
            _zones.Clear();
        }
    }
}