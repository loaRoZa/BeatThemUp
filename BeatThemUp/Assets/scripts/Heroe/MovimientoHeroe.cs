using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoHeroe : MonoBehaviour
{
    // referencias

    private Rigidbody rb;
    private Jugador controles;
    private Animator heroAnimator;
    private AtaqueHeroe ataqueH; //script de ataque

    private Transform visualTransform;

    Vector2 InputMovimiento;
    //cuando sufra daño no se podrá mover
    private bool canMove = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        controles = new Jugador(); // Instanciado
        heroAnimator = GetComponentInChildren<Animator>();
        visualTransform = GetComponentInChildren<SpriteRenderer>().transform;
        ataqueH = GetComponentInChildren<AtaqueHeroe>();
    }

    private void OnEnable()
    {
        controles.Player.Enable();
    }
    void Start()
    {
        //Cada vez que el jugador mueva los controles
        //Guardamos el resultado en nuestro vector
        controles.Player.Move.performed +=
            ctx => InputMovimiento = ctx.ReadValue<Vector2>();

        controles.Player.Move.canceled +=
            ctx => InputMovimiento = Vector2.zero;
    }

    private Vector3 traslacionHeroe;
    [SerializeField] private float velocidad = 5f;
    private void Movimiento(Vector2 direccion) //Encabezado. 
    {
        //nuestro heroe se mueve en plano xz
        traslacionHeroe.x = direccion.x;
        traslacionHeroe.z = direccion.y;
        traslacionHeroe *= velocidad; //escalar la velocidad

        //Le damos la velocidad al RigidBody
        rb.velocity = traslacionHeroe;

        //rotamos el transform del grafico hacia donde camina
        if (traslacionHeroe.x > 0f)
            visualTransform.rotation =
                Quaternion.Euler(0f, 0f, 0f);
        else if (traslacionHeroe.x < 0f)
            visualTransform.rotation =
                Quaternion.Euler(0f, 180f, 0f);

        //activar animacion de moverse
        heroAnimator.SetBool("move", true);
    }
    // Update is called once per frame
    void Update()
    {
        if(canMove && !ataqueH.isAttacking)
        Movimiento(InputMovimiento);

        //si no va a una velocidad significativa, no se mueve
        if (rb.velocity.magnitude <= 0.1f)
            heroAnimator.SetBool("move", false);
    }
    private void OnTriggerEnter(Collider other)
    {
        //preguntamos si me pega el enemigo
        if (other.CompareTag("EnemyAttackZone"))
        {
            //pierda puntos de salud
            //animación de sufrir daño
            heroAnimator.SetTrigger("receiveHit");
            //mientras esta recibiendo golpe deshabilitamos los controles
            canMove = false;
            //canMove se tiene que habilitar de nuevo cuando acabe la animacion de 
            //recibir daño en un  StateMachineBehaviour
        }
    }
    public void CanMoveAgain ()
    {
        canMove = true;
    }
}
