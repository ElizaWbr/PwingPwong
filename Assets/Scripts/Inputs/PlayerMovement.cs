using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PongInputSystem inputSystem;

    private void Awake()
    {
        inputSystem = new PongInputSystem();
    }

    private void OnEnable()
    {
        inputSystem.PlayerOne.Move.Enable();
        inputSystem.PlayerTwo.Move.Enable();
    }

    private void OnDisable()
    {
        inputSystem.PlayerOne.Move.Disable();
        inputSystem.PlayerTwo.Move.Disable();
    }
}
