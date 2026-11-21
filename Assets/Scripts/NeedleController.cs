using UnityEngine;
using UnityEngine.InputSystem;

public class NeedleController : MonoBehaviour
{
    private InputAction _moveAction;
    public int abilitySteps;
    public float abilityDelay = 0.0001f;
    public float timeScale = 0.1f;

    private float _nextmoveTime;
    private InputAction _upAction;
    private Transform targetPosition;
    private float _gridSize;
    private Transform _playerTransform;
    private PlayerMovement _playerMovement;
    private CameraController _mainCameraController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _moveAction = InputSystem.actions.FindAction("Player/Movement");
        _upAction = InputSystem.actions.FindAction("Player/Jump");
        _gridSize = this.GetComponent<SpriteRenderer>().bounds.size.y;
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _playerMovement = _playerTransform.GetComponent<PlayerMovement>();
        _playerMovement.hatController.PlayDissovleAnimation();
        _mainCameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        _mainCameraController.needle = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerMovement.isUsingAbility)
        {
            Time.timeScale = timeScale;

            if (_upAction.IsPressed() && Time.time >= _nextmoveTime)
            {
                abilitySteps--;

                transform.position = new Vector3(transform.position.x, transform.position.y + _gridSize,
                    transform.position.z);
                _nextmoveTime = Time.time + abilityDelay;
            }

            if (_moveAction.IsPressed() && Time.time >= _nextmoveTime)
            {
                abilitySteps--;
                transform.position = new Vector3(transform.position.x + _moveAction.ReadValue<Vector2>().x * _gridSize,
                    transform.position.y + _moveAction.ReadValue<Vector2>().y * _gridSize, transform.position.z);
                _nextmoveTime = Time.time + abilityDelay;
            }
        }

        if (abilitySteps <= 0)
        {
            EndAbility();
        }
    }

    public void EndAbility()
    {
        Time.timeScale = 1f;
        AudioManager.Instance.StopSFX();
        AudioManager.Instance.PlaySFX("SewingFinished");
        _playerMovement.setAbility(false);
        _playerTransform.position = transform.position;
        Destroy(gameObject);
    }

}
