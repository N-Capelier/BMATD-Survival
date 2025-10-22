using UnityEngine;

[CreateAssetMenu(fileName = "EnemyConfig", menuName = "Scriptable Objects/EnemyConfig")]
public class EnemyConfig : ScriptableObject
{
	[SerializeField]
	private float _moveSpeed = 10f;
	public float MoveSpeed => _moveSpeed;

	[SerializeField]
	private float _minRange = .5f;
	public float MinRange => _minRange;

	[SerializeField]
	private float _maxRange = 1.0f;
	public float MaxRange => _maxRange;

	[SerializeField]
	private float _attackCooldown = 1.5f;
	public float AttackCooldown => _attackCooldown;
}
