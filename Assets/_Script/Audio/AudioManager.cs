using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using static UnityEngine.Rendering.DebugUI;

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
    [SerializeField] private AudioMixer audioMixer;

    public static AudioManager instance;

    // Start is called before the first frame update
    void Start()
    {
        SetDataVolumes();

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

    public void SetDataVolumes()
    {
        Volume volumes = LocalData.instance.GetVolume();

        float dB_master;
        float dB_music;
        float dB_sfx;

        if (volumes.master == 0)
        {
            dB_master = -80;
        }
        else
        {
            dB_master = Mathf.Log10(volumes.master) * 20;
        }

        if (volumes.music == 0)
        {
            dB_music = -80;
        }
        else
        {
            dB_music = Mathf.Log10(volumes.music) * 20;
        }

        if (volumes.sfx == 0)
        {
            dB_sfx = -80;
        }
        else
        {
            dB_sfx = Mathf.Log10(volumes.sfx) * 20;
        }

        audioMixer.SetFloat("MasterVolume", dB_master);
        audioMixer.SetFloat("MusicVolume", dB_music);
        audioMixer.SetFloat("MasterSFXVolume", dB_sfx);
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

    public void SetMasterVolume(float value)
    {
        float dB;

        if (value == 0)
        {
            dB = -80;
        }
        else
        {
            dB = Mathf.Log10(value) * 20;
        }

        audioMixer.SetFloat("MasterVolume", dB);

        Volume volume = LocalData.instance.GetVolume();
        volume.master = value;
        
        LocalData.instance.SetVolume(volume);
    }

    public void SetMusicVolume(float value)
    {
        float dB;

        if (value == 0)
        {
            dB = -80;
        }
        else
        {
            dB = Mathf.Log10(value) * 20;
        }

        audioMixer.SetFloat("MusicVolume", dB);

        Volume volume = LocalData.instance.GetVolume();
        volume.music = value;

        LocalData.instance.SetVolume(volume);

    }

    public void SetSFXVolume(float value)
    {
        float dB;

        if (value == 0)
        {
            dB = -80;
        }
        else
        {
            dB = Mathf.Log10(value) * 20;
        }

        audioMixer.SetFloat("MasterSFXVolume", dB);

        Volume volume = LocalData.instance.GetVolume();
        volume.sfx = value;

        LocalData.instance.SetVolume(volume);

    }
}
