using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // para el mapa de acciones

public class AtaqueHeroe : MonoBehaviour
{
	// Referencias
	private Animator heroAnimator;
	// Zona de ataque del jugador
	[SerializeField] private Collider attackCollider;
	
	private Jugador controles;

	public bool isAttacking {get; private set;}
	private float attackTimer = 0f;
	private float attackResetTime = 0.6f;
	
	private void Awake()
	{
		controles = new Jugador();
		heroAnimator = GetComponent<Animator>();
	}
	
    void Start()
    {
		// vinculamos nuestra funcion de ataque al 
		// evento de apretar el boton de ataque
		controles.Player.NormalAttack.started +=
			ctx => NormalAttack();
    }
	
	private void OnEnable()
	{
		controles.Player.Enable();
	}
	
	private void NormalAttack()
	{
		// para atacar, el jugador tiene que 
		// estar no atacando
		if( ! isAttacking) // if(isAttacking == false)
		{
			// ahora sí está atacando
			isAttacking = true;
			
			attackTimer = 0f;
			
			// Animación de ataque
			heroAnimator.ResetTrigger("idle");
			heroAnimator.SetTrigger("normalAttack");
		}
	}
	
	public void ResetAttack()
	{
		isAttacking = false;
		attackTimer = 0f;
		heroAnimator.SetTrigger("idle");
	}
	
    void Update()
    {
        if(isAttacking)
		{
			// acumulamos o contamos tiempo
			attackTimer += Time.deltaTime;
			
			// preguntamos si ya hay que resetear
			// la bandera de ataque
			if(attackTimer > attackResetTime)
			{
				ResetAttack();
			}
		}
    } // llave del Update()
	
	// funciones para que desde los clips de animación
	// habilitemos y deshabilitemos la zona de golpe
	public void EnableAttackCollider()
	{
		attackCollider.enabled = true;
	}
	public void DisableAttackCollider()
	{
		attackCollider.enabled = false;
	}
	
	// Estas funciones se ocupan en el heroComboAttackBehaviour
	public void SetComboResetTime(float clipLength)
	{
		// cada clip de animación tiene una duración distinta
		attackResetTime = clipLength;
	}
	public void ResetAttackTimer()
	{
		attackTimer = 0f;
	}
	
}
