using UnityEngine;

public class ItemHeal : MonoBehaviour
{
    public int healAmount = 1;

    private void OnTriggerEnter2D (Collider2D other)
    {
        Health player = other.GetComponent<Health>();

        if (player != null)
        {
            player.Heal (healAmount);
            Destroy (gameObject);
        }
    }
}
