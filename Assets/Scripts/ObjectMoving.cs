using UnityEngine;

public class ObjectMoving : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float objectSpeed;
    void Update()
    {
        rb.MovePosition(transform.position + Vector3.right * objectSpeed * Time.deltaTime);
    }
}
