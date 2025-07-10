using TopDown.Movement;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Collectables : MonoBehaviour
{
	private ICollectableBehavior _collectableBehavior;

	private void Awake()
	{
		_collectableBehavior = GetComponent<ICollectableBehavior>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		var player = collision.GetComponent<PlayerMovement>();

		if (player != null)
		{
			_collectableBehavior.OnCollected(player.gameObject);
			Destroy(gameObject);
		}
	}
}
