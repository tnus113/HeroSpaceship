using UnityEngine;
using UnityEngine.UI;

public class CoreHealth : MonoBehaviour
{
	[SerializeField] private float maxHp = 10f;
	private float hp;
	public Image hpBar;

	public GameManager gameManager;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		hp = maxHp;
	}

	public void takeDamage(float damageAmount)
	{
		hp -= damageAmount;
		if (hp < 0) hp = 0;
		hpBar.fillAmount = hp / 10f;

		if (hp <= 0)
		{
			gameObject.SetActive(false);
			gameManager.gameOver();
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("EnemyBullet"))
		{
			takeDamage(1);
			Destroy(collision.gameObject);
		}
	}
}
