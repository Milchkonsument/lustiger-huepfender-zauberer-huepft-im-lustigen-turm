using System.Collections.Generic;
using System.Linq;
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

    // ---------------------------

    private List<Obstacle> _obstaclePool;
    private int _currentDifficulty = 0;
    private Vector2 _initialPlayerPos;
    private float _currentPlayerMaxHeight;
    private bool _isGameStarted = true;
    private MonoList<Obstacle> _currentObstacleInstances = new();
    private float _lastGeneratedHeight = 0f;

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
        SpawnObstacles(_bounds.bounds.min.y, _bounds.bounds.max.y * 2);
    }

    void Update()
    {
        UpdateMaxPlayerHeight();
        UpdateHeightBasedGameProps();
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
        if (_lastGeneratedHeight - Player.transform.position.y < _bounds.bounds.size.y)
        {
            SpawnObstacles(_lastGeneratedHeight, _lastGeneratedHeight + _bounds.bounds.size.y);
        }
    }

    private void UpdateMaxPlayerHeight()
    {
        _currentPlayerMaxHeight = Mathf.Max(_currentPlayerMaxHeight, Player.transform.position.y);
    }

    private void UpdateHeightBasedGameProps()
    {
        _currentDifficulty = Mathf.RoundToInt(_currentPlayerMaxHeight * DifficultyIncreaseByUnitClimbed);
    }

    private void SpawnObstacles(float minY, float maxY)
    {
        _lastGeneratedHeight = maxY;

        for (float y = minY; y < maxY; y += ObstacleSpacing.y)
        {
            int cnt = 0;

            // do
            // {
            for (float x = -_bounds.bounds.min.x; x < _bounds.bounds.max.x; x += ObstacleSpacing.x)
            {
                if (Random.Range(0f, 1f) < SpawnRate)
                {
                    cnt++;
                    var o = Instantiate(_obstaclePool.Where(o => o.Difficulty <= _currentDifficulty).ToList().PickRandom());
                    o.transform.position = new Vector3(x, y, 0f);
                }

                if (cnt >= MaxObstaclesPerRow) return;
            }
            // } while (cnt < MinObstaclesPerRow);
        }
    }
}
