using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
[RequireComponent(typeof(LineRenderer))]
public class FireShield : MonoBehaviour
{
    [SerializeField] private float _shieldLenght = 3;
    [SerializeField] private float _distanceBetweenSegments = 0.25f;

    private EdgeCollider2D _edgeCollider2D;
    private LineRenderer _lineRenderer;

    private Vector2 _mousePosition;
    private List<Vector2> _segments = new List<Vector2>();

    private float _currentLenght;
    private bool _drawingMode;

    private void Start()
    {
        _edgeCollider2D = GetComponent<EdgeCollider2D>();
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        _mousePosition = Input.mousePosition;

        if (_drawingMode)
        {
            var mousePositionWorld = Camera.main.ScreenToWorldPoint(_mousePosition);
            DrawFireShield(mousePositionWorld);
        }
        if (_segments.Count > 1)
        {
            _edgeCollider2D.enabled = true;
            _lineRenderer.enabled = true;
        }
        
    }

    public void DrawingModeOn()
    {
        Clear();
        _currentLenght = 0;
        _drawingMode = true;
    }

    public void DrawingModeOff()
    {
        _drawingMode = false;
    }

    public void DrawFireShield(Vector2 mousePosition)
    {
        if (_segments.Count < 1)
        {
            _segments.Add(mousePosition);
        }

        if (_drawingMode && _currentLenght < _shieldLenght)
        {
            var currentSegment = _segments[_segments.Count - 1];
            var distance = Vector2.Distance(currentSegment, mousePosition);

            if (distance > _distanceBetweenSegments)
            {
                _currentLenght += distance;
                _segments.Add(mousePosition);
                DrawLine(_segments);
                DrawCollider(_segments);
            }
        }
        else
        {
            DrawingModeOff();
        }

    }

    private void DrawCollider(List<Vector2> segments)
    {
        _edgeCollider2D.SetPoints(segments);
    }

    private void DrawLine(List<Vector2> segments)
    {
        _lineRenderer.positionCount = segments.Count;
        _lineRenderer.SetPositions(Utills.ListVector2_Vector3Array(segments));
    }

    private void Clear()
    {
        _segments.Clear();
        _lineRenderer.SetPositions(Utills.ListVector2_Vector3Array(_segments));
        _edgeCollider2D.enabled = false;
        _lineRenderer.enabled = false;
    }
}
