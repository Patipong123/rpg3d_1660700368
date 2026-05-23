using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource[] bgm;
    public AudioSource[] BGM { get { return bgm; } }

    [SerializeField]
    private AudioSource[] sfx;
    public AudioSource[] SFX { get { return sfx; } }

    [SerializeField]
    private AudioMixer audioMixer;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        foreach (AudioSource s in bgm) s.Stop();
        foreach (AudioSource s in sfx) s.Stop();
        PlayBGM(5);
    }

    private void StopAllBGM() 
    { 
        for (int i = 0; i < bgm.Length; i++)
            bgm[i].Stop();
    }

    public void PlayBGM(int i)
    {
        if (i >= BGM.Length) return;

        StopAllBGM();
        BGM[i].Play();
    }

    public void PlaySFX(int i) 
    {
        if (i < sfx.Length && !sfx[i].isPlaying)
            sfx[i].Play();
    }
}
