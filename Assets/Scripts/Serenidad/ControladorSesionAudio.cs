using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ControladorSesionAudio : MonoBehaviour
{
	[Header("Transici�n de Escena")]
	[SerializeField] private string escenaSig;
	[SerializeField] private TransicionEscena transicionEscena;
	[SerializeField] private AudioClip newMusicClip;
	[SerializeField] private float duracionFade;

	[Header("Actividad")]
	[SerializeField] private int puntosActividad;       // Puntos de bienestar que otorga la actividad
	[SerializeField] private int numeroActividad;    
	public AudioSource audioSource;

	[Header("UI")]
	[SerializeField] private Slider audioSlider;

	GameManager gameManager;

	void Start()
	{
		gameManager = FindObjectOfType<GameManager>();

		// Suscribe la funci�n CheckAudioCompletion al evento de finalizaci�n de la reproducci�n del audio
		audioSource = GetComponent<AudioSource>();
		audioSource.loop = false;
		audioSource.Play();

		Invoke("OnCompletarActividad", audioSource.clip.length);
		StartCoroutine(UpdateSlider());
	}

	// Forzar la actualizaci�n del Slider cuando la aplicaci�n recupera el foco
	private void OnApplicationFocus(bool hasFocus)
	{
		if (hasFocus)
		{
			audioSlider.value = audioSource.time / audioSource.clip.length;
		}
	}

	// Actualiza el slider en funcion de la reproducci�n del audio
	IEnumerator UpdateSlider()
	{
		while (audioSource.isPlaying)
		{
			audioSlider.value = audioSource.time / audioSource.clip.length;
			yield return null;
		}

		audioSlider.value = 1; // Nos aseguramos de que el slider est� lleno cuando el audio termine
	}

	// Se llama cuando la reproducci�n del audio ha terminado
	void OnCompletarActividad()
	{
		gameManager.CompletarActividad("Serenidad", numeroActividad, puntosActividad, escenaSig, newMusicClip, duracionFade);
	}
}
