using UnityEngine;

public class SheepSpawnerScript : MonoBehaviour
{
    [SerializeField] private float playerdistance;
    private GameObject _player;

    [Header("Spawn Timing")]
    [SerializeField] private float _spawnTimeInterval = 2f;
    [SerializeField] private float _timeOffset = 0.5f;
    private float _nextSpawnTime;

    [Header("Spawn Settings")]
    [SerializeField] private float _spawnRange = 10f;
    [SerializeField] private GameObject _sheepPrefab;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        SetNextSpawnTime();
    }

    void Update()
    {
        transform.position = new Vector3(transform.position.x, _player.transform.position.y + playerdistance, transform.position.z);

        if (Time.time >= _nextSpawnTime)
        {
            SpawnSheep();
            SetNextSpawnTime();
        }
    }

    void SpawnSheep()
    {
        float spawnOffset = Random.Range(-_spawnRange, _spawnRange);
        Instantiate(_sheepPrefab, new Vector3(transform.position.x + spawnOffset, transform.position.y, transform.position.z), Quaternion.identity);
    }

    void SetNextSpawnTime()
    {
        float randomDelay = Random.Range(-_timeOffset, _timeOffset);
        _nextSpawnTime = Time.time + _spawnTimeInterval + randomDelay;
    }
}