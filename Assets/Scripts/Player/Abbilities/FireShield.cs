using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FireShield : MonoBehaviour
{
    [SerializeField] ShieldFragment _shieldFragment;
    ShieldFragment _currentFragment;
    public bool _drawingMode;

    Vector2 mousePotition;

    private Stack<ShieldFragment> _shieldFragmentInstances = new Stack<ShieldFragment>();
    [SerializeField] private int _fragmentsAmount = 20;
    [SerializeField] private float _distanceBetweenSegments = 0.25f;

    //private ShieldFragment _currentFragment;

    private void Update()
    {
        mousePotition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (_drawingMode)
        {
            DrawFireShield(mousePotition);
        }
    }

    public void DrawFireShield(Vector2 mousePosition)
    {
        if (_shieldFragmentInstances.Count < 1)
        {
            _shieldFragmentInstances.Push(DrawShieldFragment(mousePosition));
        }

        if (_drawingMode && _shieldFragmentInstances.Count < _fragmentsAmount)
        {
            _currentFragment = _shieldFragmentInstances.Peek();

            if (Vector2.Distance(_currentFragment.transform.position, mousePosition) > _distanceBetweenSegments)
            {
                _shieldFragmentInstances.Push(DrawShieldFragment(mousePosition));
            }
        }
        
    }

    private ShieldFragment DrawShieldFragment(Vector2 pointPosition)
    {
        ShieldFragment fragment = Instantiate(
            original: _shieldFragment,
            position: pointPosition,
            rotation: Quaternion.identity
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


    public void DrawingModeOn()
    {
        Clear();
        _drawingMode = true;
    }

    public void DrawingModeOff()
    {
        _drawingMode = false;
    }
}
