using System.Collections.Generic;
using UnityEngine;

public class BaseAudioManager : MonoBehaviour
{
    public Sound[] sounds;
    private Dictionary<string, Sound> soundsByName;

    void Start()
    {
        soundsByName = new Dictionary<string, Sound>(sounds.Length);
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            soundsByName.Add(s.name, s);
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.spatialBlend = s.dimensionality;
            s.source.playOnAwake = false;
        }
    }

    public void Play(string name)
    {
        if(soundsByName.TryGetValue(name, out Sound targetSound))
        {
            targetSound.source.Play();
        }
        else
        {
            Debug.LogWarning($"Sound {name} not found on {this.gameObject.name}");
        }
    }
}

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0, 1)]
    public float volume;
    public float pitch;
    [Range(0, 1)]
    public float dimensionality;

    [HideInInspector]
    public AudioSource source;
}
