using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonobehaviourSingleton<AudioManager>
{
	public AudioMixerGroup mixerGroup;
	public Sound[] sounds;
    public String nameOfMainSong;

	public override void Awake()
	{
        base.Awake();
		DontDestroyOnLoad(gameObject);

		foreach (Sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.outputAudioMixerGroup = mixerGroup;
		}
	}

    void Start()
    {
        Play(nameOfMainSong);
    }

    //You send in the inspector a name of a sound or music you added to the array after to play it.
    public void Play(string sound)
	{
        //If the name dosent exist, call the exception to save the error and tell you what happend
		Sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		

		s.source.Play();
	}

}
