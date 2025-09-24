using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField]
	private List<Transform> _spawnPositions;

	[SerializeField]
	private EnemyBehaviour _enemyPrefab;

	[SerializeField]
	private Transform _playerTransform;

	private void Start()
	{
		int random = Random.Range(0, _spawnPositions.Count);

		EnemyBehaviour newEnemy = Instantiate(_enemyPrefab, _spawnPositions[random].position, _spawnPositions[random].rotation);
		newEnemy.Initialize(_playerTransform);
	}
}
