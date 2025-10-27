using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
	[SerializeField]
	private float _moveSpeed = 10f;

	private Vector3 _moveDirection;

	public void Initialize(Vector3 moveDirection)
	{
		_moveDirection = moveDirection;
		Destroy(gameObject, 5f);
	}

	private void Update()
	{
		transform.Translate(_moveSpeed * Time.deltaTime * _moveDirection.normalized);
	}
}
