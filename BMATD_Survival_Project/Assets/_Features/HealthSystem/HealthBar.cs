using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	[SerializeField]
	private int _maxHealthPoints = 200;
	private int _currentHealthPoints;

	[SerializeField]
	private Image _fillImage;

	private void Start()
	{
		_currentHealthPoints = _maxHealthPoints;
	}

	public void LoseHealthPoints(int healthPointsAmout)
	{
		///"Bad practice"
		/*
		_currentHealthPoints -= healthPointsAmout;

		if(_currentHealthPoints < 0)
			_currentHealthPoints = 0;
		*/
		
		_currentHealthPoints = Mathf.Max(0, _currentHealthPoints - healthPointsAmout);

		_fillImage.fillAmount = (float)_currentHealthPoints / _maxHealthPoints;

		if (_currentHealthPoints == 0)
			Die();
	}

	private void Die()
	{

	}
}
