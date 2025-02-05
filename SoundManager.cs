using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum SoundType
{
    ThrowFire, 
    ThrowIce,
    ThrowWater,
    Ice,
    Water,
    Fire,
    MainMusic,
    Jump,
    Fall,
    Rope,
    FootStep
}
[RequireComponent(typeof(AudioSource)), ExecuteInEditMode]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private SoundList[] soundList;
    private static SoundManager instance;
    private AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Awake()
    {
        instance = this;
    }

    public static void PlaySound(SoundType sound,float volume = 1 )/* type of sound, Volume */
    {
        AudioClip[] clips = instance.soundList[(int)sound].Sounds;
        AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];
        instance.audioSource.PlayOneShot(randomClip, volume);
    }

    public static void PlayMusic(float volume = 1)/* type of sound, Volume */
    {
        AudioClip[] clips = instance.soundList[(int)SoundType.MainMusic].Sounds;
        AudioClip randomClip = clips[UnityEngine.Random.Range(0, clips.Length)];
        instance.audioSource.clip = randomClip;
        instance.audioSource.Play();
    }public static void StopMusic()
    {
        if (instance == null) return;
        instance.audioSource.Stop();
    }



#if UNITY_EDITOR
    private void OnEnable()
    {
        string[] names = Enum.GetNames(typeof(SoundType));
        Array.Resize(ref soundList, names.Length);
        for(int i=0; i<soundList.Length; i++)
        {
            soundList[i].name = names[i];
        }
    }
#endif
}
[Serializable]
public struct SoundList
{
    public AudioClip[] Sounds { get => sounds; }
    [HideInInspector]public string name;
    [SerializeField] private AudioClip[] sounds;
}