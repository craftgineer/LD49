using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


// Class that defines an audio track
[Serializable]
public class AudioTrack
{
  // The actual audio clip
  public AudioClip clip;

  // The mixer group of the entry
  public AudioMixerGroup mixerGroup;

  // The relative volume of the entry
  [Range(0.0f, 2.0f)]
  public float relativeVolume = 1.0f;

  // The relative pitch of the entry
  [Range(0.0f, 2.0f)]
  public float relativePitch = 1.0f;

  // The loop state of the entry
  public bool loop = false;


  // Play the entry at an audio source
  public void PlayAtSource(AudioSource source, float relativeVolume = 1.0f, float relativePitch = 1.0f)
  {
    if (clip == null)
      return;

    source.clip = clip;
    source.outputAudioMixerGroup = mixerGroup;
    source.volume = relativeVolume * this.relativeVolume;
    source.pitch = relativePitch * this.relativePitch;
    source.loop = loop;
    source.Play();
  }

  // Return if the entry is equal to enother object
  public override bool Equals(object obj)
  {
    return obj is AudioTrack track &&
      EqualityComparer<AudioClip>.Default.Equals(clip, track.clip) &&
      EqualityComparer<AudioMixerGroup>.Default.Equals(mixerGroup, track.mixerGroup) &&
      relativeVolume == track.relativeVolume &&
      relativePitch == track.relativePitch &&
      loop == track.loop;
  }

  // Return the hash code of the entry
  public override int GetHashCode()
  {
    int hashCode = -1383846161;
    hashCode = hashCode * -1521134295 + EqualityComparer<AudioClip>.Default.GetHashCode(clip);
    hashCode = hashCode * -1521134295 + EqualityComparer<AudioMixerGroup>.Default.GetHashCode(mixerGroup);
    hashCode = hashCode * -1521134295 + relativeVolume.GetHashCode();
    hashCode = hashCode * -1521134295 + relativePitch.GetHashCode();
    hashCode = hashCode * -1521134295 + loop.GetHashCode();
    return hashCode;
  }
}