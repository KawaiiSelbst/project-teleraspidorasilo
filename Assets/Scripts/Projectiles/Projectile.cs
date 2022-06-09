using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected virtual void SelfDeletion()
    {
        Destroy(gameObject);
    }
}
