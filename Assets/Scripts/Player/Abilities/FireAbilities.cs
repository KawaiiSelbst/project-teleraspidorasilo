using System;
using UnityEngine;

public class FireAbilities : MonoBehaviour
{
    [SerializeField] private FireShield _fireShieldPrefab;
    [SerializeField] private Rigidbody2D _fireBallPrefab;

    private FireShield _fireShield;

    public Action<Collider2D> OnFireballInstantiate;

    private void Start()
    {
        _fireShield = Instantiate(_fireShieldPrefab);
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            _fireShield.DrawingModeOn();
        }

        if (Input.GetButtonUp("Fire2"))
        {
            _fireShield.DrawingModeOff();
        }
        if (Input.GetButtonDown("Fire1"))
        {
            var FireballCollider = ShootFireball().GetComponent<Collider2D>();
            OnFireballInstantiate(FireballCollider);
        }
    }
    private Rigidbody2D ShootFireball()
    {
        Rigidbody2D fireBallInstance = Instantiate(
            _fireBallPrefab,
            transform.position,
            Quaternion.identity);
        fireBallInstance.velocity = transform.right * 30;
        return fireBallInstance;
    }
}
