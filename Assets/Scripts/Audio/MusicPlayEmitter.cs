using System.Collections;
using UnityEngine;


// Behaviour class that plays music on a trigger
public class MusicPlayEmitter : AudioEmitterAsync
{
  // The music asset to play
  public MusicAsset musicAsset;

  // The music playback options
  public MusicPlaybackOptions musicOptions;


  // The coroutine to execute on a trigger
  protected override IEnumerator TriggerCoroutine()
  {
    // Wait until the audio player is initialized
    yield return new WaitUntil(() => AudioPlayer.Instance != null);

    // Play the music asset
    AudioPlayer.Instance.PlayMusic(musicAsset, musicOptions);
  }
}