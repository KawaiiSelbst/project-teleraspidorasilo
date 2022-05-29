using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Explosion : MonoBehaviour
{
    [SerializeField] private float _lifeTime = .25f;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(_lifeTime);
        GameObject.Destroy(gameObject);
    }

    void Update()
    {
        
    }
}
