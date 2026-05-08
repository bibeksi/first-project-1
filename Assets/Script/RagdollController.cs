
using UnityEngine;


public class RagdollController : MonoBehaviour
{
    public Animator animator;
    private Rigidbody[] ragdollBodies;

    void Awake()
    {
        ragdollBodies = GetComponentsInChildren<Rigidbody>();
        SetRagdoll(false);
    }

    public void SetRagdoll(bool active)
    {
        foreach (var rb in ragdollBodies)
        {
            if (rb.gameObject == this.gameObject)
                continue;

            rb.isKinematic = !active;
        }

        if (animator != null)
            animator.enabled = !active;
    }
}

