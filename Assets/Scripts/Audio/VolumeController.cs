using UnityEngine;
using UnityEngine.Audio;


// Class that controls the volume of an audio mixer group
[CreateAssetMenu(fileName = "Volume Controller", menuName = "Audio/Volume Controller")]
public class VolumeController : ScriptableObject
{
  // The audio mixer of the controller
  public AudioMixer audioMixer;

  // The exposed parameter name of the controller
  public string parameterName;


  // The muted state of the controller
  private bool muted = false;

  // The volume of the controller before it was muted
  private float mutedVolume = 0.0f;


  // Property for the mute state of the controller
  public bool Muted {
    get => muted;
    set {
      if (muted == value)
        return;

      muted = value;

      if (muted)
      {
        mutedVolume = AudioMixerVolume;
        AudioMixerVolume = 0.0f;
      }
      else
      {
        AudioMixerVolume = mutedVolume;
        mutedVolume = 0.0f;
      }
    }
  }

  // Property for the volume of the controller
  public float Volume {
    get {
      if (muted)
        return mutedVolume;
      else
        return AudioMixerVolume;
    }
    set {
      if (muted)
        mutedVolume = value;
      else
        AudioMixerVolume = value;
    }
  }

  // Property for the audio mixer volume
  private float AudioMixerVolume {
    get {
      if (audioMixer.GetFloat(parameterName, out var value))
        return Mathf.InverseLerp(-80.0f, 0.0f, value);
      else
        return 0.0f;
    }
    set {
      audioMixer.SetFloat(parameterName, Mathf.Lerp(-80.0f, 0.0f, Mathf.Clamp01(value)));
    }
  }
}