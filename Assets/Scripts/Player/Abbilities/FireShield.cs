using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShield : MonoBehaviour
{
    [SerializeField] ShieldFragment _shieldFragment;

    private List<ShieldFragment> _shieldFragmentInstances = new List<ShieldFragment>();

    public event Action OnNewShieldDraw;

    public IEnumerator DrawFireShield()
    {
        for (int i = 0; i < 20; i++)
        {
            if (!Input.GetButton("Fire2"))
            {
                break;
            }
            _shieldFragmentInstances.Add(DrawShieldFragment());
            yield return new WaitForSeconds(0.01f);
        }
    }

    private ShieldFragment DrawShieldFragment()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        ShieldFragment fragment = Instantiate(
            original: _shieldFragment,
            position: mousePosition,
            rotation: new Quaternion()
            );
        return fragment;
    }

    private void Clear()
    {
        foreach (var fragment in _shieldFragmentInstances)
        {
            GameObject.Destroy(fragment.gameObject);
        }
        _shieldFragmentInstances.Clear();
    }

    public void DrawNewFireShield()
    {
        Clear();
        StartCoroutine(DrawFireShield());
    }
}
