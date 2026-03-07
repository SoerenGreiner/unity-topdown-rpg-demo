using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MovableObject : MonoBehaviour
{
    public float pushForce = 10f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Apply a force to the movable object in the direction away from the player
            Vector2 pushDirection = transform.position - other.transform.position;
            GetComponent<Rigidbody2D>().AddForce(pushDirection.normalized * pushForce, ForceMode2D.Impulse);
        }
    }
}
