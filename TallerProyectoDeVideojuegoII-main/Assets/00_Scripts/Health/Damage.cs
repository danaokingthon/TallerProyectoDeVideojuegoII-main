using UnityEngine;

public class Damage : MonoBehaviour
{
    public int damageAmount = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Health player = other.GetComponent<Health>();

        if (player != null)
        {
            player.TakeDamage(damageAmount);
        }
    }
}
