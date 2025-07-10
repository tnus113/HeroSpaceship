using TopDown.Health;
using UnityEngine;

public class HealthCollectableBehavior : MonoBehaviour, ICollectableBehavior
{
	[SerializeField]
	private float _healthAmount;

	public void OnCollected(GameObject player)
	{
		player.GetComponent<PlayerHealth>().Heal(_healthAmount);
	}
}
