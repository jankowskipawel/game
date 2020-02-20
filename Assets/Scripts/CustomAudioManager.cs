using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(AudioSource))]
public class CustomAudioManager : MonoBehaviour
{
    public SoundEffect[] soundEffects;
    private Dictionary<string, SoundEffect> soundLibrary;
    private AudioSource audioSource;

    [System.Serializable]
    public struct SoundEffect
    {
        public string name;
        public AudioClip audioClip;
        [Range(0f, 1f)]
        public float volume;
    }

    void Start()
    {
        soundLibrary = new Dictionary<string, SoundEffect>();
        audioSource = GetComponent<AudioSource>();

        for (int i = 0; i < soundEffects.Length; i++)
        {
            if (soundLibrary.ContainsKey(soundEffects[i].name))
            {
                Debug.LogWarning(string.Format("AudioManager::Start: {0} already has a sound effect named: {1} please rename", gameObject.name, soundEffects[i].name));
            }
            else
            {
                soundLibrary.Add(soundEffects[i].name, soundEffects[i]);
            }
        }
    }

    public void Play(string name)
    {
        if (soundLibrary.ContainsKey(name))
        {
            audioSource.PlayOneShot(soundLibrary[name].audioClip, soundLibrary[name].volume);
        }
        else
        {
            Debug.LogWarning(string.Format("AudioManager::PlaySound: {0} does not have a sound effect named: {1} skipping", gameObject.name, name));
        }
    }
}