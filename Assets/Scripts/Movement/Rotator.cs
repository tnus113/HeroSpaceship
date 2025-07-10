using UnityEngine;

namespace TopDown.Movement
{
	public class Rotator : MonoBehaviour
	{
		protected void LookAt(Transform rotatedTransform, Vector3 target)
		{
			float lookAngle = AngleBetweenTwoPoints(transform.position, target) + 90;

			rotatedTransform.eulerAngles = new Vector3(0, 0, lookAngle);
		}
		private float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
		{
			return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
		}
	}
}
