using System.Collections;
using UnityEngine;


// Behaviour class that plays a sound on a trigger
public class SoundEmitter : AudioEmitterAsync
{
  // The sound asset to play
  public SoundAsset soundAsset;

  // The sound playback options
  public SoundPlaybackOptions soundOptions;

  // The audio source to play the sound asset on, leave empty for the pool
  public AudioSource soundSource;


  // The coroutine to execute on a trigger
  protected override IEnumerator TriggerCoroutine()
  {
    // Wait until the audio player is initialized
    yield return new WaitUntil(() => AudioPlayer.Instance != null);

    // Play the sound asset
    if (soundSource != null)
      AudioPlayer.Instance.PlaySoundAt(soundAsset, soundOptions, soundSource);
    else
      AudioPlayer.Instance.PlaySound(soundAsset, soundOptions);
  }
}