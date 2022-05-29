using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class FireBall : Projectile
{
    [SerializeField] private float _lifeTime = 10f;
    [SerializeField] private GameObject _explotionPrefab;
    private bool _isAlive = true;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(_lifeTime);
        SelfDeletion();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isAlive)
        {
            _isAlive = false;
            SelfDeletion();
            Instantiate(_explotionPrefab, transform.position, transform.rotation);
        }

    }
}
