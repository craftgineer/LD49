using System.Collections;
using UnityEngine;


// Behaviour class that stops music on a trigger
public class MusicStopEmitter : AudioEmitterAsync
{
  // The coroutine to execute on a trigger
  protected override IEnumerator TriggerCoroutine()
  {
    // Wait until the audio player is initialized
    yield return new WaitUntil(() => AudioPlayer.Instance != null);

    // Stop the music playback
    AudioPlayer.Instance.StopMusic();
  }
}