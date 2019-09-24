using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;

    public static SoundManager Instance
    {
        get { return instance; }
    }

    public string ResourceDir = "Sounds";

    void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        //audioSource.loop = false;
        //audioSource.playOnAwake = true;
        audioSource.volume = 0.3f;
        Mute = PlayerPrefs.GetInt("Mute", 0) == 0 ? false : true;
    }

    #region BGM
    private AudioSource audioSource;
    public bool Mute
    {
        get { return audioSource.mute; }
        set { audioSource.mute = value; }
    }

    public float BGMVolume //0-1
    {
        get { return audioSource.volume; }
        set { audioSource.volume = value; }
    }

    public void PlayBGM(string name)
    {
        //string path = Application.dataPath + "/Resources/" + ResourceDir + "/" + name;
        string path = ResourceDir + "/" + name;
        //Debug.Log(path);
        AudioClip ac = Resources.Load<AudioClip>(path);
        if (ac == null)
            Debug.Log("Load BGM error!");
        audioSource.clip = ac;
        audioSource.Play();
    }

    public void StopBGM()
    {
        audioSource.clip = null;
        audioSource.Stop();
    }
    #endregion

    //Audio
    //public void PlayAudio(string name, Vector2 pos = new Vector2())
    public void PlayAudio(string name)
    {
        string path = ResourceDir + "/" + name;
        AudioClip ac = Resources.Load<AudioClip>(path);
        AudioSource.PlayClipAtPoint(ac, Vector2.zero);
    }
}
