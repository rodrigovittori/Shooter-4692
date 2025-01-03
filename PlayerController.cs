using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [M3.L2] · Actividad Nº 2 "Controlando al personaje"

public class PlayerController : MonoBehaviour
{

    [SerializeField] float movementSpeed = 7f;
    private float currentSpeed;
    private Rigidbody rb;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = movementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // Movimiento del PJ
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        direction = new Vector3(moveHorizontal, 0.0f, moveVertical);
        direction = transform.TransformDirection(direction);
    }

    private void FixedUpdate()
        { rb.MovePosition(transform.position + direction * currentSpeed * Time.deltaTime ); }
}
