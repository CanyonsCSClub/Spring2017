using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour {

	public Toggle fullscreenToggle;
	public Dropdown resolutionDropdown;
	public Slider musicSlider;
	public Toggle musicToggle;

	public AudioSource musicSource;
	public Resolution[] resolutions;
	GameSettings gameSettings;

	void OnEnable()
	{
		gameSettings = new GameSettings ();

		fullscreenToggle.onValueChanged.AddListener (delegate {
			onFullscreenToggle ();
		});

		resolutionDropdown.onValueChanged.AddListener (delegate {
			onResolutionChange ();
		});

		musicSlider.onValueChanged.AddListener (delegate {
			onMusicVolumeChange ();
		});

		musicToggle.onValueChanged.AddListener (delegate {
			onMusicToggle ();
		});

		resolutions = Screen.resolutions;

		foreach (Resolution resolution in resolutions) {
			resolutionDropdown.options.Add (new Dropdown.OptionData (resolution.ToString ()));
		}
	}

	public void onFullscreenToggle()
	{
		gameSettings.fullScreen = Screen.fullScreen = fullscreenToggle.isOn;

	}

	public void onResolutionChange()
	{
		Screen.SetResolution (resolutions [resolutionDropdown.value].width, resolutions [resolutionDropdown.value].height,Screen.fullScreen);
	}

	public void onMusicVolumeChange()
	{
		musicSource.volume = gameSettings.musicVolume = musicSlider.value;
	}

	public void onMusicToggle()
	{
		gameSettings.musicOn = musicToggle.isOn;
		if (!musicToggle.isOn) {
			musicSource.Pause ();
		} else {
			musicSource.Play ();
		}
	}

	// Implemented later
	public void saveSettings()
	{

	}

	// Implemented later
	public void loadSettings()
	{

	}

}
