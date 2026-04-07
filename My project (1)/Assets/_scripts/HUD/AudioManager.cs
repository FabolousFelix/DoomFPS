using UnityEngine;
using UnityEngine.Audio; // Necesario para AudioMixer

public class AudioManager : MonoBehaviour
{
    // Singleton para llamar al manager desde otros scripts
    public static AudioManager instance;

    // Referencia al mezclador de audio (arrastrar desde el proyecto)
    [Header("Audio Mixer")]
    public AudioMixer audioMixer;

    [Header("Fuentes de audio")]
    private AudioSource SFXSource;   // Efectos de sonido
    private AudioSource musicSource; // Música de fondo (nuevo)

    // Clips de sonido existentes
    public AudioClip enemyDamage;

    // Claves para PlayerPrefs
    private const string MASTER_VOL_KEY = "MasterVolume";
    private const string MUSIC_VOL_KEY = "MusicVolume";
    private const string SFX_VOL_KEY = "SFXVolume";

    // Valores actuales (rango 0-1)
    private float masterVolume = 1f;
    private float musicVolume = 1f;
    private float sfxVolume = 1f;

    private void Awake()
    {
        // Singleton y persistencia entre escenas
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Obtener o añadir los AudioSources
        SFXSource = GetComponent<AudioSource>();
        if (SFXSource == null)
            SFXSource = gameObject.AddComponent<AudioSource>();

        // Crear AudioSource separado para la música
        AudioSource[] sources = GetComponents<AudioSource>();
        if (sources.Length < 2)
            musicSource = gameObject.AddComponent<AudioSource>();
        else
            musicSource = sources[1];

        // Configurar la música para que suene en bucle por defecto
        musicSource.loop = true;
        musicSource.playOnAwake = false;

        // Cargar volúmenes guardados y aplicarlos al mezclador
        CargarVolumenes();
        AplicarVolumenes();
    }

    private void Start()
    {
        // (Opcional) Reproducir música si hay un clip asignado
        // Ejemplo: musicSource.clip = musicaFondo; musicSource.Play();
    }

    // Convierte un valor lineal (0-1) a decibelios (logarítmico)
    private float LinearToDecibel(float linear)
    {
        return linear > 0.0001f ? 20f * Mathf.Log10(linear) : -80f;
    }

    // Aplica los volúmenes actuales al AudioMixer
    private void AplicarVolumenes()
    {
        if (audioMixer == null) return;

        audioMixer.SetFloat("MasterVolume", LinearToDecibel(masterVolume));
        audioMixer.SetFloat("MusicVolume", LinearToDecibel(musicVolume));
        audioMixer.SetFloat("SFXVolume", LinearToDecibel(sfxVolume));
    }

    // Carga los volúmenes guardados (o usa valores por defecto)
    private void CargarVolumenes()
    {
        masterVolume = PlayerPrefs.GetFloat(MASTER_VOL_KEY, 1f);
        musicVolume = PlayerPrefs.GetFloat(MUSIC_VOL_KEY, 1f);
        sfxVolume = PlayerPrefs.GetFloat(SFX_VOL_KEY, 1f);
    }

    // --- Métodos públicos para la UI ---
    public void SetMasterVolume(float value)
    {
        masterVolume = Mathf.Clamp01(value);
        if (audioMixer != null)
            audioMixer.SetFloat("MasterVolume", LinearToDecibel(masterVolume));
        PlayerPrefs.SetFloat(MASTER_VOL_KEY, masterVolume);
        PlayerPrefs.Save();
    }

    public void SetMusicVolume(float value)
    {
        musicVolume = Mathf.Clamp01(value);
        if (audioMixer != null)
            audioMixer.SetFloat("MusicVolume", LinearToDecibel(musicVolume));
        PlayerPrefs.SetFloat(MUSIC_VOL_KEY, musicVolume);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float value)
    {
        sfxVolume = Mathf.Clamp01(value);
        if (audioMixer != null)
            audioMixer.SetFloat("SFXVolume", LinearToDecibel(sfxVolume));
        PlayerPrefs.SetFloat(SFX_VOL_KEY, sfxVolume);
        PlayerPrefs.Save();
    }

    // Métodos para obtener los valores actuales (útil para inicializar Sliders)
    public float GetMasterVolume() => masterVolume;
    public float GetMusicVolume() => musicVolume;
    public float GetSFXVolume() => sfxVolume;

    // --- Métodos para reproducir sonidos (los que ya tenías) ---
    public void PlayEnemyDamage()
    {
        PlayAudio(enemyDamage);
    }

    // Método base para reproducir efectos en el AudioSource de SFX
    private void PlayAudio(AudioClip clip)
    {
        if (clip != null && SFXSource != null)
            SFXSource.PlayOneShot(clip);
    }

    // Método adicional para reproducir música (si quieres cambiarla dinámicamente)
    public void PlayMusic(AudioClip musicClip)
    {
        if (musicClip == null || musicSource == null) return;
        musicSource.clip = musicClip;
        musicSource.Play();
    }

    public void StopMusic()
    {
        if (musicSource != null)
            musicSource.Stop();
    }
}
