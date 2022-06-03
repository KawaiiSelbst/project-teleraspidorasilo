using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldFragment : MonoBehaviour
{
    private CircleCollider2D circleCollider2d;
    
    private Player playerComponent;

    private void Start()
    {
        
        circleCollider2d = GetComponent<CircleCollider2D>();
    }
    private void SelfDeletion()
    {
        throw new NotImplementedException();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        throw new NotImplementedException();
    }
}
