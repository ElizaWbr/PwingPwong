using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPaddleController : MonoBehaviour
{
    [Header("Player")]
    public string inputKey = "PlayerOne";
    public bool isPlayerOne = false;
    public bool isAuto = false;

    [Header("Speed")]
    public float autoSpeed = 4f;
    public float speed = 8f;
    
    [Header("Screen limit")]
    public float screenLimit = 3.5f;

    private PongInputSystem _inputSystem;
    private InputAction _moveAction;

    private Vector2 _moveInput;
    private Rigidbody2D _rb;
    private GameObject _ball;

    private void Awake()
    {
        _inputSystem = new PongInputSystem();
        _moveAction = _inputSystem.asset.FindActionMap(inputKey).FindAction("Move");
    }
    private void OnEnable()
    {
        _moveAction.Enable();
    }
    private void OnDisable()
    {
        _moveAction.Disable();
    }
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _ball = GameObject.Find("Ball");

        ApplyColor();
    }
    private void Update()
    {
        if (isAuto)
        {
            CheckAutoMovement();
        }
        else
        {
            CheckPlayerMovement();
        }
    }
    private void ApplyColor() 
    {
        Color selectedColor = SaveController.Instance.GetPlayerColor(isPlayerOne);
        gameObject.GetComponent<SpriteRenderer>().color = selectedColor;
    }
    private void CheckAutoMovement()
    {
        if(_ball != null)
        {
            float targetY = Mathf.Clamp(_ball.transform.position.y, -screenLimit, screenLimit);
            Vector2 targetPosition = new Vector2(transform.position.x, targetY);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, Time.deltaTime * autoSpeed);
        }
    }
    private void CheckPlayerMovement()
    {
        /* This will return 1 or -1 depending on the pressed key */
        float triggeredAction = _moveAction.ReadValue<float>();

        if (triggeredAction == 1 || triggeredAction == -1)
        {
            /* Current position, up movement, speed, time AND the direction */
            Vector3 newPosition = transform.position + (triggeredAction * Vector3.up * speed * Time.deltaTime);
            newPosition.y = Mathf.Clamp(newPosition.y, -screenLimit, screenLimit);

            transform.position = newPosition;
        }
    }
}
