using UnityEngine;

public class BallController : MonoBehaviour
{
    public LevelManager levelManager;

    private Rigidbody2D _rb;
    public Vector2 startingVelocity = new Vector2(5f, 5f);
    public float speedUp = 1.1f; /* Aumenta 10% da velocidade toda vez que bate na bola */

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string collistionTag = collision.gameObject.tag;

        if (collistionTag == "Wall")
        {
            /* Invert y direction, it touched the roof or the floor */

            Vector2 newVelocity = _rb.linearVelocity;
            newVelocity.y = -newVelocity.y;
            _rb.linearVelocity = newVelocity;
        }
        else if (collistionTag == "PlayerOne" || collistionTag == "PlayerTwo")
        {
            /* Invert x direction, it touched the paddles */

            _rb.linearVelocity = new Vector2(-_rb.linearVelocity.x, _rb.linearVelocity.y);
            _rb.linearVelocity *= speedUp;

            levelManager.PaddlesCollided();
        }
        else if (collistionTag == "PlayerOnePoint")
        {
            levelManager.ScorePlayerOne();
            ResetBall();
        }
        else if (collistionTag == "PlayerTwoPoint")
        {
            levelManager.ScorePlayerTwo();
            ResetBall();
        }
    }

    public void ResetBall()
    {
        transform.position = Vector3.zero;
        if (_rb == null)
        {
            _rb = GetComponent<Rigidbody2D>();
        }
        _rb.linearVelocity = startingVelocity;
    }

}
