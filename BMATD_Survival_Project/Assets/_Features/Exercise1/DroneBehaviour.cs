using UnityEngine;

public class DroneBehaviour : MonoBehaviour
{
	[SerializeField]
	private Transform _playerTransform = null;

	[SerializeField]
	private Transform _enemyTransform = null;

	[SerializeField]
	private float _rotationAroundPlayerSpeed = 1f;

	[SerializeField]
	private float _rotationSpeed = 1f;

	[Header("Shooting"), SerializeField]
	private float _cooldown = 1f;
	private float _cooldownDelta;
	[SerializeField]
	private BulletBehaviour _bulletBehaviour = null;
	[SerializeField]
	private Transform _bulletSpawnPoint = null;
	[SerializeField]
	private Transform _turretTransform = null;

	private float _startYPosition = 0f;

	[SerializeField]
	private float _wobbleFrequency = 1f;
	[SerializeField]
	private float _wobbleAmplitude = 1f;

	private void Start()
	{
		_cooldownDelta = 0f;
		_startYPosition = transform.position.y;
	}

	private void TryShootEnemy()
	{
		if (_cooldownDelta > 0f)
		{
			_cooldownDelta -= Time.deltaTime;
			return;
		}

		_cooldownDelta = _cooldown;

		BulletBehaviour newBullet = Instantiate(_bulletBehaviour, _bulletSpawnPoint.position, Quaternion.identity);
		newBullet.Initialize(_turretTransform.position - newBullet.transform.position);
	}

	private void Update()
	{
		//transform.RotateAround(_playerTransform.position, Vector3.up, _rotationAroundPlayerSpeed * Time.deltaTime);

		Vector3 offset = transform.position - _playerTransform.position;
		transform.position = _playerTransform.position + Quaternion.AngleAxis(_rotationAroundPlayerSpeed * Time.deltaTime, Vector3.up) * offset;


		Vector3 direction = _enemyTransform.position - transform.position;

		if (direction.sqrMagnitude < .001f)
			return;

		Quaternion targetRotation = Quaternion.LookRotation(direction);
		transform.rotation = targetRotation;

		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

		float newYPosition = _startYPosition + Mathf.Sin(Time.time * _wobbleFrequency) * _wobbleAmplitude;
		transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);

		TryShootEnemy();

































		//transform.position = new Vector3(transform.position.x, _startYPosition + (Mathf.Sin(Time.time * _wobbleSpeed) * _wobbleAmplitude), transform.position.z);
	}
}
