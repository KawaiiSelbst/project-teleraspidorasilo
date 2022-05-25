using System.Collections;
using UnityEngine;

public class FireBall : Projectile
{
    [SerializeField]
    private int _lifeTime = 10;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(_lifeTime);
        SelfDeletion();
    }
}
