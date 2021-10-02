using System;
using UnityEngine;


// Class that defines options for playing back music
[Serializable]
public class MusicPlaybackOptions
{
	[Tooltip("Force rewinding the music playback if the same music asset is played")]
	public bool rewindSameAsset = false;


	public override string ToString()
	{
		return $"{{rewindSameAsset: {rewindSameAsset}}}";
	}
}
