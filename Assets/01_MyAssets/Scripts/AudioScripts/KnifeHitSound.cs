using UnityEngine;

public class KnifeHitSound : MonoBehaviour
{
    public AudioSource audioSource;

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("CuttingBoard"))
        {
            Debug.Log("Knife hit cutting board");
            // Play the sound
            audioSource.Play();
        }

        //if (collision.gameObject.CompareTag("CuttingBoard"))
        //{
        //    Debug.Log("Knife hit cutting board");
        //    // Play the sound
        //    audioSource.Play();
        //}
    }
}