using UnityEngine;


// Class that defines a music asset
[CreateAssetMenu(fileName = "Music Asset", menuName = "Audio/Music Asset")]
public class MusicAsset : AudioAsset
{
  // The entries of the music
  [Header("Entries")]
  public AudioTrack introTrack;
  public AudioTrack loopTrack;


  // Play the intro of the music asset at a source
  public void PlayIntroAtSource(AudioSource source)
  {
    introTrack.PlayAtSource(source);
  }

  // Play the loop of the music asset at a source
  public void PlayLoopAtSource(AudioSource source)
  {
    loopTrack.PlayAtSource(source);
  }
}