using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float _acceleration;
    [SerializeField] float _maxVelocity;
    [SerializeField] float _rotationSpeed;

    Rigidbody2D _body;

    bool _accelerating;

    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _accelerating = Input.GetKey(KeyCode.UpArrow);
        HandleRotation();
    }

    void FixedUpdate()
    {
        if (_accelerating)
        {
            _body.AddForce(_acceleration * transform.up);
            _body.velocity = Vector2.ClampMagnitude(_body.velocity, _maxVelocity);
        }    
    }

    void HandleRotation()
    {
        var rotationMultiplier = Input.GetKey(KeyCode.LeftArrow) ? 1f :
                                    Input.GetKey(KeyCode.RightArrow) ? -1f : 0f;

        transform.Rotate(_rotationSpeed * Time.deltaTime * transform.forward * rotationMultiplier);
    }

}
