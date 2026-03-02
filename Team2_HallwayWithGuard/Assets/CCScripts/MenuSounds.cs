using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class MenuSounds : MonoBehaviour
{
    
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip clickSound;


    [SerializeField] private string sceneToLoad;
    [SerializeField] private bool quitGame = false;

    [SerializeField] private float delay = 0.3f;

    public void Execute()
    {
        StartCoroutine(DoAction());
    }

    private IEnumerator DoAction()
    {
        if (audioSource && clickSound)
        {
            audioSource.PlayOneShot(clickSound);
            yield return new WaitForSeconds(clickSound.length);
        }
        else
        {
            yield return new WaitForSeconds(delay);
        }

        if (quitGame)
        {
            QuitGame();
        }
        else
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    private void QuitGame()
    {
        Application.Quit();

     
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
