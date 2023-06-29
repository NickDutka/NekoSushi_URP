using UnityEngine;

public class Blinking : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Check for input or a timer to trigger the blink animation
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.Play("Blink", -1, 0f);
        }
    }
}