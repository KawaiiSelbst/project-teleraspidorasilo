using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class FireBall : Projectile
{
    [SerializeField] private float _lifeTime = 10f;
    [SerializeField] private GameObject _explotionPrefab;
    private GameObject _explotion;
    private bool _isAlive = true;

    private void Awake()
    {
        _explotion = Instantiate(_explotionPrefab, transform.position, transform.rotation);
        _explotion.SetActive(false);
    }
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(_lifeTime);
        SelfDeletion();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isAlive)
        {
            Explode();
        }
    }

    private void Explode()
    {
        _isAlive = false;
        _explotion.transform.SetPositionAndRotation(transform.position, transform.rotation);
        _explotion.SetActive(true);
        Destroy(gameObject);
    }
    protected override void SelfDeletion()
    {
        base.SelfDeletion();
        Destroy(_explotion);
    }
}
