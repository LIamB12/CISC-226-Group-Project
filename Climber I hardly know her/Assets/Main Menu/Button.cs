using UnityEngine;

public class Button : MonoBehaviour
{

    [SerializeField] private Animator animator;

    
    
    public void Button_MouseHover()
    {
        animator.Play("Button_Hover");
    }
    
    public void Button_MouseUnHover()
    {
        animator.Play("Normal");
    }
    
    public void Button_Click()
    {
        animator.Play("Button_Click");
    }
    
    public void Button_UnClick()
    {
        animator.Play("Button_UnClick");
    }
    
}
