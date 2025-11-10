using System;
using UnityEngine;

public class TurretBehaviour : MonoBehaviour
{
	[SerializeField]
	Transform _playerTransform;

	[SerializeField, Range(0f, 180f)]
	private float _fieldOfView = 90f;

	//[SerializeField, Range(0f, 1f)]
	//private float _radFOV = .5f;

	[SerializeField]
	private float _cooldown = 1f;
	private float _cooldownDelta;

	[SerializeField]
	private BulletBehaviour _bulletBehaviour = null;

	[SerializeField]
	private Transform _bulletSpawnPoint = null;

	private bool _isPlayerInView = false;

	[SerializeField]
	private Transform _renderer = null;

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

		if (product > _fieldOfView.Remap(180, 0, 0, 1))
		{
			_isPlayerInView = true;
			_renderer.LookAt(_playerTransform.position);
			Vector3 currentRotation = _renderer.rotation.eulerAngles;
			_renderer.rotation = Quaternion.Euler(new Vector3(0f, currentRotation.y, 0f));
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

	private void OnDrawGizmos()
	{
		//Gizmos.color = Color.white;

		//Vector3 startPosition = transform.position;

		//Quaternion rotationLeft = Quaternion.AngleAxis(_fieldOfView * .5f, Vector3.up);
		//Vector3 directionLeft = rotationLeft * transform.forward;
		//Vector3 targetPositionLeft = startPosition + directionLeft * 5f;

		//Quaternion rotationRight = Quaternion.AngleAxis(-_fieldOfView * .5f, Vector3.up);
		//Vector3 directionRight = rotationRight * transform.forward;
		//Vector3 targetPositionRight = startPosition + directionRight * 5f;

		//Gizmos.DrawLine(startPosition, targetPositionLeft);
		//Gizmos.DrawLine(startPosition, targetPositionRight);












		//More elegant solution

		Gizmos.color = Color.red;
		DrawViewLine(_fieldOfView * 0.5f);
		DrawViewLine(-_fieldOfView * 0.5f);
	}

	private void DrawViewLine(float angle)
	{
		Vector3 start = transform.position;
		Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
		Vector3 dir = rotation * transform.forward;
		Gizmos.DrawLine(start, start + dir * 5f);
	}
}
