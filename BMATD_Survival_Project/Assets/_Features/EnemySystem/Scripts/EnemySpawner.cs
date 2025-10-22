using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField]
	private int _enemiesCount = 4;

	[SerializeField]
	private List<Transform> _spawnPositions;

	[SerializeField]
	private List<EnemyConfig> _enemyConfigs;

	[SerializeField]
	private EnemyBehaviour _enemyPrefab;

	[SerializeField]
	private Player _player;

	private void Start()
	{
		StartCoroutine(GenerateEnemiesCoroutine());
	}

	IEnumerator GenerateEnemiesCoroutine()
	{
		for(int i = 0; i < _enemiesCount; i++)
		{
			int randomPositionIndex = Random.Range(0, _spawnPositions.Count);

			EnemyBehaviour newEnemy = Instantiate(_enemyPrefab, _spawnPositions[randomPositionIndex].position, _spawnPositions[randomPositionIndex].rotation);

			int randomEnemyConfigIndex = Random.Range(0, _enemyConfigs.Count);

			newEnemy.Initialize(_player, _enemyConfigs[randomEnemyConfigIndex]);

			yield return new WaitForSeconds(2f);
		}
	}
}
