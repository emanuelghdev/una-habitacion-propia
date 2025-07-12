using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System;

public class DialogosMenu : MonoBehaviour
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
	[SerializeField] private CanvasGroup canvasGroup;

	[Header("ID del Menu")]
	[SerializeField] private int menuID; // Identificador del menú

	GameManager gameManager;

	private void Start()
	{
		gameManager = FindObjectOfType<GameManager>();

		// Verificar si los diálogos ya se han mostrado
		if (!DialogosMostrados(menuID))
		{
			// Si no se han mostrado, iniciar el diálogo
			EmpezarDialogo();
			gameManager.SetVisibilidadCursor(false);
		}
		else
		{
			// Si ya se han mostrado, asegurar que el panel de diálogo esté desactivado
			panelDialogo.SetActive(false);
			dialogoTerminado = true;
			gameManager.SetVisibilidadCursor(true);
		}
	}

	private void Update()
	{
		if (!dialogoTerminado && (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)))
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

	private void EmpezarDialogo()
	{
		panelDialogo.SetActive(true);

		// Desactivamos la interactividad del canvas
		canvasGroup.alpha = 0.75f;
		canvasGroup.interactable = false;
		canvasGroup.blocksRaycasts = false;


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

			// Reactivamos la interactividad del canvas
			canvasGroup.alpha = 1;
			canvasGroup.interactable = true;
			canvasGroup.blocksRaycasts = true;

			// Guardar que los diálogos ya se han mostrado para este menú
			GuardarDialogoMostrado(menuID);

			// Hacemos que el cursor sea visible
			gameManager.SetVisibilidadCursor(true);
		}
	}

	// Métodos de carga y guardado de estados de dialogos
	private bool[] CargarEstadosDialogos()
	{
		string estadoSerializado = PlayerPrefs.GetString("EstadosDialogos", string.Empty);
		if (string.IsNullOrEmpty(estadoSerializado))
		{
			return new bool[4];				// El número de menús con tutoriales
		}
		return estadoSerializado.Split(',').Select(bool.Parse).ToArray();
	}

	private void GuardarEstadosDialogos(bool[] estados)
	{
		string estadoSerializado = string.Join(",", estados.Select(b => b.ToString()).ToArray());
		PlayerPrefs.SetString("EstadosDialogos", estadoSerializado);
	}

	private bool DialogosMostrados(int id)
	{
		bool[] estados = CargarEstadosDialogos();
		return estados.Length > id && estados[id];
	}

	private void GuardarDialogoMostrado(int id)
	{
		bool[] estados = CargarEstadosDialogos();
		if (estados.Length <= id)
		{
			Array.Resize(ref estados, id + 1);
		}
		estados[id] = true;
		GuardarEstadosDialogos(estados);
	}
}

