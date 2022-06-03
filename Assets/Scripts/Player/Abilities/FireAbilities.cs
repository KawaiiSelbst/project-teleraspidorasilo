using UnityEngine;

public class FireAbilities : MonoBehaviour
{
    [SerializeField] private FireShield _fireShieldPrefab;
    private FireShield _fireShield;

    private void Start()
    {
        _fireShield = Instantiate(_fireShieldPrefab);
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            _fireShield.DrawingModeOn();
        }

        if (Input.GetButtonUp("Fire2"))
        {
            _fireShield.DrawingModeOff();
        }
    }
}
