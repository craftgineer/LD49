using System.Collections.Generic;
using UnityEngine;


// Class that defines a sound asset
[CreateAssetMenu(fileName = "Sound Asset", menuName = "Audio/Sound Asset")]
public class SoundAsset : AudioAsset
{
  // The list of audio tracks
  public List<AudioTrack> tracks;

  // Settings for the sound asset
  [Header("Settings")]
  public bool preventSameClips = true;

  // The minimal and maximal volume and pitch ranges
  [Header("Volume and pitch")]
  [Range(0.0f, 2.0f)]
  public float minVolume = 1.0f;
  [Range(0.0f, 2.0f)]
  public float maxVolume = 1.0f;
  [Range(0.0f, 2.0f)]
  public float minPitch = 1.0f;
  [Range(0.0f, 2.0f)]
  public float maxPitch = 1.0f;


  // The last selected audio clip entry
  private AudioTrack lastSelectedEntry = null;


  // Play the sound asset at an audio source
  public void PlayAtSource(AudioSource source, float relativePitch = 1.0f, float relativeVolume = 1.0f)
  {
    // Select an audio clip entry
    var selectedEntry = SelectEntry();
    if (selectedEntry == null)
      return;

    // Set the audio source parameters and play the clip
    var volume = relativePitch * Random.Range(minVolume, maxVolume);
    var pitch = relativeVolume * Random.Range(minPitch, maxPitch);
    selectedEntry.PlayAtSource(source, volume, pitch);
  }

  // Select an audio clip entry to play
  private AudioTrack SelectEntry()
  {
    // If there are no entries to select from, return nothing
    if (tracks.Count == 0)
      return null;

    // If there is one entry to select from, return that one
    else if (tracks.Count == 1)
      return tracks[0];

    // Otherwise select a random clip from the clips to select from
    else
    {
      AudioTrack selectedEntry;
      do
      {
        selectedEntry = tracks[Random.Range(0, tracks.Count)];
      } while (selectedEntry == null || (preventSameClips && selectedEntry == lastSelectedEntry));
      return lastSelectedEntry = selectedEntry;
    }
  }
}