using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    public RagdollController ragdoll;
    public PlayerMovementRunner movement;

    public AudioClip hitSound;     // <-- Add hit sound
    private bool dead = false;

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (dead) return;

        if (hit.collider.CompareTag("Obstacle"))
        {
            dead = true;

            // Play hit sound
            if (hitSound != null)
                AudioSource.PlayClipAtPoint(hitSound, transform.position);

            // Disable movement and enable ragdoll
            movement.enabled = false;
            ragdoll.SetRagdoll(true);

            // Restart after delay
            Invoke("RestartGame", 1.2f);
        }
    }

    void RestartGame()
    {
        // Reset score before the scene reloads
        ScoreManager.instance.ResetScore();

        // Reload the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
