using JetBrains.Annotations;
using System.Collections.Generic;
using TopDown.Shooting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class EnemyController : MonoBehaviour
{
	public float speed;
	Rigidbody2D body;
	Transform player;
	Transform playerBase;
	Vector2 moveDirection;
	float distanceToPlayer;
	float distanceToBase;
	float speedStatus;
	[SerializeField] float health, maxHealth = 3f;

	public float cooldown = 0.25f;
	private float cooldownTimer;
	public GameObject bulletPrefab;
	public List<Transform> firepoints;
	public List<Animator> muzzleFlashAnimators;

	public UnityEvent OnDied;

	private void Awake()
	{
		body = GetComponent<Rigidbody2D>();
	}

	void Start()
	{
		player = GameObject.Find("Player").transform;
		playerBase = GameObject.Find("Base").transform;
		health = maxHealth;
	}

	void Update()
	{
		if (player)
		{
			distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
			distanceToBase = Vector2.Distance(transform.position, playerBase.transform.position);

			if (distanceToPlayer < 2 || distanceToBase < 2)
			{
				speedStatus = 0;
				Shoot();
			}
			else
			{
				speedStatus = speed;
			}

			if (distanceToPlayer <= distanceToBase)
			{
				Vector3 direction = (player.position - transform.position).normalized;
				moveDirection = direction;
				float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
				body.rotation = angle - 90;
			}
			else
			{
				Vector3 direction = (playerBase.position - transform.position).normalized;
				moveDirection = direction;
				float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
				body.rotation = angle - 90;
			}
			cooldownTimer += Time.deltaTime;
		}
	}

	private void FixedUpdate()
	{
		if (player)
		{
			body.linearVelocity = new Vector2(moveDirection.x, moveDirection.y) * speedStatus * Time.deltaTime;
		}
	}

	private void Shoot()
	{
		if (cooldownTimer < cooldown) return;

		for (int i = 0; i < firepoints.Count; i++)
		{
			GameObject bullet = Instantiate(bulletPrefab, firepoints[i].position, firepoints[i].rotation, null);
			bullet.GetComponent<Projectile>().ShootBullet(firepoints[i]);

			if (i < muzzleFlashAnimators.Count)
			{
				muzzleFlashAnimators[i].SetTrigger("shoot");
			}
		}

		cooldownTimer = 0;

		SoundManager.instance.PlaySound3D("DefaultShoot", transform.position);
	}

	public void takeDamage(float damageAmount)
	{
		health -= damageAmount;

		if (health <= 0)
		{
			OnDied.Invoke();
			Destroy(gameObject);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Bullet"))
		{
			takeDamage(1);
			Destroy(collision.gameObject);
		}
	}
}