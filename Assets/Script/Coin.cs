using UnityEngine;

public class Coin : MonoBehaviour
{
    public AudioClip coinSound;  // <-- Drag sound here in Inspector

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Play sound
            AudioSource.PlayClipAtPoint(coinSound, transform.position);

            // Add score
            ScoreManager.instance.AddScore(1);

            Destroy(gameObject);
        }
    }
}
