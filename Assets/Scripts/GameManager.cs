using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [Header("Options")]
    [Range(0.1f, 10f)] public float InitialSpawnRate = 1f;
    [Range(0f, 1f)] public float DifficultyIncreaseByUnitClimbed = 0.05f;

    [Header("Components")]
    public SpriteRenderer TowerBounds;
    public BoxCollider2D Deadzone;

    // ---------------------------

    private List<Obstacle> _obstacles;
    private Bounds _towerBounds;
    private BoxCollider2D _player;

    void Awake()
    {
        _obstacles = Resources.LoadAll<Obstacle>("Prefabs").ToList();
        _towerBounds = TowerBounds.bounds;
    }

    void Update()
    {

    }
}
