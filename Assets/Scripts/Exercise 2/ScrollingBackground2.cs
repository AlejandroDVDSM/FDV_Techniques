using UnityEngine;

public class ScrollingBackground2 : MonoBehaviour
{
    [SerializeField] private float _scrollSpeedX = 0.5f;
    
    private Renderer _renderer; 
    private Vector2 _offset = Vector2.right;
    
    private static readonly int MainTex = Shader.PropertyToID("_MainTex");

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        _offset.x += _scrollSpeedX * Time.deltaTime;
        
        _renderer.material.SetTextureOffset(MainTex, _offset);
    }
}