using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    //public bool IfBeginBgmIsPlaying = false;

    private AudioSource bgmSource;//播放bgm的音频

    private void Awake()
    {
        Instance = this;
        Debug.Log("音乐播放器已启动");
    }

    public void Init()
    {       
       bgmSource = gameObject.AddComponent<AudioSource>();
       Debug.Log("添加音乐物体");
    }

    public void PlayBGM(string name,bool isLoop = true)
    {
        //加载bgm声音剪辑
        AudioClip clip = Resources.Load<AudioClip>("Sound/BGM/" + name);

        bgmSource.clip = clip;

        bgmSource.loop = isLoop;

        bgmSource.Play();
        
    }

    //播放音效
    public void PlayEffect(string name)
    {
        AudioClip clip = Resources.Load<AudioClip>("Sound/Effect/" + name);

        AudioSource.PlayClipAtPoint(clip, transform.position);//在特定时间点播放
    }
}
