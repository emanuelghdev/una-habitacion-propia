using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using System.Linq;
using System;
using UnityEngine.UI;

public class DialogoFinal : MonoBehaviour
{
	[Header("Texto")]
	[SerializeField] private GameObject panelDialogo;
	[SerializeField] private TMP_Text textoDialogo;
	[SerializeField] private float tiempoTyping = 0.04f;
	[SerializeField, TextArea(4, 6)] private string[] lineasDialogo;
	private bool dialogoTerminado;
	private int indiceLinea;

	[Header("Sonido")]
	[SerializeField] private AudioClip voz;
	[SerializeField] private AudioSource audioSource;
	[SerializeField] private int letrasPorSonido;

	[Header("IU")]
	[SerializeField] private GameObject canvasContenedor;
	[SerializeField] private Cartel cartelCasaJugador;



	[Header("Recargar Escena")]
	public string escenaSig;
	public TransicionEscena transicionEscena;
	public AudioClip newMusicClip;
	public float duracionFade;
	public AudioClip musicaFinal;

	[Header("ID del Menu")]
	[SerializeField] private int menuID; // Identificador del menú

	GameManager gameManager;

	private void Start()
	{
		gameManager = FindObjectOfType<GameManager>();

		if (CargarEstadoMision() && PlayerPrefs.GetInt("FinalMostrado", 0) != 1)
		{
			PlayerPrefs.SetInt("FinalMostrado", 1);
			EmpezarDialogo();
		}
	}

	private void Update()
	{
		if (!dialogoTerminado && PlayerPrefs.GetInt("FinalMostrado", 0) == 1  && (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)))
		{
			if (textoDialogo.text == lineasDialogo[indiceLinea])
			{
				SiguienteLinea();
			}
			else
			{
				StopAllCoroutines();
				textoDialogo.text = lineasDialogo[indiceLinea];
			}
		}
	}

	public void EmpezarDialogo()
	{
		gameManager.juegoIniciado = false;
		gameManager.FadeOutMusica(0.5f);
		gameManager.IniciarMusica(musicaFinal);
		gameManager.FadeInMusica(0.5f);
		canvasContenedor.SetActive(true);
		panelDialogo.SetActive(true);
	
		indiceLinea = 0;
		StartCoroutine(MostrarLinea());
	}

	IEnumerator MostrarLinea()
	{
		textoDialogo.text = string.Empty;
		int indiceLetra = 0;

		foreach (char ch in lineasDialogo[indiceLinea])
		{
			textoDialogo.text += ch;

			if (indiceLetra % letrasPorSonido == 0)
			{
				audioSource.PlayOneShot(voz);
			}

			indiceLetra++;
			yield return new WaitForSeconds(tiempoTyping);
		}
	}

	private void SiguienteLinea()
	{
		indiceLinea++;
		
		if(indiceLinea < lineasDialogo.Length){
			StartCoroutine(MostrarLinea());
		}
		else{
			dialogoTerminado = true;
			panelDialogo.SetActive(false);
			canvasContenedor.SetActive(false);

			PlayerPrefs.SetString("CasaJugador", "Casa de " + gameManager.nombreJugador);
			cartelCasaJugador.text = PlayerPrefs.GetString("CasaJugador", "Se vende");
			
			StartCoroutine(transicionEscena.CambiarEscena(escenaSig));
			gameManager.juegoIniciado = true;
		}
	}

	private bool CargarEstadoMision()
	{
		string nombreNPC1 = "Iris";
		string nombreNPC2 = "Evencio";
		string nombreNPC3 = "Felipe";
		string nombreNPC4 = "Alex";
		string nombreNPC5 = "Tamara";
		string nombreNPC6 = "Sofía";
		string nombreNPC7 = "Lucio";
		string nombreNPC8 = "Julián";
		string nombreNPC9 = "Aura";

		// Comprobar si la misión para este NPC ya ha sido finalizada
		return (PlayerPrefs.GetInt(nombreNPC1 + "MisionFinalizada", 0) == 1 &&
				PlayerPrefs.GetInt(nombreNPC2 + "MisionFinalizada", 0) == 1 &&
				PlayerPrefs.GetInt(nombreNPC3 + "MisionFinalizada", 0) == 1 &&
				PlayerPrefs.GetInt(nombreNPC4 + "MisionFinalizada", 0) == 1 &&
				PlayerPrefs.GetInt(nombreNPC5 + "MisionFinalizada", 0) == 1 &&
				PlayerPrefs.GetInt(nombreNPC6 + "MisionFinalizada", 0) == 1 &&
				PlayerPrefs.GetInt(nombreNPC7 + "MisionFinalizada", 0) == 1 &&
				PlayerPrefs.GetInt(nombreNPC8 + "MisionFinalizada", 0) == 1 &&
				PlayerPrefs.GetInt(nombreNPC9 + "MisionFinalizada", 0) == 1);
	}
}

