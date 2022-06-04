using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected void SelfDeletion()
    {
        Destroy(gameObject);
    }
    private void Update()
    {
        
    }
    protected virtual void Collision()
    {

    }
}
