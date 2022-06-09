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

    private IEnumerator Start()
    {
        _explotion = Instantiate(_explotionPrefab, transform.position, transform.rotation);
        _explotion.SetActive(false);
        yield return new WaitForSeconds(_lifeTime);
        SelfDeletion();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isAlive)
        {
            _isAlive = false;
            SelfDeletion();
            _explotion.transform.SetPositionAndRotation(transform.position, transform.rotation);
            _explotion.SetActive(true);
        }

    }
}
