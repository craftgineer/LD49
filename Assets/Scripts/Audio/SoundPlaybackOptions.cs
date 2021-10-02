using System;
using UnityEngine;


// Class that defines options for playing back sounds
[Serializable]
public class SoundPlaybackOptions
{
	[Tooltip("Relative volume for the sound asset"), Range(0.0f, 2.0f)]
	public float relativeVolume = 1.0f;

	[Tooltip("Relative pitch for the sound asset"), Range(0.0f, 2.0f)]
	public float relativePitch = 1.0f;

	[Tooltip("Volume to duck the music to while the sound asset is playing"), Range(0.0f, 1.0f)]
	public float duckVolume = 1.0f;


	public override string ToString()
	{
		return $"{{relativeVolume: {relativeVolume}, relativePitch: {relativePitch}, duckVolume: {duckVolume}}}";
	}
}