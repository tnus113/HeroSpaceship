using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class ScoreUI : MonoBehaviour
{
    private TMP_Text _scoreText;

	private void Awake()
	{
		_scoreText = GetComponent<TMP_Text>();
	}

	public void UpdateScore(ScoreManager scoreManager)
	{
		_scoreText.text = $"Score: {scoreManager.Score}";
	}
}
