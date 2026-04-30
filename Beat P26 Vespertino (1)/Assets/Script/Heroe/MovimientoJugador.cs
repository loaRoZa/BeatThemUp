using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // "nueva" libreria de entradas

public class MovimientoJugador : MonoBehaviour
{
    // referencias
    private Rigidbody rb;
    private Jugador controles;
	private Animator heroAnimator;
	private AtaqueHeroe ataqueH; // script de ataque
	private HealthSystem heroHealthSystem;
	
	private Transform visualTransform; // el objeto hijo con el sprite

    Vector2 inputMovimiento;
	// cuando sufra daño no se podrá mover
	private bool canMove = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        controles = new Jugador(); // instanciando
		heroAnimator = GetComponentInChildren<Animator>();
		visualTransform = 
			GetComponentInChildren<SpriteRenderer>().transform;
		ataqueH = GetComponentInChildren<AtaqueHeroe>();
		heroHealthSystem = GetComponent<HealthSystem>();
    }

    void OnEnable()
    {
        // habilitamos nuestro mapa de acciones
        controles.Player.Enable();
    }

    void Start()
    {
        // cada vez que el jugador mueva los controles
        // guardamos el resultado en nuestro vector
        controles.Player.Move.performed += 
            ctx => inputMovimiento = ctx.ReadValue<Vector2>();

        controles.Player.Move.canceled +=
            ctx => inputMovimiento = Vector2.zero;
    }

    private Vector3 traslacionHeroe;
    [SerializeField] private float velocidad = 1f;
    private void Movimiento(Vector2 direccion)
    {
        // nuestro heroe se mueve en el plano xz
        traslacionHeroe.x = direccion.x;
        traslacionHeroe.z = direccion.y;
        traslacionHeroe.Normalize(); // vector tamaño 1
        traslacionHeroe *= velocidad; // escalar la vel
        //traslacionHeroe = traslacionHeroe * velocidad;

        // le damos la velocidad al rigidbody
        rb.velocity = traslacionHeroe;
		
		// rotamos el transform del grafico hacia donde camina
		if(traslacionHeroe.x > 0f)
			visualTransform.rotation = 
				Quaternion.Euler(0f, 0f, 0f);
		else if(traslacionHeroe.x < 0f)
			visualTransform.rotation =
				Quaternion.Euler(0f, 180f, 0f);
		
		// activar animación de moverse
		heroAnimator.SetBool("move", true);
    }

    void Update()
    {
		if(canMove &&  ! ataqueH.isAttacking)
			Movimiento(inputMovimiento);
		
		// si no va a una velocidad significativa, no se mueve
		if(rb.velocity.magnitude <= 0.1f)
			heroAnimator.SetBool("move", false);
    }
	
	private void OnTriggerEnter(Collider col)
	{
		// preguntamos si me pega el enemigo
		if(col.CompareTag("EnemyAttackZone"))
		{
			// pierda puntos de salud (HP)
			if(heroHealthSystem)
				heroHealthSystem.Damage(1);
			
			// animación de sufrir daño
			heroAnimator.SetTrigger("receiveHit");
			
			// mientras está recibiendo golpe
			// deshabilitamos los controles
			canMove = false;
			// canMove se tiene que habilitar de nuevo
			// cuando acabe la animación de recibir daño
			// en un stateMachineBehaviour
		}
	}
	
	public void CanMoveAgain()
	{
		canMove = true;
	}
}
