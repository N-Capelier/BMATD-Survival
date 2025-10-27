using System;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{
	[SerializeField]
	Transform _playerTransform;

	//[SerializeField, Range(0f, 180f)]
	//private float _fieldOfView = 90f;

	[SerializeField, Range(0f, 1f)]
	private float _radFOV = .5f;

	[SerializeField]
	private float _cooldown = 1f;
	private float _cooldownDelta;

	[SerializeField]
	private BulletBehaviour _bulletBehaviour = null;

	[SerializeField]
	private Transform _bulletSpawnPoint = null;

	private bool _isPlayerInView = false;

	/*
	 180 => 0
	 90 => .5
	 0 => 1
	 */

	private void Start()
	{
		_cooldownDelta = 0f;
	}

	private void Update()
	{
		DetectPlayer();
		TryShootPlayer();
	}

	private void DetectPlayer()
	{
		float product = Vector3.Dot(transform.forward, (_playerTransform.position - transform.position).normalized);

		if (product > _radFOV)
		{
			_isPlayerInView = true;
		}
		else
		{
			_isPlayerInView = false;
		}
	}

	private void TryShootPlayer()
	{
		if (_cooldownDelta > 0f)
		{
			_cooldownDelta -= Time.deltaTime;
			return;
		}

		if (_isPlayerInView == false)
		{
			return;
		}

		_cooldownDelta = _cooldown;

		BulletBehaviour newBullet = Instantiate(_bulletBehaviour, _bulletSpawnPoint.position, Quaternion.identity);
		newBullet.Initialize(_playerTransform.position - newBullet.transform.position);
	}
}
