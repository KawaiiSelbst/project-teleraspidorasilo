using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return new WaitForSeconds(10);
        SelfDeletion();
    }
    private void SelfDeletion()
    {
        Destroy(gameObject);
    }
}
