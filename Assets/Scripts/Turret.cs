using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    [SerializeField] private Rigidbody2D _fireBall;

    private List<Rigidbody2D> fireballInstances;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SomeCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
    }

    private Rigidbody2D ShootFireball()
    {
        Rigidbody2D fireBallInstance = Instantiate(_fireBall, transform.position, transform.rotation);
        fireBallInstance.velocity = transform.right * -70;
        return fireBallInstance;
    }
    
    IEnumerator SomeCoroutine()
    {
        //Declare a yield instruction.
        WaitForSeconds wait = new WaitForSeconds(1);

        while (true)
        {
            ShootFireball();
            yield return wait; //Pause the loop for 3 seconds.
        }
    }
}
