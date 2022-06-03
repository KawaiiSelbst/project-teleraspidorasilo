using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FireShield : MonoBehaviour
{
    [SerializeField] private ShieldFragment _shieldFragment;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private EdgeCollider2D _edgeColliderPrefab;
    [SerializeField] private int _fragmentsAmount = 20;
    [SerializeField] private float _distanceBetweenSegments = 0.25f;

    private EdgeCollider2D _edgeCollider2D;

    private ShieldFragment _currentFragment;
    private Vector2 _mousePosition;
    private Stack<ShieldFragment> _shieldFragments = new Stack<ShieldFragment>();

    private Vector2 _currentSegment;
    private List<Vector2> _segments = new List<Vector2>();
    private bool _drawingMode;

    private void Start()
    {
        _edgeCollider2D = Instantiate(_edgeColliderPrefab, transform.position, Quaternion.identity).GetComponent<EdgeCollider2D>();
        _edgeCollider2D.transform.parent = null;
    }

    private void Update()
    {
        _mousePosition = Input.mousePosition;

        if (_drawingMode)
        {
            var mousePositionWorld = Camera.main.ScreenToWorldPoint(_mousePosition);
            DrawFireShield(mousePositionWorld);
        }
        
    }

    public void DrawFireShield(Vector2 mousePosition)
    {
        if (_segments.Count < 1)
        {
            _segments.Add(mousePosition);
        }

        if (_drawingMode && _segments.Count < _fragmentsAmount)
        {
            _currentSegment = _segments[_segments.Count - 1];

            if (Vector2.Distance(_currentSegment, mousePosition) > _distanceBetweenSegments)
            {
                _segments.Add(mousePosition);
            }
        }
        _lineRenderer.positionCount = _segments.Count;
        _lineRenderer.SetPositions(Util.Vec2toVec3(_segments.ToArray()));
        _edgeCollider2D.SetPoints(_segments);

    }

    private void DrawLine()
    {
        var i = 0;
        foreach (var segment in _segments)
        {
            
            _lineRenderer.SetPosition(i, segment);
            i += 1;
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
        foreach (var segment in _segments)
        {
            //GameObject.Destroy(segment.gameObject);
        }
        _lineRenderer.SetPositions(new Vector3[] { Vector3.zero });
        _segments.Clear();
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

public static class Util
{
    public static Vector3[] Vec2toVec3(Vector2[] Vec2) 
    {
        List<Vector3> list = new List<Vector3>();
        foreach (Vector2 v in Vec2)
        {
            list.Add(v);
        }
        return list.ToArray();
    }
}