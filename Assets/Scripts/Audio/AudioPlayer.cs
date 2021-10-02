using System;
using System.Collections;
using UnityEngine;


// Class that plays back audio
public class AudioPlayer : MonoBehaviour
{
	// The music and sound asset sets of the player
	[Header("Assets")]
	public MusicAssetSet musicAssets;
	public SoundAssetSet soundAssets;

	// The audio source and pool prefab of the player
	[Header("Prefabs")]
	public GameObject audioSourcePrefab;
	public GameObject audioSourcePoolPrefab;
	public int soundSourcePoolSize = 5;

	// The volume controllers of the player
	[Header("Volume Controllers")]
	public VolumeController masterVolumeController;
	public VolumeController musicVolumeController;
	public VolumeController soundEffectsVolumeController;

	// The singleton instance of the audio player
	public static AudioPlayer Instance;



	// Reference to the audio source for music
	private AudioSource musicSource = null;

	// Reference to the audio source pool for sound effects
	private AudioSourcePool soundSourcePool = null;

	// Reference to the current music asset
	private MusicAsset currentMusicAsset = null;

	// Reference to the current music coroutine
	private Coroutine currentMusicCoroutine = null;


	// Properties for the volume controllers
	public float MasterVolume { get => masterVolumeController.Volume;	set => masterVolumeController.Volume = value;	}
	public bool MasterMuted {	get => masterVolumeController.Muted; set => masterVolumeController.Muted = value; }
	public float MusicVolume {	get => musicVolumeController.Volume;	set => musicVolumeController.Volume = value;	}
	public bool MusicMuted {	get => musicVolumeController.Muted;	set => musicVolumeController.Muted = value;	}
	public float SoundEffectsVolume {	get => soundEffectsVolumeController.Volume;	set => soundEffectsVolumeController.Volume = value;	}
	public bool SoundEffectsMuted {	get => soundEffectsVolumeController.Muted;	set => soundEffectsVolumeController.Muted = value;	}


	// Awake is called when the script instance is being loaded
	protected void Awake()
	{
		// Set the singleton instance
		Instance = this;

		// Load the preferences
		LoadPreferences();

		// Create the audio source for music
		var musicSourceObject = Instantiate(audioSourcePrefab, transform);
		musicSourceObject.name = "Music";

		musicSource = musicSourceObject.GetComponent<AudioSource>();

		// Create the audio source pool for sound effects
		var soundSourcePoolObject = Instantiate(audioSourcePoolPrefab, transform);
		soundSourcePoolObject.name = "Sounds";

		soundSourcePool = soundSourcePoolObject.GetComponent<AudioSourcePool>();
		soundSourcePool.size = soundSourcePoolSize;
	}

  // OnDestroy is called when the script instance is being destroyed
  protected void OnDisable()
  {
		// Destroy the audio source for music
		Destroy(musicSource.gameObject);

		musicSource = null;

		// Destroy the audio source pool for sound effects
		Destroy(soundSourcePool.gameObject);

		soundSourcePool = null;
	}

	// Load the preferences
	public void LoadPreferences()
  {
		MasterVolume = PlayerPrefs.GetFloat("AudioPlayer.MasterVolume", 1.0f);
		MasterMuted = PlayerPrefs.GetInt("AudioPlayer.MasterMuted", 0) > 0;
		MusicVolume = PlayerPrefs.GetFloat("AudioPlayer.MusicVolume", 1.0f);
		MusicMuted = PlayerPrefs.GetInt("AudioPlayer.MusicMuted", 0) > 0;
		SoundEffectsVolume = PlayerPrefs.GetFloat("AudioPlayer.SoundEffectsVolume", 1.0f);
		SoundEffectsMuted = PlayerPrefs.GetInt("AudioPlayer.SoundEffectsMuted", 0) > 0;
	}

	// Save the preferences
	public void SavePreferences()
	{
		PlayerPrefs.SetFloat("AudioPlayer.MasterVolume", MasterVolume);
		PlayerPrefs.SetInt("AudioPlayer.MasterMuted", MasterMuted ? 1 : 0);
		PlayerPrefs.SetFloat("AudioPlayer.MusicVolume", MusicVolume);
		PlayerPrefs.SetInt("AudioPlayer.MusicMuted", MusicMuted ? 1 : 0);
		PlayerPrefs.SetFloat("AudioPlayer.SoundEffectsVolume", SoundEffectsVolume);
		PlayerPrefs.SetInt("AudioPlayer.SoundEffectsMuted", SoundEffectsMuted ? 1 : 0);
	}

	// Play a music asset
	public void PlayMusic(MusicAsset musicAsset, MusicPlaybackOptions options)
	{
		// Set the default options if none given
		options ??= new MusicPlaybackOptions();

		// Check if there is a music asset to play
		if (musicAsset == null)
		{
			Debug.LogWarning($"[AudioPlayer] Music asset is null");
			return;
		}

		// Check if the music asset is the one currently playing and if we have to rewind it
		if (musicAsset == currentMusicAsset && !options.rewindSameAsset)
		{
			Debug.Log($"[AudioPlayer] Continuing music asset {musicAsset.name} without restarting");
			return;
		}

		// Stop the current music coroutine and play the music asset at the source through a coroutine
		Debug.Log($"[AudioPlayer] Playing music asset {musicAsset.name} at {musicSource.name} with options {options}");
		if (currentMusicCoroutine != null)
			StopCoroutine(currentMusicCoroutine);
		currentMusicCoroutine = StartCoroutine(PlayMusicCoroutine(musicAsset, options));
	}

	// Play a music asset by index or name
	public void PlayMusicByIndex(int musicIndex, MusicPlaybackOptions options)
	{
		var musicAsset = musicAssets.GetAssetByIndex(musicIndex);
		if (musicAsset == null)
		{
			Debug.LogWarning($"[AudioPlayer] Could not find music by index: {musicIndex}");
			return;
		}
		PlayMusic(musicAsset, options);
	}
	public void PlayMusicByName(string musicName, MusicPlaybackOptions options)
	{
		var musicAsset = musicAssets.GetAssetByName(musicName);
		if (musicAsset == null)
		{
			Debug.LogWarning($"[AudioPlayer] Could not find music by name: {musicName}");
			return;
		}
		PlayMusic(musicAsset, options);
	}

	// Coroutine for playing music assets
	private IEnumerator PlayMusicCoroutine(MusicAsset musicAsset, MusicPlaybackOptions options)
	{
		// Stop the last music asset
		yield return StopMusicCoroutine();

		// Set the current music asset
		currentMusicAsset = musicAsset;

		// Check if we can play the intro
		if (musicAsset.introTrack.clip != null)
		{
			// Play the intro of the music asset at the source
			musicAsset.PlayIntroAtSource(musicSource);

			// Yield while the music is playing
			yield return new WaitWhile(() => musicSource.isPlaying);
		}

		// Check if we can play the loop
		if (musicAsset.loopTrack.clip != null)
		{
			// Play the intro of the music asset at the source
			musicAsset.PlayLoopAtSource(musicSource);

			// Yield while the music is playing
			yield return new WaitWhile(() => musicSource.isPlaying);
		}
	}

	// Stop the currently playing music asset
	public void StopMusic()
  {
		// Stop the current music coroutine and stop the currently playing music asset
		Debug.Log($"[AudioPlayer] Stopping music asset {currentMusicAsset.name}");
		if (currentMusicCoroutine != null)
			StopCoroutine(currentMusicCoroutine);
		StartCoroutine(StopMusicCoroutine());
	}

	// Coroutine for stopping music assets
	private IEnumerator StopMusicCoroutine()
  {
		// Stop the previous music asset
		if (currentMusicAsset != null && musicSource.isPlaying)
		{
			// Fade out the audio source
			yield return FadeOut(musicSource, 1.0f);
		}

		// Reset the current music asset
		currentMusicAsset = null;
	}

	// Play a sound asset at a source
	public void PlaySoundAt(SoundAsset soundAsset, SoundPlaybackOptions options, AudioSource source)
	{
		// Set the default options if none given
		options ??= new SoundPlaybackOptions();

		// Check if there is a sound asset to play
		if (soundAsset == null)
		{
			Debug.LogWarning($"[AudioPlayer] Sound asset is null");
			return;
		}

		// Play the sound asset at the source
		soundAsset.PlayAtSource(source, options.relativeVolume, options.relativePitch);

		// Mute the music if that's provided in the options
		if (options.duckVolume < 1.0f)
			StartCoroutine(DuckCoroutine(options.duckVolume, source));
	}

	// Play a sound asset at a source by index or name
	public void PlaySoundByIndexAt(int soundIndex, SoundPlaybackOptions options, AudioSource source)
	{
		var soundAsset = soundAssets.GetAssetByIndex(soundIndex);
		if (soundAsset == null)
		{
			Debug.LogWarning($"[AudioPlayer] Could not find sound by index: {soundIndex}");
			return;
		}
		PlaySoundAt(soundAsset, options, source);
	}
	public void PlaySoundByNameAt(string soundName, SoundPlaybackOptions options, AudioSource source)
	{
		var soundAsset = soundAssets.GetAssetByName(soundName);
		if (soundAsset == null)
		{
			Debug.LogWarning($"[AudioPlayer] Could not find sound by name: {soundName}");
			return;
		}
		PlaySoundAt(soundAsset, options, source);
	}

	// Play a sound asset at the sound source pool
	public AudioSource PlaySound(SoundAsset soundAsset, SoundPlaybackOptions options)
	{
		var source = soundSourcePool.Next();
		PlaySoundAt(soundAsset, options, source);
		return source;
	}

	// Play a sound asset at the sound source pool
	public AudioSource PlaySoundByIndex(int soundIndex, SoundPlaybackOptions options)
	{
		var soundAsset = soundAssets.GetAssetByIndex(soundIndex);
		if (soundAsset == null)
		{
			Debug.LogWarning($"[AudioPlayer] Could not find sound by index: {soundIndex}");
			return null;
		}
		return PlaySound(soundAsset, options);
	}
	public AudioSource PlaySoundByName(string soundName, SoundPlaybackOptions options)
	{
		var soundAsset = soundAssets.GetAssetByName(soundName);
		if (soundAsset == null)
		{
			Debug.LogWarning($"[AudioPlayer] Could not find sound by name: {soundName}");
			return null;
		}
		return PlaySound(soundAsset, options);
	}

	// Coroutine that mutes the music until an audio source is done playing
	public IEnumerator DuckCoroutine(float relativeVolume, AudioSource source)
	{
		float originalVolume = musicSource.volume;

		yield return FadeOut(musicSource, 0.25f, originalVolume * relativeVolume);
		yield return new WaitWhile(() => source.isPlaying);
		yield return FadeIn(musicSource, 0.75f, originalVolume);

	}

	// Coroutine that fades in an audio source
	public static IEnumerator FadeIn(AudioSource source, float duration, float volume = 1.0f)
	{
		var startVolume = source.volume;
		for (float t = 0; t < 1.0f; t += Time.deltaTime / duration)
		{
			source.volume = Mathf.Lerp(startVolume, volume, t);
			yield return null;
		}

		source.volume = volume;
	}

	// Coroutine that fades out an audio source
	public static IEnumerator FadeOut(AudioSource source, float duration, float volume = 0.0f, bool stopAfterFadeOut = false)
	{
		var startVolume = source.volume;
		for (float t = 0; t < 1.0f; t += Time.deltaTime / duration)
		{
			source.volume = Mathf.Lerp(startVolume, volume, t);
			yield return null;
		}

		source.volume = volume;
		if (stopAfterFadeOut)
			source.Stop();
	}
}