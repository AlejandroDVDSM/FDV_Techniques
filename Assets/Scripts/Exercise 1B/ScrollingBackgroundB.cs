using Cinemachine;
using UnityEngine;

public class ScrollingBackgroundB : MonoBehaviour
{
    [SerializeField] private Renderer _backgroundOnView;
    [SerializeField] private Renderer _auxBackground;
    
    [SerializeField] private CinemachineVirtualCamera _vcam;

    private void Update()
    {
        if (_backgroundOnView.transform.position.x + _backgroundOnView.bounds.size.x < _vcam.transform.position.x)
        {
            var newBackgroundPos = _backgroundOnView.transform.position;
            newBackgroundPos.x = _auxBackground.transform.position.x + _auxBackground.bounds.size.x;
            _backgroundOnView.transform.position = newBackgroundPos;
            (_auxBackground, _backgroundOnView) = (_backgroundOnView, _auxBackground);
        }
    }
}
