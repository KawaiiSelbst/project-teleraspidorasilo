using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShield : MonoBehaviour
{
    [SerializeField] ShieldFragment _shieldFragment;
    ShieldFragment _previousFragment;

    private Stack<ShieldFragment> _shieldFragmentInstances = new Stack<ShieldFragment>();
    //private ShieldFragment _currentFragment;

    public event Action OnNewShieldDraw;

    public IEnumerator DrawFireShield()
    {
        while (_shieldFragmentInstances.Count < 20 && Input.GetButton("Fire2"))
        {
            var _currentFragment = DrawShieldFragment();
            _shieldFragmentInstances.Push(_currentFragment);
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
