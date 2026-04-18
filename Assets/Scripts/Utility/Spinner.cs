using UnityEngine;

namespace Utility
{
    public class Spinner : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _spinSpeed = Vector3.up;
        
        private void Update()
        {
            transform.Rotate(_spinSpeed * Time.deltaTime);
        }
    }
}