using Unity.Mathematics;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    public int lifes;
    private Rigidbody2D _rigidBody2D;
    private bool _isGrounded = false;
    private bool _enemy = false;
    private int _jumps = 0;
    public Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lifes = 3;
        _rigidBody2D = GetComponent<Rigidbody2D>();
        speed = 0.2f;
        jumpForce = 6f;

    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("movement", speed);
        if (Input.GetKey(KeyCode.D))
        {
            MoveRight();
        }
        if (Input.GetKey(KeyCode.A))
        {
            MoveLeft();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }
    }

    void MoveLeft()
    {
        _rigidBody2D.AddForce(new Vector2(-speed, 0), ForceMode2D.Impulse);

        //_rigidBody2D.MovePosition(_rigidBody2D.position + new Vector2(-speed, 0));
        //_rigidBody2D.MovePosition(new Vector2(speed, 0));

    }

    void MoveRight()
    {
        _rigidBody2D.AddForce(new Vector2(speed, 0), ForceMode2D.Impulse);

        //_rigidBody2D.MovePosition(_rigidBody2D.position + new Vector2(speed, 0));
        // transform.position += Vector3.right * speed;
    }

    void Jump()
    {
        if (_isGrounded || _jumps < 2)
        {
            _rigidBody2D.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            _isGrounded = false;
            _jumps += 1;
        }
    }

    void Attack()
    {

    }

    void LoseLife()
    {
        lifes = lifes - 1;
        if (lifes <= 0)
        {
            GameOver();
        }
    }
    void GameOver()
    {
        Debug.Log("Has Perdido");
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
            _jumps = 0;
        }
        if (collider.gameObject.CompareTag("Enemy"))
        {
            _enemy = true;
            LoseLife();
        }
    }
}
