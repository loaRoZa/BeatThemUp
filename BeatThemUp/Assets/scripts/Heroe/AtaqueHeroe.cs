using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueHeroe : MonoBehaviour
{
    // referencias
    private Animator HeroAnimator;
    [SerializeField] private Collider attackCollider;

    private Jugador controles;

    public bool isAttacking { get; private set; }
    private float attackTimer = 0f;
    private float attackResetTime = 0.6f;

    private void Awake()
    {
        controles = new Jugador();
        HeroAnimator = GetComponent<Animator>();
    }
    void Start()
    {
        controles.Player.NormalAttack.started +=
            ctx => NormalAttack();
    }


    private void OnEnable()
    {
        controles.Player.Enable();
    }

    private void NormalAttack()
    {
        if( ! isAttacking) // if (isAttacking == false)
        {
            //ahora si esta atacando
            isAttacking = true;

            attackTimer = 0f;

            //Animacion de ataque
            HeroAnimator.ResetTrigger("Enter");
            HeroAnimator.SetTrigger("NormalAttack");
;        }
    }

    public void ResetAttack()
    {
        isAttacking = false;
        attackTimer = 0f;
        HeroAnimator.SetTrigger("idle");
    }

    private void Update()
    {
        if(isAttacking)
        {
            //acumulamos o contamos tiempo
            attackTimer += Time.deltaTime;

            //preguntamos si ya hay que resetear
            //la bandera de ataque
            if(attackTimer > attackResetTime)
            {
                ResetAttack();
            }
        }
    }

    //funciones para que desde los clips de animacion 
    //habilitemos y deshabilitemos la zona de golpe
    public void EnableAttackCollider()
    {
        attackCollider.enabled = true;
    }
    public void DisableAttackCollider()
    {
        attackCollider.enabled = false;
    }

    //estas funciones se ocupan en el heroComboAttackBehaviour
    public void SetComboResetTime(float clipLenght)
    {
        //cada clip de animacion tiene una duracion distinta
        attackResetTime = clipLenght;
    }
    public void ResetAttackTimer()
    {
        attackTimer = 0f;
    }
}
