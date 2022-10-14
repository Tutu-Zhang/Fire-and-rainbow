using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    //public bool IfBeginBgmIsPlaying = false;

    private AudioSource bgmSource;//����bgm����Ƶ

    private void Awake()
    {
        Instance = this;
        Debug.Log("���ֲ�����������");
    }

    public void Init()
    {       
       bgmSource = gameObject.AddComponent<AudioSource>();
       Debug.Log("�����������");
    }

    public void PlayBGM(string name,bool isLoop = true)
    {
        //����bgm��������
        AudioClip clip = Resources.Load<AudioClip>("Sound/BGM/" + name);

        bgmSource.clip = clip;

        bgmSource.loop = isLoop;

        bgmSource.Play();
        
    }

    //������Ч
    public void PlayEffect(string name)
    {
        AudioClip clip = Resources.Load<AudioClip>("Sound/Effect/" + name);

        AudioSource.PlayClipAtPoint(clip, transform.position);//���ض�ʱ��㲥��
    }
}
