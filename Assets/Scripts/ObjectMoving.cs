using UnityEngine;

public class ObjectMoving : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    GameManager gm;
    [SerializeField] float objectSpeed;

    private void Start()
    {
        gm = FindAnyObjectByType<GameManager>();
    }

    void Update()
    {
        rb.MovePosition(transform.position + Vector3.right * objectSpeed * gm.objSpeedMult * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "EndScene")
            Destroy(this.gameObject);
    }
}
