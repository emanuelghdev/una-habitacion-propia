using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorActividad : MonoBehaviour
{
	[Header("Transición de Escena")]
	[SerializeField] private string escenaSig;
	[SerializeField] private TransicionEscena transicionEscena;
	[SerializeField] private AudioClip newMusicClip;
	[SerializeField] private float duracionFade;

	[Header("Actividad")]
	[SerializeField] string tipoActividad;
	[SerializeField] private int puntosActividad;       // Puntos de bienestar que otorga la actividad
	[SerializeField] private int numeroActividad;

	GameManager gameManager;

	void Start()
	{
		gameManager = FindObjectOfType<GameManager>();
	}

	public void OnCompletarActividad()
	{
		// Incrementa la puntuación al completar la actividad
		gameManager.CompletarActividad(tipoActividad, numeroActividad, puntosActividad, escenaSig, newMusicClip, duracionFade);
	}
}
