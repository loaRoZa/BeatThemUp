using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	// máquina de estados
	protected FSM enemyFSM;
	
	// Referencias
	protected Rigidbody enemyRigidbody;
	protected Animator enemyAnimator;
	protected Transform player;
	public Transform Player => player; // <- propiedad
	protected Transform enemyVisualTransform;
	protected HealthSystem enemyHealthSystem;
	protected Collider enemyCollider; // hitbox con el que recibe impactos
	
	// variable de velocidad
	[SerializeField] private float moveSpeed;
	
	// Cosas para pelear
	[SerializeField] private float distanceToAttack;
	// Propiedad
	public float DistanceToAttack => distanceToAttack;
	
	// Saber si el enemigo fue golpeado
	public bool wasHitted {get; private set;}
	
	// Saber si el enemigo ya murió
	public bool isDead {get; private set;}
	
	// Prefab de efecto de recibir golpe
	[SerializeField] private GameObject prefabHitEffect;
	
	void Awake()
	{
		enemyRigidbody = GetComponent<Rigidbody>();
		enemyAnimator = GetComponentInChildren<Animator>();
		// transform.Find busca un objeto hijo con el nombre
		enemyVisualTransform = transform.Find("Grafico");
		enemyHealthSystem = GetComponent<HealthSystem>();
		enemyCollider = GetComponent<Collider>();
	}
	
    protected virtual void Start()
    {
        // crear mi máquina de estados
		enemyFSM = new FSM(this);

		player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected virtual void Update()
    {
        // ejecutar el estado actual de la fsm
		enemyFSM.Update();
    }
	
	public void MoveToPlayer()
	{
		// encontrar al jugador
		player = 
			GameObject.FindGameObjectWithTag("Player").transform;
			
		// Creamos un vector que apunte al jugador
		Vector3 direccion = player.position - transform.position;
		direccion.Normalize();
		direccion *= moveSpeed; // direccion = direccion * moveSpeed;
		
		// le pasamos el vector al rigidbody
		enemyRigidbody.velocity = direccion;
		
		// Giramos el visual hacia donde camina el enemigo
		if(direccion.x > 0f)
		{
			// derecha
			enemyVisualTransform.rotation = 
				Quaternion.Euler(0f, 0f, 0f);
		}
		else if (direccion.x < 0f)
		{
			// izquierda
			enemyVisualTransform.rotation = 
				Quaternion.Euler(0f, 180f, 0f);
		}
	}
	
	private float distanceToPlayer;
	public float DistanceToPlayer()
	{
		distanceToPlayer = 
			Vector3.Distance(
				player.position, transform.position);
				
		return distanceToPlayer;
	}
	
	public void OnTriggerEnter(Collider col)
	{
		// preguntar si me pega el ataque del jugador
		if(col.CompareTag("PlayerAttackZone"))
		{
			// deshabilitar su colisionador para que no 
			// le sigan pegando seguido
			
			// bajar puntos de salud
			enemyHealthSystem.Damage(1);
			
			// sfx recibir golpe
			
			// vfx recibir golpe
			Instantiate(prefabHitEffect, transform.position, Quaternion.identity);
			
			// si no ha muerto, recibe el golpe
			if(enemyHealthSystem.CurrentHealth <= 0)
				isDead = true;
			else
				wasHitted = true;
		}
	}
	
	public void ResetHitted()
	{
		wasHitted = false;
	}
	
	public void Die()
	{
		Destroy(gameObject);
	}

	public void DisableCollider()
	{
		enemyCollider.enabled = false;
	}

	public void EnableCollider()
	{
		enemyCollider.enabled = true;
	}

	public void MoveTowards(Vector3 target)
	{
		Vector3 direccion = target - transform.position;
		direccion.Normalize();
		direccion *= moveSpeed;
		enemyRigidbody.velocity = direccion;

		// Giramos el visual hacia donde camina el enemigo
		if(direccion.x > 0f)
		{
			// derecha
			enemyVisualTransform.rotation = Quaternion.Euler(0f, 180f, 0f);
		}
		else if (direccion.x < 0f)
		{
			// izquierda
			enemyVisualTransform.rotation = Quaternion.Euler(0f, 0f, 0f);
		}
	}

}
