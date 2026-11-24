using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{

    [SerializeField] GameManager gm;

    [Header("Player References")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] BoxCollider2D col;
    [SerializeField] PlayerInput pInput;
    [SerializeField] Animator anim;

    [Header("Jump")]
    [SerializeField] float jumpForce;
    [SerializeField] float rcDistance;
    [SerializeField] LayerMask groundLayer;

    [Header("Duck")]
    [SerializeField] float ogHeight;
    [SerializeField] float duckHeight;

    private void Update()
    {
        HandleDucking();
    }

    void OnJump(InputValue action)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rcDistance, groundLayer);
        Debug.DrawRay(transform.position, Vector2.down * rcDistance, Color.red, 2f);

        Vector2 forceDirection = new Vector2(0, jumpForce);

        if(hit.collider.gameObject.name == "Ground")
            rb.AddForce(forceDirection, ForceMode2D.Impulse);
        anim.SetTrigger("Jump");
    }
    void OnShift(InputValue action)
    {
        Debug.Log("OnShift fired");
        if (!action.isPressed) return;
        gm.TogglePlayer();
    }

    void HandleDucking() { 
        Vector2 full = new Vector2(col.size.x, ogHeight);
        Vector2 duck = new Vector2(col.size.x, duckHeight);

        bool duckHeld = pInput.actions["Duck"].IsPressed();

        if (duckHeld)
        {
            col.size = duck;
            col.offset = new Vector2(col.offset.x, duckHeight / 2f);

            anim.SetBool("isCrouching", true);
        }
        else
        {
            col.size = full;
            col.offset = new Vector2(col.offset.x, ogHeight / 2f);

            anim.SetBool("isCrouching", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Death")
            gm.GameOver();
    }

}
