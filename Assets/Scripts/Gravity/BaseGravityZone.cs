using UnityEngine;

namespace GravityZones
{
    [DisallowMultipleComponent]
    public class BaseGravityZone : MonoBehaviour
    {
        //strength of gravity
        [SerializeField]
        protected float _strength;
        public virtual Vector3 GetGravity(Vector3 position)
        {
            return Vector3.zero;
        }
        
    }
}