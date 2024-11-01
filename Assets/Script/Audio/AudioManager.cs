using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CharacterAudioSoucre
{
    public int id;
    public AudioClip jumpSound;
    public AudioClip hurtSound;
    public AudioClip deadSound;
}

public class AudioManager : MonoBehaviour
{

    [Header("===Background Music===")]
    public AudioClip MenuMusic;
    public List<AudioClip> lst_gameMusic=new List<AudioClip>();
    public AudioClip LoseMusic;

    [Header("===Sound Effect===")]
    public AudioClip countNumber;
    public AudioClip fishBone;
    public AudioClip invincible;
    public AudioClip manget;
    public AudioClip powerUp;
    public AudioClip slide;
    public AudioClip premiumCurrency;
    [SerializeField] private List<CharacterAudioSoucre> lst_characterSound=new List<CharacterAudioSoucre>();


    [Header("======")]
    [SerializeField] private AudioSource musicAdudio;
    [SerializeField] private AudioSource soundEffect;

    public static AudioManager instance;

    // Start is called before the first frame update
    void Start()
    {
        SetMusicAudio(MenuMusic);
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetMusicAudio(AudioClip audioClip)
    {
        if (audioClip != null)
        {
            musicAdudio.clip=audioClip;
            musicAdudio.Play();
        }
    }

    public void PlaySoundEffect(AudioClip audioClip)
    {
        if (audioClip != null)
        {
            soundEffect.PlayOneShot(audioClip);
        }
    }

    public CharacterAudioSoucre GetCharacterSound(int id)
    {
        return lst_characterSound.Find(x=>x.id==id);
    }
}
