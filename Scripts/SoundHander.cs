using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundHander : MonoBehaviour
{
    [field: SerializeField] public AudioSource PlayerSounds {get; private set;}
    [field: SerializeField] public AudioSource MagicSounds {get; private set;}
    [field: SerializeField] public AudioSource EnviromentSounds {get; private set;}
    [field: SerializeField] public List<AudioClip> jumpSounds {get; private set;}
    [field: SerializeField] public List<AudioClip> landingSounds {get; private set;}
    [field: SerializeField] public List<AudioClip> dashSounds {get; private set;}
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayJumpSound()
    {
        int sound = Random.Range(0, jumpSounds.Count);
        PlayerSounds.clip = jumpSounds[sound];
        PlayerSounds.Play();
    }
    public void PlayLandSound()
    {
        int sound = Random.Range(0, landingSounds.Count);
        EnviromentSounds.clip = landingSounds[sound];
        EnviromentSounds.Play();
    }
    public void PlayDashSound()
    {
        int sound = Random.Range(0, dashSounds.Count);
        MagicSounds.clip = dashSounds[sound];
        MagicSounds.Play();
    }
}
