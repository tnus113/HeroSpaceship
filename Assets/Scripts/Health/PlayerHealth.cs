using UnityEngine;
using UnityEngine.UI;

namespace TopDown.Health
{
	public class PlayerHealth : MonoBehaviour
	{
		[SerializeField] private float maxHp = 10f;
		private float hp;
		public Image hpBar;

		private bool isDead;
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

			if (hp <= 0 && !isDead)
			{
				isDead = true;
				gameObject.SetActive(false);
				gameManager.gameOver();
			}
		}

		public void Heal(float healAmount)
		{
			hp += healAmount;
            if (hp > maxHp)
            {
				hp = maxHp;
            }
			hpBar.fillAmount = hp / 10f;
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
}