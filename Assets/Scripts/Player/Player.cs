using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 9;
    [SerializeField] private float _walkAcceleration = 75;
    [SerializeField] private float _airAcceleration = 30;
    [SerializeField] private float _groundDeceleration = 70;
    [SerializeField] private float _jumpHeight = 4;

    private BoxCollider2D _boxCollider2D;
    private Vector2 _velocity;
    private bool _isGrounded;
    private int _jumpsCount;


    public event Action<Vector3> CameraToPlayerWrap;

    private void Awake()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {

        transform.Translate(_velocity * Time.deltaTime);

        CameraToPlayerWrap(transform.position);

        float moveInput = Input.GetAxisRaw("Horizontal");

        float acceleration = _isGrounded ? _walkAcceleration : _airAcceleration;
        float deceleration = _isGrounded ? _groundDeceleration : 0;

        if (moveInput != 0)
        {
            _velocity.x = Mathf.MoveTowards(_velocity.x, _speed * moveInput, acceleration * Time.deltaTime);
        }
        else
        {
            _velocity.x = Mathf.MoveTowards(_velocity.x, 0, deceleration * Time.deltaTime);
        }

        if (_isGrounded)
        {
            _velocity.y = 0;
            _jumpsCount = 2;
        }

        if (Input.GetButtonDown("Jump") && _jumpsCount > 0)
        {
            _jumpsCount -= 1;
            _velocity.y = Mathf.Sqrt(2 * _jumpHeight * Mathf.Abs(Physics2D.gravity.y));
        }

        _velocity.y += Physics2D.gravity.y * Time.deltaTime;

        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, _boxCollider2D.size, 0);

        _isGrounded = false;

        foreach (Collider2D hit in hits)
        {
            if (hit.GetComponent<Player>()) { continue; }
            if (hit.GetComponent<FireShield>()) { continue; }
            if (hit.GetComponent<FireBall>()) { continue; }


            ColliderDistance2D colliderDistance = hit.Distance(_boxCollider2D);

            if (colliderDistance.isOverlapped)
            {
                transform.Translate(colliderDistance.pointA - colliderDistance.pointB);

                if (Vector2.Angle(colliderDistance.normal, Vector2.up) < 90 && _velocity.y < 0)
                {
                    _isGrounded = true;
                }
            }
        }
    }
}

