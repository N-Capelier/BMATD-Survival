using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LootBox : MonoBehaviour
{
	[SerializeField]
	private int _playerLevel = 1;

	[SerializeField]
	private List<Loot> _lootTable = new();

    [SerializeField]
    private int _commonItemWeight = 0;

	[SerializeField]
	private int _uncommonItemWeight = 0;

	[SerializeField]
	private int _rareItemWeight = 0;

	[SerializeField]
	private int _legendaryItemWeight = 0;

	void Start()
    {
		//BasicPickRandomLoot();
		//BetterPickRandomLoot();
		//ScalablePickRandomLook();

		InvokeRepeating(nameof(ScalablePickRandomLook), 0f, 3f);
    }

	private void BasicPickRandomLoot()
	{
        //Select a random item
		int total = _commonItemWeight + _uncommonItemWeight + _rareItemWeight + _legendaryItemWeight;

		int random = Random.Range(0, total);

		if(random <= _commonItemWeight)
			Debug.Log("Loot : common item");

		if(random <= _commonItemWeight + _uncommonItemWeight)
			Debug.Log("Loot : uncommon item");

		if (random <= _commonItemWeight + _uncommonItemWeight + _rareItemWeight)
			Debug.Log("Loot : rare item");

		//=> if the picked item is not any of the categories above, then it MUST be part of the last category
		//if (random <= _commonItemWeight + _uncommonItemWeight + _rareItemWeight + _legendaryItemWeight)
		//	Debug.Log("Loot : legendary item");

		Debug.Log("Loot : legendary item");
	}

	private void BetterPickRandomLoot()
	{
		int totalWeight = 0;

		for (int i = 0; i < _lootTable.Count; i++)
		{
			totalWeight += _lootTable[i].Weight;
		}

		int random = Random.Range(0, totalWeight);

		int currentWeight = 0;

		for (int i = 0; i < _lootTable.Count - 1; i++)
		{
			currentWeight += _lootTable[i].Weight;

			if(random <= currentWeight)
			{
				Debug.Log(_lootTable[i].Rarity);
				return;
			}
		}

		Debug.Log(_lootTable[^1].Rarity);
	}

	private void ScalablePickRandomLook()
	{
		float totalWeight = 0;

		Debug.Log("===================");
		Debug.Log("Picking random item for level " + _playerLevel);

		for (int i = 0; i < _lootTable.Count; i++)
		{
			totalWeight += _lootTable[i].WeightsCurve.Evaluate(_playerLevel);
			Debug.Log($"Rarity: {_lootTable[i].Rarity} has weight = {_lootTable[i].WeightsCurve.Evaluate(_playerLevel)}");
		}

		float random = Random.Range(0, totalWeight);

		float currentWeight = 0;

		for (int i = 0; i < _lootTable.Count - 1; i++)
		{
			currentWeight += _lootTable[i].WeightsCurve.Evaluate(_playerLevel);

			if (random <= currentWeight)
			{
				Debug.Log(_lootTable[i].Rarity);
				return;
			}
		}

		Debug.Log(_lootTable[^1].Rarity);
	}

	[Serializable]
	private struct Loot
	{
		public string Rarity;
		public int Weight;
		public AnimationCurve WeightsCurve;
	}
}
