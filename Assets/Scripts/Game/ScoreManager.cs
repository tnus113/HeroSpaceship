using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    public UnityEvent OnScoreChanged;

    public int Score {  get; private set; }

    public void addScore(int amount)
    {
        Score += amount;
        OnScoreChanged.Invoke();
    }
}
