using UnityEngine;
using System.Collections;
using DG.Tweening.Core.Easing;

public class ShieldManager : MonoBehaviour
{
	[SerializeField] private GameObject shield;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("EnemyBullet"))
		{
			shield.SetActive(false);
			StartCoroutine(RespawnShield());
		}
	}

	private IEnumerator RespawnShield()
	{
		yield return new WaitForSeconds(10f);
		shield.SetActive(true);
	}
}
