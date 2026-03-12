using UnityEngine;

public class MenuMusicController : MonoBehaviour
{
    public AudioSource introSource;
    public AudioSource loopSource;

    public AudioClip introClip;
    public AudioClip loopClip;

    public float volume = 0.5f;

    void Start()
    {
        introSource.clip = introClip;
        loopSource.clip = loopClip;

        introSource.volume = volume;
        loopSource.volume = volume;

        double startTime = AudioSettings.dspTime + 0.1;

        introSource.PlayScheduled(startTime);

        double loopStartTime = startTime + introClip.length;
        loopSource.loop = true;
        loopSource.PlayScheduled(loopStartTime);
    }
}