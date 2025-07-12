using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class InicioJuego : MonoBehaviour
{
	public GameObject panelInicio;
	public Button botonCargarPartida;
	public string escenaSig;
	public TransicionEscena transicionEscena;
	public AudioClip newMusicClip;
	public float duracionFade;
	public MovimientoTopDown movimientoJugador; 

	private static bool iniciado = false;

	GameManager gameManager;
	ControladorDatosJuego controladorDatosJuego;

	private void Awake()
	{
		gameManager = FindObjectOfType<GameManager>();
		controladorDatosJuego = gameManager.GetComponent<ControladorDatosJuego>();
	}

	void Start()
	{
		if (!iniciado)
		{
			// Mostrar el panel solo cuando se ejecuta el juego por primera vez
			panelInicio.SetActive(true);
			gameManager.SetVisibilidadCursor(true);
			iniciado = true;
		}
		else
		{
			panelInicio.SetActive(false);
		}

		ComprobarArchivoGuardado();
		Invoke("SetVisibilidadCursorDelay", 7.8f);
	}

	public void IniciarNuevaPartida()
	{
		// Reestablecemos el estado del juego
		controladorDatosJuego.EliminarDatos();
		controladorDatosJuego.jugador.GetComponent<Transform>().position = new Vector3(-1.47f, 1.83f, 0);
		IniciarPartida();		
	}

	public void CargarPartida()
	{
		// Cargamos el archivo de guardado del juego
		if (controladorDatosJuego.CargarDatos())
		{
			IniciarPartida();
		}
	}

	public void IniciarPartida()
	{
		StartCoroutine(transicionEscena.CambiarEscena(escenaSig, newMusicClip, duracionFade));

		gameManager.SetVisibilidadCursor(false);
		gameManager.HabilitarLoop();
		gameManager.juegoIniciado = true;
	}

	public void CerrarJuego()
	{
		Application.Quit();
	}

	public void ComprobarArchivoGuardado()
	{
		if (File.Exists(controladorDatosJuego.archivoGuardado))
		{
			botonCargarPartida.interactable = true;
		}
		else
		{
			botonCargarPartida.interactable = false;
		}
	}

	private void SetVisibilidadCursorDelay()
	{
		gameManager.SetVisibilidadCursor(true);
	}
}
