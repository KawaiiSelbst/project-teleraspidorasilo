using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    [SerializeField] private Rigidbody2D _fireBall;

    private List<Rigidbody2D> _fireballInstances;
    private BoxCollider2D _boxCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
        StartCoroutine(SomeCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
    }

    private Rigidbody2D ShootFireball()
    {
        Rigidbody2D fireBallInstance = Instantiate(_fireBall, transform.position, transform.rotation);
        fireBallInstance.velocity = transform.right * -30;
        return fireBallInstance;
    }
    
    IEnumerator SomeCoroutine()
    {
        //Declare a yield instruction.
        WaitForSeconds wait = new WaitForSeconds(1);

        while (true)
        {
            var fireballCollider = ShootFireball().GetComponent<Collider2D>();
            Physics2D.IgnoreCollision(fireballCollider, _boxCollider2D);
            yield return wait; //Pause the loop for 3 seconds.
        }
    }
}
