using System.Collections;
using UnityEngine;
using TMPro; // Import TextMeshPro namespace

public class MusicPlayer : MonoBehaviour
{
    public AudioSource audioSource;          // The AudioSource component
    public AudioClip[] songs;                // Array of songs to play
    public string[] songTitles;              // Corresponding song titles
    public TMP_Text songTitleText;           // TMP_Text for song title display
    public float fadeDuration = 1f;          // Duration of fade in and fade out

    private int currentSongIndex = 0;

    void Start()
    {
        if (songs.Length == 0 || songs.Length != songTitles.Length)
        {
            Debug.LogError("Make sure to set songs and songTitles arrays with the same length.");
            return;
        }

        PlayNextSong();
    }

    void Update()
    {
        // Check if the current song has finished playing
        if (!audioSource.isPlaying)
        {
            PlayNextSong();
        }
    }

    void PlayNextSong()
    {
        if (songs.Length == 0) return;

        // Play the current song
        audioSource.clip = songs[currentSongIndex];
        audioSource.Play();

        // Display the song title with fade effect
        StartCoroutine(ShowSongTitleWithFade(songTitles[currentSongIndex], audioSource.clip.length));

        // Move to the next song index (loop back if at the end)
        currentSongIndex = (currentSongIndex + 1) % songs.Length;
    }

    IEnumerator ShowSongTitleWithFade(string title, float songDuration)
    {
        // Set the text and make it visible
        songTitleText.text = title;
        Color originalColor = songTitleText.color;

        // Fade in
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;
            songTitleText.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(0, 1, normalizedTime));
            yield return null;
        }
        songTitleText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1);

        // Wait while the song is playing (duration of the song minus fade out time)
        yield return new WaitForSeconds(songDuration - fadeDuration);

        // Fade out
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            float normalizedTime = t / fadeDuration;
            songTitleText.color = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(1, 0, normalizedTime));
            yield return null;
        }
        songTitleText.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
    }
}
