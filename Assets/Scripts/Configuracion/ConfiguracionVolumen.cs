using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class ConfiguracionVolumen : MonoBehaviour
{
	public Slider sliderVolumen;
	public AudioMixer audioMixer;

	private float valorPredeterminado = 0.75f;

	private void Awake()
	{
		// Configuramos el valor inicial del slider basado en el volumen actual
		float volumen = PlayerPrefs.GetFloat("VolumenJuego", valorPredeterminado);
		sliderVolumen.value = volumen;
		SetVolumen(volumen);

		// A�adimos un listener al slider para que el volumen cambie en tiempo real
		sliderVolumen.onValueChanged.AddListener(SetVolumen);
	}

	// M�todo que ajusta el volumen
	public void SetVolumen(float volumen)
	{
		// Ajustamos el volumen del AudioMixer para convertir el valor lineal del Slider en una escala logar�timica
		if (volumen == 0)
		{
			audioMixer.SetFloat("VolumenMaster", -80f);			// -80 dB es b�sicamente silencio total
		}
		else
		{
			audioMixer.SetFloat("VolumenMaster", Mathf.Log10(volumen) * 20);
		}

		// Guardamos el volumen ajustado
		PlayerPrefs.SetFloat("VolumenJuego", volumen);
	}
}
