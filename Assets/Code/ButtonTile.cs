using UnityEngine;

public class ButtonTile : MonoBehaviour
{
    public bool activated = false;

    public Vector3 pressedOffset = new Vector3(0, -0.1f, 0);
    private Vector3 startPosition;

    private Animator animator;

    void Awake()
    {
        startPosition = transform.position;
        animator = GetComponent<Animator>();
    }

    public void Activate()
    {
        if (activated) return;

        activated = true;

        if (animator != null)
            animator.SetBool("activated", true);

        transform.position = startPosition + pressedOffset;
    }

    public void Deactivate()
    {
        activated = false;

        if (animator != null)
            animator.SetBool("activated", false);

        transform.position = startPosition;
    }
}

