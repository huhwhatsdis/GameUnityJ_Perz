using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Movement : MonoBehaviour
{
    public float speed = 5f;
    public bool isMoving;
    public bool canMove = true;

    private Vector2 input;
    private Animator animator;

    public LayerMask buttonLayer;
    public LayerMask Interactive;
    public LayerMask Walls;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
public void HandleUpdate()
{
    if (!canMove)
    {
        animator.SetBool("isMoving", false);
        return;
    }

    input.x = Input.GetAxisRaw("Horizontal");
    input.y = Input.GetAxisRaw("Vertical");

    if (!isMoving && input != Vector2.zero)
    {
        animator.SetFloat("move x", input.x);
        animator.SetFloat("move y", input.y);

        Vector3 targetPos = transform.position;
        targetPos.x += input.x;
        targetPos.y += input.y;

        if (IsWalkable(targetPos))
        {
            StartCoroutine(Move(targetPos));
        }
    }

    animator.SetBool("isMoving", isMoving);

    if (Input.GetKeyDown(KeyCode.Z))
    {
        Interact();
    }
}


    void Interact() {
        var facingDir = new Vector3(animator.GetFloat("move x"), animator.GetFloat("move y"));
        var ineractPos = transform.position + facingDir;

        var collider = Physics2D.OverlapCircle(ineractPos, 0.2f, Interactive);
        if (collider != null) {
            collider.GetComponent<Interactable>()?.Interact();
        }

    }

    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPos,
                speed * Time.deltaTime
            );
            yield return null;
        }

        transform.position = targetPos;
        isMoving = false;

        CheckButton();
    }

    void CheckButton()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, 0.2f, buttonLayer);
        if(hit != null)
        {
            ButtonTile button = hit.GetComponent<ButtonTile>();
            if(button != null)
                button.Activate();
        }
    }

    bool IsWalkable(Vector3 worldPosition)
    {
        if (Physics2D.OverlapCircle(worldPosition, 0.2f, Interactive) != null || Physics2D.OverlapCircle(worldPosition, 0.2f, Walls) != null) {
            return false;
        }
        return true;
    }
}
