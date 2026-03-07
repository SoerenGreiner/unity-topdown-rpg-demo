using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movmentFilter;
    public SwordAttack swordAttack;

    Vector2 movmentInput;
    public Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Animator animator;
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    private Vector3 moveDir;

    bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + moveDir * moveSpeed * Time.fixedDeltaTime);

        if (canMove)
        {
            if (movmentInput != Vector2.zero)
            {
                bool succsess = TryMove(movmentInput);

                if (!succsess)
                {
                    succsess = TryMove(new Vector2(movmentInput.x, 0));
                }
                if (!succsess)
                {
                    succsess = TryMove(new Vector2(0, movmentInput.y));
                }
                animator.SetBool("IsMoving", succsess);
            }
            else
            {
                animator.SetBool("IsMoving", false);
            }

            if (movmentInput.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (movmentInput.x > 0)
            {
                spriteRenderer.flipX = false;
            }
        }
    }

    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
            int count = rb.Cast(
                direction,
                movmentFilter,
                castCollisions,
                moveSpeed * Time.fixedDeltaTime + collisionOffset);
            if (count == 0)
            {
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    void OnMove(InputValue movmentValue)
    {
        movmentInput = movmentValue.Get<Vector2>();
    }

    void OnFire()
    {
        animator.SetTrigger("SwordAttack");
    }

    public void SwordAttack()
    {
        LockMovement();

        if (spriteRenderer.flipX == true)
        {
            swordAttack.AttackLeft();
        }
        else
        {
            swordAttack.AttackRight();
        }
    }

    public void EndSwordAttack()
    {
        UnlockMovement();
        swordAttack.StopAttack();
    }

    public void LockMovement()
    {
        canMove = false;
    }

    public void UnlockMovement()
    {
        canMove = true;
    }
}
