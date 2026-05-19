using TMPro;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSlider : MonoBehaviour
{
    [SerializeField]
    private AudioMixer Mixer;
    [SerializeField]
    private AudioSource AudioSource;
    [SerializeField]
    private TextMeshProUGUI ValueText;
    [SerializeField]
    private AudioMixMode MixMode;

    // Nuevo: elegir canal del mixer o un parámetro personalizado
    [SerializeField]
    private MixerChannel Channel = MixerChannel.Master;
    [SerializeField]
    private string CustomParameter = "Volume";

    private void Start()
    {
        string param = GetParameterName();
        float saved = PlayerPrefs.GetFloat(GetPrefsKey(param), 1f);

        // Inicializar según el modo seleccionado
        if (MixMode == AudioMixMode.LinearAudioSourceVolume)
        {
            if (AudioSource != null)
                AudioSource.volume = saved;
        }
        else if (MixMode == AudioMixMode.LinearMixerVolume)
        {
            Mixer.SetFloat(param, -80 + saved * 80);
        }
        else if (MixMode == AudioMixMode.LogrithmicMixerVolume)
        {
            Mixer.SetFloat(param, Mathf.Log10(Mathf.Max(saved, 0.0001f)) * 20f);
        }
    }

    public void OnChangeSlider(float Value)
    {
        ValueText.SetText($"{Value.ToString("N4")}");

        string param = GetParameterName();

        switch (MixMode)
        {
            case AudioMixMode.LinearAudioSourceVolume:
                if (AudioSource != null)
                    AudioSource.volume = Value;
                break;
            case AudioMixMode.LinearMixerVolume:
                Mixer.SetFloat(param, (-80 + Value * 80));
                break;
            case AudioMixMode.LogrithmicMixerVolume:
                Mixer.SetFloat(param, Mathf.Log10(Mathf.Max(Value, 0.0001f)) * 20f);
                break;
        }

        PlayerPrefs.SetFloat(GetPrefsKey(param), Value);
        PlayerPrefs.Save();
    }

    private string GetParameterName()
    {
        switch (Channel)
        {
            case MixerChannel.Master:
                return "Volume"; // mantenga "Volume" para master por compatibilidad
            case MixerChannel.Music:
                return "MusicVolume";
            case MixerChannel.SFX:
                return "SFXVolume";
            case MixerChannel.Custom:
                return CustomParameter;
            default:
                return "Volume";
        }
    }

    private string GetPrefsKey(string param) => $"Audio_{param}";

    public enum AudioMixMode
    {
        LinearAudioSourceVolume,
        LinearMixerVolume,
        LogrithmicMixerVolume
    }

    public enum MixerChannel
    {
        Master,
        Music,
        SFX,
        Custom
    }
}
