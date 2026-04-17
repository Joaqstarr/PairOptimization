using UnityEngine;

namespace GravityZones
{
    public class SphericalGravityZone : BaseGravityZone
    {
        public override Vector3 GetGravity(Vector3 position)
        {
            Vector3 direction = transform.position - position;
            Vector3 dirNormalized = direction.normalized;
            
            return dirNormalized * _strength;
        }
    }
}