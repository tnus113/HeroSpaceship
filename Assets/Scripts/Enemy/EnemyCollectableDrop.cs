using UnityEngine;

public class EnemyCollectableDrop : MonoBehaviour
{
    [SerializeField]
    private float _chanceOfCollectableDrop;

    private CollectableSpawner _spawner;

	private void Awake()
	{
		_spawner = FindObjectOfType<CollectableSpawner>();
	}

	public void RandomlyDropCollectable()
	{
		float random = Random.Range(0f, 1f);

        if (_chanceOfCollectableDrop >= random)
        {
            _spawner.SpawnCollectable(transform.position);
        }
    }
}
