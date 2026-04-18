using Player;
using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies
{
    public class HealthChangeOnCollide : MonoBehaviour
    {
 
		[FormerlySerializedAs("_damageAmount")] [SerializeField] private int _changeAmount  = -1;
		[SerializeField] private bool _shouldDestroy = true;
		private void OnCollisionEnter(Collision collision)
		{
			var damageable = collision.gameObject.GetComponent<Health>();
			if (damageable != null)
			{
				damageable.TakeDamage(-_changeAmount);
				if (_shouldDestroy)
				{
					Destroy(gameObject);
				}
			}
		}
    }
}