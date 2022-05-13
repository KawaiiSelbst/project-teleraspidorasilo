using System.Collections;
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
    [SerializeField]
    private ShieldFragment shieldFragment; 

    private List<ShieldFragment> shieldFragmentInstances = new List<ShieldFragment>();

    private BoxCollider2D boxCollider2D;
    private Vector2 velocity;
    private bool isGrounded;
    private int jumpsCount;

    public delegate void NewShieldDrawHandler();
    public event NewShieldDrawHandler OnNewShieldDraw;

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
            StartCoroutine(DrawFireShield());
        }

        velocity.y += Physics2D.gravity.y * Time.deltaTime;

        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, boxCollider2D.size, 0);

        isGrounded = false;

        foreach (Collider2D hit in hits)
        {
            if (hit.tag == "Player")
                continue;
            if (hit.tag == "Shield")
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
        Rigidbody2D fireBallInstance = Instantiate(
            fireBall,
            transform.position,
            transform.rotation);
        fireBallInstance.velocity = transform.right * 70;
        return fireBallInstance;
    }

    private IEnumerator DrawFireShield()
    {
        if (OnNewShieldDraw != null)
        {
            OnNewShieldDraw();
        }
            for (int i = 0; i < 20; i++)
        {
            if (!Input.GetButton("Fire2"))
            {
                break;
             }
            shieldFragmentInstances.Add(DrawShieldFragment());
            yield return new WaitForSeconds(0.01f);
        }
    }

    private ShieldFragment DrawShieldFragment()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        ShieldFragment fragment = Instantiate(
            shieldFragment,
            mousePosition,
            transform.rotation);
        fragment.SetPlayerComponent(this);
        return fragment;
    }
}

