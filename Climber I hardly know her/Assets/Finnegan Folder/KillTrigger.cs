using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{

    [SerializeField] private BoxCollider2D collider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<Player>().TakeDamage(collision.gameObject.GetComponent<Player>().MaxHealth);
    }
}
