using Unity.VisualScripting;
using UnityEngine;

namespace TopDown.Movement
{
	[RequireComponent (typeof (Rigidbody2D))]
	public class Mover : MonoBehaviour
	{
		[SerializeField] private float movementSpeed;
		private Rigidbody2D body;
		protected Vector3 currentInput;
		protected Vector3 smoothInput;
		protected Vector3 smoothInputVelocity;

		private void Awake()
		{
			body = GetComponent<Rigidbody2D>();
		}

		private void FixedUpdate()
		{
			smoothInput = Vector3.SmoothDamp(
				smoothInput,
				currentInput,
				ref smoothInputVelocity,
				0.1f);

			body.linearVelocity = movementSpeed * smoothInput * Time.deltaTime;
		}
	}
}

