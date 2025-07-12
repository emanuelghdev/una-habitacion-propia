using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using System.Linq;
using System;
using UnityEngine.UI;

public class DialogoTutorial : MonoBehaviour
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
	[SerializeField] private GameObject insertarNombre;
	[SerializeField] private Button botonConfirmar;
	[SerializeField] private TMP_InputField inputFieldNombre;
	

	[Header("Recargar Escena")]
	public string escenaSig;
	public TransicionEscena transicionEscena;
	public AudioClip newMusicClip;
	public float duracionFade;

	[Header("ID del Menu")]
	[SerializeField] private int menuID; // Identificador del menú

	GameManager gameManager;

	private void Start()
	{
		gameManager = FindObjectOfType<GameManager>();

		// Verificar si los diálogos ya se han mostrado
		if (!DialogosMostrados(menuID) && gameManager.juegoIniciado == true)
		{
			// Si no se han mostrado, iniciar el diálogo
			EmpezarDialogo();
			botonConfirmar.onClick.AddListener(OnBotonConfirmarClick);
			gameManager.SetVisibilidadCursor(false);
		}
		else
		{
			// Si ya se han mostrado, asegurar que el panel de diálogo esté desactivado y el canvas tambien
			panelDialogo.SetActive(false);
			canvasContenedor.SetActive(false);
			dialogoTerminado = true;
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
		gameManager.juegoIniciado = false;
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

			// Guardar que los diálogos ya se han mostrado para este menú
			GuardarDialogoMostrado(menuID);

			insertarNombre.SetActive(true);
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

	void OnBotonConfirmarClick()
	{
		string inputText = inputFieldNombre.text.Trim();  // Elimina los espacios en blanco al inicio y al final

		// Verifica si el campo está vacío
		if (string.IsNullOrEmpty(inputText))
		{
			return;
		}

		gameManager.nombreJugador = inputText;
		PlayerPrefs.SetString("NombrePersonaje", gameManager.nombreJugador);
		gameManager.SetVisibilidadCursor(false);
		StartCoroutine(transicionEscena.CambiarEscena(escenaSig, newMusicClip, duracionFade));

		gameManager.juegoIniciado = true;
		gameManager.tutorialHabilitado = true;
	}
}

