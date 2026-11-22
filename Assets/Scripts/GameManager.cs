using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Multiplayer.Center.Common;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [Header("Options")]
    [Range(0f, 1f)] public float SpawnRate = .5f;
    [Range(0f, 1f)] public float DifficultyIncreaseByUnitClimbed = 0.05f;
    public int MinObstaclesPerRow = 1;
    public int MaxObstaclesPerRow = 3;
    public Vector2 ObstacleSpacing = Vector2.one;
    public float ObstacleWallPadding = 1f;
    public float NoiseScale = 1f;
    public float DeadzoneSpeedIncreaseByDifficultyPoint = 0.5f;

    [Header("Components")]
    public SpriteRenderer _bounds;
    public PlayerMovement Player;
    public TMPro.TextMeshProUGUI CurrentScoreText;
    public TMPro.TextMeshProUGUI DeadzoneText;
    public Deadzone Deadzone;
    public GameObject GameOverPanel;

    // ---------------------------

    private List<Obstacle> _obstaclePool;
    private Vector2 _initialPlayerPos;
    private float _currentPlayerMaxHeight;
    private bool _isGameStarted = true;
    private MonoList<Obstacle> _currentObstacleInstances = new();
    private float _lastGeneratedHeight = 0f;
    private float _initDeadzoneSpeed;

    public override bool DestroyOnLoad => true;

    public void EndGame()
    {
        // TODO show UI
        StartCoroutine(RestartGame());
        GameOverPanel.SetActive(true);
    }

    private IEnumerator RestartGame()
    {
        yield return PlayDeathAnim();
        Time.timeScale = 0f;
    }

    private IEnumerator PlayDeathAnim()
    {
        Player.Blood.gameObject.SetActive(true);
        var r = Player.GetComponent<SpriteRenderer>();
        var rr = Player.hatController.hatSpriteRenderer;
        Time.timeScale = 0.5f;

        if (r != null)
        {
            r.color = Color.red;
        }

        if (rr != null)
        {
            rr.gameObject.SetActive(false);
            //r.color = Color.red;
        }
        else
        {
            Debug.Log("FAAHH");
        }

        for (int i = 0; i < 10; i++)
        {
            if (i % 2 == 0)
            {
                Player.transform.position += new Vector3(0.025f, 0f, 0f);
            }
            else
            {
                Player.transform.position -= new Vector3(0.025f, 0f, 0f);
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    void Start()
    {
        Time.timeScale = 1f;
        _obstaclePool = Resources.LoadAll<Obstacle>("Prefabs").ToList();
        _initialPlayerPos = Player.transform.position;
        _initDeadzoneSpeed = Deadzone.deadZoneSpeed;
        SpawnObstacles(_bounds.bounds.min.y, _bounds.bounds.max.y * 200);
    }

    void Update()
    {
        UpdateMaxPlayerHeight();
        UpdateObstacles();
        UpdateUI();
    }

    private void UpdateDeadzoneSpeed()
    {
        Deadzone.deadZoneSpeed = _initDeadzoneSpeed + (DeadzoneSpeedIncreaseByDifficultyPoint * GetDifficultyForHeight(_currentPlayerMaxHeight));
    }

    private void UpdateUI()
    {
        int score = Mathf.RoundToInt(_currentPlayerMaxHeight);

        CurrentScoreText.text = score.ToString();
        DeadzoneText.text = $"Certain Death : {Mathf.RoundToInt(Player.transform.position.y - Deadzone.transform.position.y)}m ↓";
        CurrentScoreText.text = score.ToString();

        ScoreManager.I.SetScore(score);

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
