// using UnityEngine;
//
// public class HouseMusicTrigger : MonoBehaviour
// {
//     public AudioSource houseMusic;  // Reference to the AudioSource
//
//     private void OnTriggerEnter(Collider other)
//     {
//         if (other.CompareTag("Player"))  // Check if the player enters the trigger
//         {
//             if (!houseMusic.isPlaying)   // Only play if it’s not already playing
//             {
//                 houseMusic.Play();
//                 Debug.Log("Music started in the house.");
//             }
//         }
//     }
//
//     private void OnTriggerExit(Collider other)
//     {
//         if (other.CompareTag("Player"))  // Check if the player leaves the trigger
//         {
//             houseMusic.Stop();  // Stop the music when the player leaves
//             Debug.Log("Music stopped in the house.");
//         }
//     }
// }

using UnityEngine;

public class HouseMusicTrigger : MonoBehaviour
{
    public AudioSource houseMusic;   // Reference to the AudioSource
    public GameObject player;        // Reference to the player GameObject

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player
        if (other.gameObject == player)
        {
            if (!houseMusic.isPlaying)   // Only play if it’s not already playing
            {
                houseMusic.Play();
                Debug.Log("Music started in the house.");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the object exiting the trigger is the player
        if (other.gameObject == player)
        {
            houseMusic.Stop();  // Stop the music when the player leaves
            Debug.Log("Music stopped in the house.");
        }
    }
}
