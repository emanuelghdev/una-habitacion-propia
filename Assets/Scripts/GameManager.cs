using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using TMPro;

public class GameManager : MonoBehaviour
{
	[Header("Singleton")]
	public static GameManager instance;

	[Header("Dialogos")]
	public GameObject dialogoBoxCarteles;
	public TextMeshProUGUI dialogoTextCarteles;

	[Header("Sonido")]
	public AudioSource audioSource;
	public AudioClip sonidoAcierto;

	[Header("Ejercicios")]
	public Dictionary<TipoEjercicio, bool[]> ejerciciosCompletados;
	private const int numEjSerenidad = 7;
	private const int numEjAsertividad = 7;

	[Header("Canvas")]
	public GameObject barraCarga;
	public TMP_Text puntosUI;
	public TMP_Text puntosUIPausa;
	public string nombreJugador;

	[Header("Inventario")]
	public List<Objeto> objetosInventario = new List<Objeto>();

	[Header("General")]
	public MenuPausa menuPausa;
	public TransicionEscena transicionEscena;
	public bool transicionTerminada = false;
	public bool juegoIniciado = false;
	public bool tutorialHabilitado = false;
	public int PB;
	public bool pausado = false;

	// Aplicamos patrón Singleton
	void Awake()
	{
		// Si ya existe una instancia y no es esta, destruye esta instancia
		if (GameManager.instance != null && GameManager.instance != this)
		{
			Destroy(gameObject);
		}
		else
		{
			// Asignamos esta instancia a la estática y haz que no se destruya al cambiar de escena
			GameManager.instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
	}

	void Start()
	{
		ejerciciosCompletados = new Dictionary<TipoEjercicio, bool[]>
		{
			{ TipoEjercicio.Serenidad, new bool[numEjSerenidad] },
            { TipoEjercicio.Resiliencia, new bool[numEjAsertividad] }
        };

		// Cargar el estado de los ejercicios desde PlayerPrefs
		CargarEstadoEjercicios(TipoEjercicio.Serenidad, numEjSerenidad);
		CargarEstadoEjercicios(TipoEjercicio.Resiliencia, numEjAsertividad);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape) && !pausado && menuPausa != null && transicionTerminada == true)
		{
			menuPausa.Pausa();
			pausado = true;
		}
	}


	// Método para manejar la visibilidad del cursor
	public void SetVisibilidadCursor(bool esVisible)
	{
		Cursor.visible = esVisible;
	}


	// Métodos para manejar la música
	public void IniciarMusica(AudioClip musicClip)
	{
		if (audioSource != null)
		{
			if (audioSource.clip != musicClip)
			{
				audioSource.clip = musicClip;
				audioSource.Play();
			}
		}
	}

	public void PararMusica()
	{
		if (audioSource != null)
		{
			audioSource.Stop();
		}
	}

	public void CambiarMusica(AudioClip newMusicClip)
	{
		if (audioSource != null)
		{
			audioSource.clip = newMusicClip;
			audioSource.Play();
		}
	}

	public void SetVolumenMusica(float volumen)
	{
		if (audioSource != null)
		{
			audioSource.volume = volumen;
		}
	}


	// Métodos para manejar sonidos
	public void IniciarSonido(AudioClip clip)
	{
		if (audioSource != null)
		{
			if (audioSource.clip != clip)
			{
				audioSource.PlayOneShot(clip);
			}
		}
	}

	public void HabilitarLoop()
	{
		audioSource.loop = true;
	}

	// Métodos de Fade para la música
	public IEnumerator FadeOutMusica(float duracionFade)
	{
		float volumenIni = audioSource.volume;
		float tiempo = 0;

		// Realizar el fade out durante la duración especificada
		while (tiempo < duracionFade)
		{
			tiempo += Time.deltaTime;
			audioSource.volume = Mathf.Lerp(volumenIni, 0, tiempo / duracionFade);
			yield return null;
		}

		// Aseguramos que el volumen es exactamente 0 y detener el audio
		audioSource.volume = 0;
		audioSource.Stop();
	}

	public IEnumerator FadeInMusica(float duracionFade)
	{
		float volumenIni = 0.2f;
		float volumenFin = 0.55f;
		float tiempo = 0;

		audioSource.volume = volumenIni;
		audioSource.Play();

		// Realizar el fade in durante la duración especificada
		while (tiempo < duracionFade)
		{
			tiempo += Time.deltaTime;
			audioSource.volume = Mathf.Lerp(volumenIni, volumenFin, tiempo / duracionFade);

			yield return null;
		}

		// Asegurar que el volumen es exactamente volumenFin
		audioSource.volume = volumenFin;
	}

	public IEnumerator FadeOutAndIn(AudioClip newMusicClip, float duracionFade)
	{
		yield return StartCoroutine(FadeOutMusica(duracionFade));

		audioSource.clip = newMusicClip;

		yield return StartCoroutine(FadeInMusica(duracionFade));
	}


	// Métodos para manejar los dialogos
	public void MostrarTexto(string text)
	{
		dialogoBoxCarteles.SetActive(true);

		// Utilizamos una expresión regular para encontrar secuencias que empiecen con "\uXXXX"
		string pattern = @"\\u[0-9A-Fa-f]{4}";
		MatchCollection matches = Regex.Matches(text, pattern);

		// Iteramos sobre las coincidencias encontradas y convierte las secuencias Unicode en caracteres
		foreach (Match match in matches)
		{
			string unicodeString = match.Value;
			int unicodeValue = int.Parse(unicodeString.Substring(2), System.Globalization.NumberStyles.HexNumber);
			char unicodeChar = (char)unicodeValue;
			text = text.Replace(unicodeString, unicodeChar.ToString());
		}

		// Asignamos el texto modificado al componente de texto
		dialogoTextCarteles.text = text;
	}

	public void OcultarTexto()
	{
		dialogoBoxCarteles.SetActive(false);
		dialogoTextCarteles.text = "";
	}


	//  Métodos para manejar las actividades
	private void CargarEstadoEjercicios(TipoEjercicio tipo, int cantidadEjercicios)
	{
		for (int i = 0; i < cantidadEjercicios; i++)
		{
			string key = tipo.ToString() + "_Ejercicio_" + i;
			ejerciciosCompletados[tipo][i] = PlayerPrefs.GetInt(key, 0) == 1;
		}
	}

	public void CompletarActividad(string tipo, int num, int puntos, string escenaSig, AudioClip newMusicClip, float duracionFade)
	{
		TipoEjercicio tipoEjercicioEnum = (TipoEjercicio)System.Enum.Parse(typeof(TipoEjercicio), tipo);

		// Marcar el ejercicio como completado y guardar el estado en PlayerPrefs
		ejerciciosCompletados[tipoEjercicioEnum][num] = true;
		string key = tipo + "_Ejercicio_" + num;
		PlayerPrefs.SetInt(key, 1);
		PlayerPrefs.Save();

		// Sumar puntos
		SumarPuntos(puntos);

		StartCoroutine(transicionEscena.CambiarEscena(escenaSig, newMusicClip, duracionFade));
	}

	public void CompletarActividadDiario(int puntos)
	{
		SumarPuntos(puntos);
	}

	private void SumarPuntos(int puntos)
	{
		PB += puntos;
	}


	// Métodos para manejar el canvas
	private IEnumerator BarraCarga()
	{
		barraCarga.SetActive(true);
		yield return new WaitForSecondsRealtime(2f);
		IniciarSonido(sonidoAcierto);
		barraCarga.SetActive(false);
	}

	public void MostrarBarraCarga()
	{
		StartCoroutine(BarraCarga());
	}

	public void ActualizarInterfaz()
	{
		puntosUI.text = PB.ToString();
		puntosUIPausa.text = PB.ToString();
	}

	
	// Métodos para manejar los objetos del inventario
	public void ComprarObjeto(Objeto objetoTienda, int precio, int cantidad)
	{
		SumarPuntos(-precio);		// Restamos el precio a los PB acumulados

		// Modificamos el objeto de la tienda
		objetoTienda.cantidadObjeto += cantidad;

		// Añadimos el objeto al inventario
		AñadirObjeto(objetoTienda.idObjeto, objetoTienda.nombreIconoObjeto, cantidad);
	}

	public void AñadirObjeto(string id, string nombreIcono, int cantidad)
	{
		Objeto objeto = objetosInventario.Find(i => i.idObjeto == id);
		if (objeto != null)
		{
			// Si el objeto ya está en el inventario, aumenta la cantidad
			objeto.cantidadObjeto += cantidad;
		}
		else
		{
			// Si no está, agrega un nuevo objeto al inventario
			objetosInventario.Add(new Objeto(id, nombreIcono, cantidad));
		}
	}

	public void EliminarObjeto(string id, int cantidad)
	{
		Objeto objeto = objetosInventario.Find(i => i.idObjeto == id);
		if (objeto != null && (objeto.cantidadObjeto - cantidad) > 0)
		{
			// Si el objeto está en el inventario de sobra, solamenete reducimos la cantidad
			objeto.cantidadObjeto -= cantidad;
		}
		else if(objeto != null)
		{
			// Si no, eliminamos el objeto del inventario
			objetosInventario.Remove(objeto);
		}
	}

	public bool TieneObjeto(string id, int cantidad)
	{
		Objeto objeto = objetosInventario.Find(i => i.idObjeto == id);

		if (objeto != null && objeto.cantidadObjeto >= cantidad)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}