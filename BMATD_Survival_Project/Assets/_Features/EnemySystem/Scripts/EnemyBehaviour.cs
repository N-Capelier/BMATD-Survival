using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
	[SerializeField] private float _moveSpeed = 10f;
	[SerializeField] private float _minRange = .5f;
	[SerializeField] private float _maxRange = 1.0f;

	private bool _isInRange = false;
	private Transform _playerTransform;

	public void Initialize(Transform playerTransform)
	{
		_playerTransform = playerTransform;
	}

	private void Update()
	{
		Vector3 direction = _playerTransform.position - transform.position;

		if(direction.magnitude < _minRange)
		{
			_isInRange = true;
		}
		else if(direction.magnitude > _maxRange)
		{
			_isInRange = false;
		}

		if(_isInRange == false)
		{
			transform.Translate(direction.normalized * _moveSpeed * Time.deltaTime, Space.World);
		}

		transform.forward = direction.normalized;
	}
}
