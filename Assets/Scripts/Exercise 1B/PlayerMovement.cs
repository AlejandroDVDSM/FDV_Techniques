using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;

    private float _horizontalMovement;
    private Vector3 _direction;
    
    // Update is called once per frame
    void Update()
    {
        // Move player to the right
        _direction = Vector3.right.normalized;
        transform.Translate(_direction * (_moveSpeed * Time.deltaTime));
    }
}
