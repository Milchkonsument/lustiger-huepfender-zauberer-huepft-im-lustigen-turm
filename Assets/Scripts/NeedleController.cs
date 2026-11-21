using UnityEngine;
using UnityEngine.InputSystem;

public class NeedleController : MonoBehaviour
{
    private InputAction _moveAction;
    public int abilitySteps;
    private InputAction _upAction;
    private Transform targetPosition;
    private float _gridSize;
    private Transform _playerTransform;
    private PlayerMovement _playerMovement;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _moveAction = InputSystem.actions.FindAction("Player/Movement");
        _upAction = InputSystem.actions.FindAction("Player/Jump");
        _gridSize = this.GetComponent<SpriteRenderer>().bounds.size.y;
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _playerMovement = _playerTransform.GetComponent<PlayerMovement>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerMovement.isUsingAbility)
        {
            if (_upAction.triggered)
            {
                abilitySteps--;
                
                transform.position = new Vector3(transform.position.x, transform.position.y + _gridSize,
                    transform.position.z);
            }

            if (_moveAction.triggered)
            {
                abilitySteps--;
                transform.position = new Vector3(transform.position.x + _moveAction.ReadValue<Vector2>().x * _gridSize,
                    transform.position.y, transform.position.z);
            }
        }
        
        if (abilitySteps <= 0)
        {
            EndAbility();
        }
    }

    public void EndAbility()
    {
        _playerMovement.setAbility(false);
        _playerTransform.position = transform.position;
        Destroy(gameObject);
    }
    
}
