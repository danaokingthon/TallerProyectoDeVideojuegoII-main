
using UnityEngine;
using UnityEngine.SceneManagement;


public class Health : MonoBehaviour
{
    private Vector3 checkpointPosition;
    public int maxHealth = 100;
    public int currentHealth;

    private Rigidbody2D rb;
    private bool isDead = false;

    [Header("Pantalla de derrota")]
    public GameObject defeatCanvas;

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        checkpointPosition = transform.position;

        //ocultar pantalla de derrota
        if (defeatCanvas != null)
        {
            defeatCanvas.SetActive(false);
        }
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
            Die();
           
        }
        else
        {
            //recibe daño pero sigue vivo, ultimo checkpoint
            transform.position = checkpointPosition;

            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
            }
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

        //pantalla de derrota
        if (defeatCanvas != null)
        {
            defeatCanvas.SetActive(true);
        }
    }

    public void RestartFromCheckpoint()
    {
        transform.position = checkpointPosition;

        currentHealth = maxHealth;
        isDead = false;

        if (rb!= null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.linearVelocity = Vector2.zero;
        }

        //ocultar pantalla de derrota
        if (defeatCanvas != null)
        {
            defeatCanvas.SetActive(false);
        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SetCheckpoint(Vector3 position)
    {
        checkpointPosition = position;
    }
}



