using UnityEngine;

public class ParentTrigger : MonoBehaviour
{
    [SerializeField] private GameObject Parent;

    
    void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.transform.parent = Parent.transform;
        Debug.Log("x");
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        other.gameObject.transform.parent = null;

    }
}
