using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    private Vector3 checkpointPosition;
    public int maxHealth = 100;
    public int currentHealth;

    private Rigidbody2D rb;
    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        checkpointPosition = transform.position;
    }

    public void TakeDamage(int amount)
    {
        if (isDead)
        {
            return;
        }
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            //muerte
            Die();
            //animacion muerte
            //pantalla muerte
        }
    }

    public void Heal(int amount)
    {
        if (isDead)
        {
            return;
        }

        currentHealth += amount;
        //no superar vida máxima
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    private void Die()
    {
        isDead = true;

        //detener movimiento
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }

        //esperar un segundo
        Invoke(nameof(ResetLevel),0.1f);
    }
    private void ResetLevel()
    {
        transform.position = checkpointPosition;
        currentHealth = maxHealth;
        isDead = false;
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SetCheckpoint(Vector3 position)
    {
        checkpointPosition = position;
    }

}
