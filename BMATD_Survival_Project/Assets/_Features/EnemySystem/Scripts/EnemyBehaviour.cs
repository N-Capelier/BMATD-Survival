using System.Collections;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
	private EnemyConfig _config;

	[SerializeField]
	private Animator _animator;

	private float _attackCooldownDelta = 0f;

	private bool _isInRange = false;
	private Player _player;

	private void Start()
	{
		_attackCooldownDelta = 0f;
	}

	public void Initialize(Player player, EnemyConfig config)
	{
		_player = player;
		_config = config;
	}

	private void Update()
	{
		HandleMovement();
		HandleAttack();
	}

	private void HandleMovement()
	{
		Vector3 direction = _player.transform.position - transform.position;

		if (direction.magnitude < _config.MinRange)
		{
			_isInRange = true;
		}
		else if (direction.magnitude > _config.MaxRange)
		{
			_isInRange = false;
		}

		if (_isInRange == false)
		{
			transform.Translate(direction.normalized * _config.MoveSpeed * Time.deltaTime, Space.World);
			_animator.SetBool("IsMoving", true);
		}
		else
		{
			_animator.SetBool("IsMoving", false);
		}

		transform.forward = direction.normalized;
	}

	private void HandleAttack()
	{
		_attackCooldownDelta -= Time.deltaTime;
		//The same as
		//_attackCooldownDelta = _attackCooldownDelta - Time.deltaTime;

		if (_isInRange == false)
			return;

		if (_attackCooldownDelta > 0f)
			return;

		_animator.SetTrigger("StartAttacking");
		_attackCooldownDelta = _config.AttackCooldown;
		StartCoroutine(AttackWithDelayCoroutine(0.5f));
	}

	IEnumerator AttackWithDelayCoroutine(float delay)
	{
		yield return new WaitForSeconds(delay);

		if (_isInRange == false)
			yield break;

		_player.PlayerHealthBar.LoseHealthPoints(10);
	}
}