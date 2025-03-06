using UnityEngine;

public class KillTrigger : MonoBehaviour
{

    [SerializeField] private BoxCollider2D collider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.GetComponent<Player>().TakeDamage(other.gameObject.GetComponent<Player>().MaxHealth);
    }
    
    
}
