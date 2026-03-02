using UnityEngine;

public class MooButton : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip rareClip;         
    [SerializeField] private AudioClip[] commonClips;     

   
    [Range(0f, 1f)]
    [SerializeField] private float rareChance = 0.05f;      

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            PlayRandomSound();
        }
    }

    private void PlayRandomSound()
    {
        
        if (Random.value < rareChance && rareClip != null)
        {
            audioSource.PlayOneShot(rareClip);
            return;
        }

        if (commonClips.Length > 0)
        {
            AudioClip clip = commonClips[Random.Range(0, commonClips.Length)];
            audioSource.PlayOneShot(clip);
        }
    }
}
