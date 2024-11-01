using UnityEngine;

public class ScrollingBackgroundParallaxB : MonoBehaviour
{
    [SerializeField] private float _scrollSpeedX;
    
    private Renderer _renderer;
    private Material[] _parallaxLayers;
    
    private Vector2 _offset = Vector2.right;

    private static readonly int MainTex = Shader.PropertyToID("_MainTex");
    
    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        _parallaxLayers = _renderer.materials;
    }

    private void Update()
    {
        _offset.x = _scrollSpeedX * Time.deltaTime;
        
        for (int i = 0; i < _parallaxLayers.Length; i++)
            _parallaxLayers[i].SetTextureOffset(MainTex, 
                _parallaxLayers[i].GetTextureOffset(MainTex) + _offset / (i + 1.0f));
    }
}
