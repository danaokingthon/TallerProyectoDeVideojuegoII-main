using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Health player = other.GetComponent<Health>();

        if (player != null)
        {
            player.SetCheckpoint(transform.position);
        }
    }
}
