using Cinemachine;
using UnityEngine;

public class ScrollingBackgroundA : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10.0f;
    
    [SerializeField] private Renderer _backgroundOnView;
    [SerializeField] private Renderer _auxBackground;

    [SerializeField] private CinemachineVirtualCamera _vcam;
    private float _horizontalMovement;
    private Vector3 _direction;

    private void Update()
    {
        // Move background to the left
        _direction = Vector3.left.normalized;
        transform.Translate(_direction * (_moveSpeed * Time.deltaTime));

        if (_backgroundOnView.transform.position.x + _backgroundOnView.bounds.size.x < _vcam.transform.position.x)
        {
            var newBackgroundPos = _backgroundOnView.transform.position;
            newBackgroundPos.x = _auxBackground.transform.position.x + _auxBackground.bounds.size.x;
            _backgroundOnView.transform.position = newBackgroundPos;
            (_auxBackground, _backgroundOnView) = (_backgroundOnView, _auxBackground);
        }
    }
}
