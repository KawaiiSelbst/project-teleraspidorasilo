using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 9;
    [SerializeField]
    private float walkAcceleration = 75;
    [SerializeField]
    private float airAcceleration = 30;
    [SerializeField]
    private float groundDeceleration = 70;
    [SerializeField]
    private float jumpHeight = 4;

    [SerializeField]
    private Rigidbody2D fireBall;

    private BoxCollider2D boxCollider2D;
    private Vector2 velocity;
    private bool isGrounded;
    private int jumpsCount;

    private void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {

        transform.Translate(velocity * Time.deltaTime);

        float moveInput = Input.GetAxisRaw("Horizontal");

        float acceleration = isGrounded ? walkAcceleration : airAcceleration;
        float deceleration = isGrounded ? groundDeceleration : 0;

        if (moveInput != 0)
        {
            velocity.x = Mathf.MoveTowards(velocity.x, speed * moveInput, acceleration * Time.deltaTime);
        }
        else
        {
            velocity.x = Mathf.MoveTowards(velocity.x, 0, deceleration * Time.deltaTime);
        }

        if (isGrounded)
        {
            velocity.y = 0;
            jumpsCount = 2;
        }

        if (Input.GetButtonDown("Jump") && jumpsCount > 0)
        {
            jumpsCount -= 1;
            velocity.y = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics2D.gravity.y));
        }

        if (Input.GetButtonDown("Fire1"))
        {
            ShootFireball();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            DrawFireShield();
        }

        velocity.y += Physics2D.gravity.y * Time.deltaTime;

        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, boxCollider2D.size, 0);

        isGrounded = false;

        foreach (Collider2D hit in hits)
        {
            if (hit == boxCollider2D)
                continue;

            ColliderDistance2D colliderDistance = hit.Distance(boxCollider2D);

            if (colliderDistance.isOverlapped)
            {
                transform.Translate(colliderDistance.pointA - colliderDistance.pointB);

                if (Vector2.Angle(colliderDistance.normal, Vector2.up) < 90 && velocity.y < 0)
                {
                    isGrounded = true;
                }
            }
        }
    }
    private Rigidbody2D ShootFireball()
    {
        Vector2 fireBallPosition = transform.position + Vector3.right;
        Rigidbody2D fireBallInstance = Instantiate(fireBall, fireBallPosition, transform.rotation);
        fireBallInstance.velocity = transform.right * 70;
        return fireBallInstance;
    }
    private void DrawFireShield()
    {

    }
}

