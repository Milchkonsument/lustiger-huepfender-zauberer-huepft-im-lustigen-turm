using System.Collections.Generic;
using System.Linq;
using TreeEditor;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Options")]
    [Range(0f, 1f)] public float SpawnRate = .5f;
    [Range(0f, 1f)] public float DifficultyIncreaseByUnitClimbed = 0.05f;
    public int MinObstaclesPerRow = 1;
    public int MaxObstaclesPerRow = 3;
    public Vector2 ObstacleSpacing = Vector2.one;
    public float ObstacleWallPadding = 1f;

    [Header("Components")]
    public SpriteRenderer _bounds;
    public PlayerMovement Player;
    public float NoiseScale = 1f;

    // ---------------------------

    private List<Obstacle> _obstaclePool;
    private Vector2 _initialPlayerPos;
    private float _currentPlayerMaxHeight;
    private bool _isGameStarted = true;
    private MonoList<Obstacle> _currentObstacleInstances = new();
    private float _lastGeneratedHeight = 0f;

    public void EndGame()
    {
        // TODO show UI
        RestartGame();
    }

    public void RestartGame()
    {
        _currentObstacleInstances.ClearAndDestroy();
        Player.transform.position = _initialPlayerPos;
        _currentPlayerMaxHeight = 0f;
    }

    void Awake()
    {
        _obstaclePool = Resources.LoadAll<Obstacle>("Prefabs").ToList();
        _initialPlayerPos = Player.transform.position;
    }

    void Start()
    {
        SpawnObstacles(_bounds.bounds.min.y, _bounds.bounds.max.y * 200);
    }

    void Update()
    {
        UpdateMaxPlayerHeight();
        UpdateObstacles();
    }

    private void UpdateObstacles()
    {
        UpdateDestroyObstacles();
        UpdateSpawnObstacles();
    }

    private void UpdateDestroyObstacles()
    {
        var old = _currentObstacleInstances.Where(o => o.gameObject.transform.position.y < Player.transform.position.y - _bounds.bounds.extents.y * 2).ToList();
        _currentObstacleInstances.RemoveAndDestroy(old);
    }

    private void UpdateSpawnObstacles()
    {
        if (_lastGeneratedHeight - Player.transform.position.y < _bounds.bounds.size.y * 2f)
        {
            SpawnObstacles(_lastGeneratedHeight, _lastGeneratedHeight + _bounds.bounds.size.y);
        }
    }

    private void UpdateMaxPlayerHeight()
    {
        _currentPlayerMaxHeight = Mathf.Max(_currentPlayerMaxHeight, Player.transform.position.y);
    }

    float GetDifficultyForHeight(float height)
    {
        return Mathf.RoundToInt(height * DifficultyIncreaseByUnitClimbed);
    }

    private void SpawnObstacles(float minY, float maxY)
    {
        _lastGeneratedHeight = maxY;

        for (float y = minY; y < maxY; y += ObstacleSpacing.y)
        {
            int cnt = 0;

            for (float x = _bounds.bounds.min.x + ObstacleWallPadding; x < _bounds.bounds.max.x - ObstacleWallPadding; x += ObstacleSpacing.x)
            {
                if (Mathf.PerlinNoise(9.456f + x * NoiseScale, y * NoiseScale) < SpawnRate)
                {
                    cnt++;
                    var o = Instantiate(_obstaclePool.Where(o => o.Difficulty <= GetDifficultyForHeight(y)).ToList().PickRandom(), new Vector3(x, y, 0f), Quaternion.identity);
                }
            }
        }
    }
}
