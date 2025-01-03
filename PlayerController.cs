using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [M3.L2] · Actividad Nº 4 "Animando el movimiento!"

public class PlayerController : MonoBehaviour
{

    [SerializeField] float movementSpeed = 7f; // Velocidad de movimiento base del jugador
    [SerializeField] float shiftSpeed = 15f; // Velocidad de movimiento del jugador cuando corre
    private float currentSpeed; // Velocidad de movimiento actual del jugador
    [SerializeField] float jumpForce = 7f; // Altura del salto

    private bool isGrounded = true; // flag que determina si estoy tocando una plataforma (o no)
    private Rigidbody rb;
    private Vector3 direction;

    [SerializeField] Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
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

        if (direction.x != 0 || direction.z != 0 )
            {anim.SetBool("Run", true);}
        
        else
            { {anim.SetBool("Run", false);} }

        // Salto del PJ
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            isGrounded = false;
            anim.SetBool("Jump", true);
        }
    }

    private void FixedUpdate()
        { rb.MovePosition(transform.position + direction * currentSpeed * Time.deltaTime ); }

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
        anim.SetBool("Jump", false);
    }
}
