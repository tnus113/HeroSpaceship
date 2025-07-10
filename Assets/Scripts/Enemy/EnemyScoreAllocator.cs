using UnityEngine;

public class EnemyScoreAllocator : MonoBehaviour
{
    [SerializeField]
    private int _killScore;

    private ScoreManager _scoreManager;

	private void Awake()
	{
		_scoreManager = FindObjectOfType<ScoreManager>();
	}

	public void AllocateScore()
	{
		_scoreManager.addScore(_killScore);
	}
}
