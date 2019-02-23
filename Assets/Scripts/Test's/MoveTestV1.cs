using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTestV1 : MonoBehaviour
{
    // Start is called before the first frame update
    private CharacterController _controller;
    private float Speed = 5f;
    private Vector3 _velocity;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _controller.Move(move * Time.deltaTime * Speed); //<--- is the same as --> _body.MovePosition(_body.position + _input * moveSpeed * Time.fixedDeltaTime);

        if (move != Vector3.zero)
            transform.forward = move;

        _velocity.y += Physics.gravity.y * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }
}
