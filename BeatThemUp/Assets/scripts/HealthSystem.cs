using System.Collections;
using System.Collections.Generic;
using System; // lo agrego para poder usar EventHandler
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int maxHealth; // maximo puntos de salud
    [SerializeField] private int currentHealth;      // el numero actual de puntos de salud

    // Propiedades para la salud
    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;

    // Eventos personalizados para daño y muerte
    public event EventHandler OnDamaged;
    public event EventHandler OnDie;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    // Método para aplicar daño
    public void Damage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth < 0) currentHealth = 0;

        // Invocar el evento de que me lastimaron
        OnDamaged?.Invoke(this, EventArgs.Empty);

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    // Método para manejar la muerte
    private void Die()
    {
        // Invocar el evento de que morí :(
        OnDie?.Invoke(this, EventArgs.Empty);

    }

    // Método para obtener la salud normalizada (0 a 1)
    public float GetHealthNormalized()
    {
        return (float)currentHealth / maxHealth;
    }
}
