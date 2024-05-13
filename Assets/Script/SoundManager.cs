using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public enum SoundType
{
    footStep,
    attackSound,
    takeDamage,
    healing,
    clickBTN,
    BGM


}

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    private AudioSource audioSource;
    [SerializeField] AudioClip[] soundList;
   
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(SoundType sound, float volume = 0.3f)
    {
        instance.audioSource.PlayOneShot(instance.soundList[(int)sound],volume);
    }
}
