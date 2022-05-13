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
        playerComponent.OnNewShieldDraw -= this.SelfDeletion;
        Destroy(gameObject);
    }

    public void SetPlayerComponent(Player player)
    {
        playerComponent = player;
        playerComponent.OnNewShieldDraw += this.SelfDeletion;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("HUY");
    }
}
