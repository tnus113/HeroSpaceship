using UnityEngine;

namespace TopDown.Shooting
{
	public class GunController : MonoBehaviour
	{
		[Header("Cooldown")]
		[SerializeField] private float cooldown = 0.25f;
		private float cooldownTimer;

		[Header("References")]
		[SerializeField] private GameObject bulletPrefab;
		[SerializeField] private Transform firepoint;
		[SerializeField] private Animator muzzleFlashAnimator;

		private void Update()
		{
			cooldownTimer += Time.deltaTime;
		}

		private void Shoot()
		{
			if (cooldownTimer < cooldown) return;

			GameObject bullet = Instantiate(bulletPrefab,firepoint.position,firepoint.rotation,null);
			bullet.GetComponent<Projectile>().ShootBullet(firepoint);

			muzzleFlashAnimator.SetTrigger("shoot");
			cooldownTimer = 0;

			SoundManager.instance.PlaySound3D("DefaultShoot", transform.position);
		}

		#region Input
		private void OnShoot()
		{
			Shoot();
		}
		#endregion
	}
}
