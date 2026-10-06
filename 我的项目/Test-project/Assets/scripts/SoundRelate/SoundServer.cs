using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BGMType { NULL, TEST, START_SCOPE }

public class SoundServer : MonoBehaviour
{
    public int idle_sound_calls_pool_size = 16;
    public SoundCallComponent[] idle_sound_calls_pool;

    private Dictionary<BGMType, string> BGMTypeToPathMap = new Dictionary<BGMType, string>();

    public BGMType BGMState = BGMType.NULL;

    private BGMType BGMStateChaser = BGMType.NULL;

    private SoundCallComponent BGMPlayer = null;

    GameObject spikemap;

    // Start is called before the first frame update
    void Start()
    {
        spikemap = GameObject.Find("Spikemap");

        idle_sound_calls_pool = new SoundCallComponent[idle_sound_calls_pool_size];
        for (int i = 0;i < idle_sound_calls_pool.Length;i++)
        {
            string new_name = "AudioSourceObj_" + i;
            SoundCallComponent soundCallComponent = CreateGameObjectWithAudioSourceAndSoundCallComponent(new_name);
            soundCallComponent.id = i;
            soundCallComponent.server = this;
            idle_sound_calls_pool[i] = soundCallComponent;
        }
        BGMStateChaser = BGMType.NULL;
        BGMTypeToPathMap.Add(BGMType.TEST, "Musics/Test");
    }

    public void BGMStateChase()
    {
        if (BGMState != BGMStateChaser)
        {
            BGMPlayerRelease();
            GameObject player = GameObject.Find("Player");
            if (player == null)
            {
                player = this.gameObject;
            }
            
            BGMPlayer = StartSoundCallLoopTraceGo(player, BGMTypeToPathMap[BGMState]);
            BGMStateChaser = BGMState;
        }
    }

    public void BGMPlayerRelease()
    {
        if (BGMPlayer != null)
        {
            BGMPlayer.StopPlay();
            BGMPlayer = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (BGMState != BGMType.NULL)
        {
            if (BGMTypeToPathMap[BGMState] == "")
            {
                BGMPlayerRelease();
            }
            else
            {
                BGMStateChase();
            }
        }
        else
        {
            BGMPlayerRelease();
            BGMStateChaser = BGMState;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            
            spikemap.SetActive(!spikemap.activeSelf);
        }
    }

    public int GetIdleSoundCallCount()
    {
        int count = 0;
        for (int i = 0;i < idle_sound_calls_pool.Length; i++)
        {
            if (idle_sound_calls_pool[i] != null)
            {
                count += 1;
            }
        }
        return count;
    }

    SoundCallComponent CreateGameObjectWithAudioSourceAndSoundCallComponent(string new_name = "")
    {
        GameObject audioGo;
        if (new_name.Length > 0){
            audioGo = new GameObject(new_name);
        }
        else
        {
            audioGo = new GameObject();
        }
        audioGo.transform.parent = transform;
        AudioSource audioSource = audioGo.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        SoundCallComponent soundCallComponent = audioGo.AddComponent<SoundCallComponent>();
        return soundCallComponent;
    }

    public SoundCallComponent ApplySoundCall(SoundCallPlayType PlayModeType = SoundCallPlayType.OneShot)
    {
        if (GetIdleSoundCallCount() > 0)
        {
            SoundCallComponent soundCallComponent;
            for (int i = 0; i < idle_sound_calls_pool.Length; i++)
            {
                if (idle_sound_calls_pool[i] != null)
                {
                    soundCallComponent = idle_sound_calls_pool[i];
                    soundCallComponent.Apply(PlayModeType);
                    return soundCallComponent;
                }
            }
            return null;
        }
        else
        {
            return null;
        }
    }

    public SoundCallComponent ApplySoundCallOneShot(Vector3 pos, string clip_resourse_path, float volume = 1)
    {
        SoundCallComponent audioCallComponent = ApplySoundCall();
        if (audioCallComponent == null)
        {
            Debug.LogError("播放失败：无有效的管理组件");
            return audioCallComponent;
        }
        audioCallComponent.transform.position = pos;
        audioCallComponent.ClipInit(clip_resourse_path, volume);
        audioCallComponent.StartPlay();
        return audioCallComponent;
    }

    public SoundCallComponent ApplySoundCallOneShotTraceGo(GameObject Go, string clip_resourse_path, float volume = 1)
    {
        SoundCallComponent audioCallComponent = ApplySoundCall();
        if (audioCallComponent == null)
        {
            Debug.LogError("播放失败：无有效的管理组件");
            return audioCallComponent;
        }
        audioCallComponent.transform.position = Go.transform.position;
        audioCallComponent.Trace = Go;
        audioCallComponent.ClipInit(clip_resourse_path, volume);
        audioCallComponent.StartPlay();
        return audioCallComponent;
    }

    public SoundCallComponent StartSoundCallLoop(Vector3 pos, string clip_resourse_path, float volume = 1)
    {
        SoundCallComponent audioCallComponent = ApplySoundCall(SoundCallPlayType.Cycle);
        if (audioCallComponent == null)
        {
            Debug.LogError("播放失败：无有效的管理组件");
            return audioCallComponent;
        }
        audioCallComponent.transform.position = pos;
        audioCallComponent.ClipInit(clip_resourse_path, volume);
        audioCallComponent.StartPlay();
        return audioCallComponent;
    }

    public SoundCallComponent StartSoundCallLoopTraceGo(GameObject Go, string clip_resourse_path, float volume = 1)
    {
        SoundCallComponent audioCallComponent = ApplySoundCall(SoundCallPlayType.Cycle);
        if (audioCallComponent == null)
        {
            Debug.LogError("播放失败：无有效的管理组件");
            return audioCallComponent;
        }
        audioCallComponent.transform.position = Go.transform.position;
        audioCallComponent.Trace = Go;
        audioCallComponent.ClipInit(clip_resourse_path, volume);
        audioCallComponent.StartPlay();
        return audioCallComponent;
    }
}
