using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


// Behaviour that manages a pool of audio sources
public class AudioSourcePool : MonoBehaviour, IEnumerable<AudioSource>
{
  // The prefab to instantiate when the pool is created
  public GameObject audioSourcePrefab;

  // The size of the pool
  public int size = 4;


  // The list of audio sources
  private readonly List<AudioSource> sources = new List<AudioSource>();

  // The dictionary of timestamps when an audio source was last activated;
  private readonly Dictionary<int, float> timestamps = new Dictionary<int, float>();


  // Start is called before the first frame update
  protected void Start()
  {
    // Create the audio sources
    for (int i = 0; i < size; i++)
    {
      // Instantiate the audio source game object
      var sourceObject = Instantiate(audioSourcePrefab, transform);
      sourceObject.name = $"{name}[{i}]";

      // Instantiate the audio source
      var source = sourceObject.GetComponent<AudioSource>();

      // Add the audio source to the pool
      sources.Add(source);
    }
  }

  // Get an available audio source
  public AudioSource Next()
  {
    // Iterate over the audio sources
    for (int i = 0; i < size; i++)
    {
      // Check if the current source is playing, otherwise return it
      if (!sources[i].isPlaying)
      {
        timestamps[i] = Time.unscaledTime;
        return sources[i];
      }
    }

    // No sources are available, so select the oldest playing source
    var oldestIndex = timestamps.OrderBy(e => e.Value).FirstOrDefault().Key;
    timestamps[oldestIndex] = Time.unscaledTime;
    return sources[oldestIndex];
  }

  // Stop all audio sources
  public void StopAll(float fadeOutTime = 0.0f)
  {
    // Iterate over the audio sources
    for (int i = 0; i < size; i++)
    {
      if (fadeOutTime > 0.0f)
        StartCoroutine(StopAllCoroutine(sources[i], fadeOutTime));
      else
        sources[i].Stop();
    }
  }

  // Coroutine that fades out and stops an audio source
  private IEnumerator StopAllCoroutine(AudioSource source, float fadeOutTime)
  {
    var startVolume = source.volume;

    for (float t = 0; t < 1.0f; t += Time.deltaTime / fadeOutTime)
    {
      source.volume = Mathf.Lerp(startVolume, 0.0f, t);
      yield return null;
    }

    source.volume = 0.0f;
    source.Stop();
  }

  // Return a generic enumerator over the audio sources
  public IEnumerator<AudioSource> GetEnumerator()
  {
    return sources.GetEnumerator();
  }

  // Return an enumerator over the audio sources
  IEnumerator IEnumerable.GetEnumerator()
  {
    return GetEnumerator();
  }
}