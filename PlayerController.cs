using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [M3.L2] · Actividad Nº 7 (HomeWork) "Implementando resistencia"

public class PlayerController : MonoBehaviour
{
    [SerializeField] float movementSpeed = 7f; // Velocidad de movimiento base del jugador
    [SerializeField] float shiftSpeed = 15f; // Velocidad de movimiento del jugador cuando corre
    [SerializeField] float stamina = 5f; // cant. de segundos que puedo correr
    [SerializeField] float staminaCooldown = 0.6f; // cant. de segundos que debo "respirar"/"descansar" tras agotar la Stamina
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
        // Leemos el input de Movimiento del PJ
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // convertimos el Input en un vector para poder aplicarlo a nuestro RigidBody
        direction = new Vector3(moveHorizontal, 0.0f, moveVertical);
        direction = transform.TransformDirection(direction);

        // Animación de movimiento:
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

        // Sprint
        if(Input.GetKey(KeyCode.LeftShift))
        {
            if (stamina >= staminaCooldown)
            {
                stamina -= Time.deltaTime; // Resto el tiempo que pasa corriendo
                currentSpeed = shiftSpeed; // Velocidad = Correr
            }
            else
                // Si yo presiono Shift, pero NO tengo stamina, (o la agoté recientemente) no corro
                { currentSpeed = movementSpeed; } // Velocidad = caminar
        }

        else if (!Input.GetKey(KeyCode.LeftShift))
            {   // Cuando NO pulso shift, recargo stamina
                stamina += Time.deltaTime;
                currentSpeed = movementSpeed;
            } 

        stamina = Mathf.Clamp(stamina, 0f, 5f);

        Debug.Log("STAMINA: " + stamina + "| SPEED: " + currentSpeed);
    }

    private void FixedUpdate()
        { rb.MovePosition(transform.position + direction * currentSpeed * Time.deltaTime ); }

    private void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
        anim.SetBool("Jump", false);
    }
}
