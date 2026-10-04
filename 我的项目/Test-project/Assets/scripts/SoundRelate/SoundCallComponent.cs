using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum SoundCallPlayType { OneShot, Cycle};

public class SoundCallComponent : MonoBehaviour
{
    
    public int id;
    public SoundServer server;

    public SoundCallPlayType PlayTypeMode;

    public GameObject Trace;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AudioSource audio = GetAudioSource();
        if (audio.isPlaying)
        {
            if (Trace != null)
            {
                gameObject.transform.position = Trace.transform.position;
            }
        }
        else
            {
                if (!audio.loop)
                {
                   Release();
                }
            }
    }

    public void Apply(SoundCallPlayType PlayModeType = SoundCallPlayType.OneShot)
    {
        PlayTypeMode = PlayModeType;
        AudioSource audio = GetAudioSource();
        if (audio != null)
        {
            server.idle_sound_calls_pool[id] = null;
            switch (PlayTypeMode)
            {
                case SoundCallPlayType.OneShot:
                    audio.loop = false;
                    break;
                case SoundCallPlayType.Cycle:
                    audio.loop = true;
                    break;
            }
        }
        else
        {
            Debug.LogError("加载失败：未找到AudioSource");
        }
        
    }

    public void ClipInit(string sound_file_path, float volume = 1)
    {
        AudioSource audio = GetAudioSource();
        if (audio != null)
        {
            AudioClip clip = Resources.Load<AudioClip>(sound_file_path);
            if (clip != null)
            {
                audio.clip = clip;
                audio.volume = volume;
            }
            else
            {
                Debug.LogError("加载失败：sound_file_path无效");
            }
        }
        else
        {
            Debug.LogError("加载失败：未找到AudioSource");
        }
    }

    public void Release()
    {
        Trace = null;
        AudioSource audio = GetAudioSource();
        //audio.clip = null;
        gameObject.SetActive(false);
        server.idle_sound_calls_pool[id] = this;
    }

    AudioSource GetAudioSource()
    {
        AudioSource audio = gameObject.GetComponent<AudioSource>();
        if (audio != null)
        {
            return audio;
        }
        else
        {
            Debug.LogError("加载失败：未找到AudioSource");
            return null;
        }
    }

    public void StartPlay()
    {
        AudioSource audio = GetAudioSource();
        gameObject.SetActive (true);
        audio.Play();
    }

    public void StopPlay()
    {
        AudioSource audio = GetAudioSource();
        gameObject.SetActive(false);
        audio.Stop();
    }
}
