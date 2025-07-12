using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DialogosNPC : MonoBehaviour
{
	private bool jugadorEnRango;

	[Header("Jugador")]
	public GameObject jugador;

	[Header("Texto")]
	[SerializeField] private GameObject ControladorNPC;
	[SerializeField] private ControladorDialogos controladorDialogos;
	[SerializeField] private GameObject panelDialogo;
	[SerializeField] private TMP_Text textoDialogo;
	[SerializeField] private float tiempoTyping = 0.04f;
	[SerializeField, TextArea(4, 6)] private string[] lineasDialogo;
	private bool dialogoIniciado;
	private bool dialogoTerminado;
	private int indiceLinea;
	public bool puedeHablar = true;

	[Header("Sonido")]
	[SerializeField] private AudioClip voz;
	[SerializeField] private AudioSource audioSource;
	[SerializeField] private int letrasPorSonido;

	[Header("Prefab")]
	public GameObject bannerDialogoPrefab;                  // Prefab del banner de personaje
	public string nombrePj;
	public Sprite bannerIzquierda;
	public Sprite bannerMedio;
	public Sprite bannerDerecha;
	public Sprite iconoPersonaje;

	private GameObject instanciaBanner;                     // Variable para almacenar la instancia del prefab

	GameManager gameManager;
	private void Start()
	{
		gameManager = FindObjectOfType<GameManager>();
	}

	private void Update()
	{
		if (jugadorEnRango)
		{
			if (!dialogoIniciado && !dialogoTerminado && puedeHablar && gameManager .juegoIniciado && Input.GetKeyDown(KeyCode.E))
			{
				EmpezarDialogo();
			}
			else if (!dialogoTerminado && panelDialogo.activeSelf && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)))
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
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			jugadorEnRango = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			jugadorEnRango = false;
			dialogoIniciado = false;
			dialogoTerminado = false;
		}
	}

	public void EmpezarDialogo()
	{
		jugador.GetComponent<MovimientoTopDown>().enabled = false;
		dialogoIniciado = true;
		panelDialogo.SetActive(true);
		InstanciarPrefab();

		// Obtener las líneas de diálogo desde el ControladorDialogos
		lineasDialogo = controladorDialogos.ObtenerLineasDialogo();
		indiceLinea = 0;

		StartCoroutine(MostrarLinea());
	}

	private void InstanciarPrefab()
	{
		if (instanciaBanner == null)
		{
			// Asignamos el nombre del personaje
			bannerDialogoPrefab.GetComponentInChildren<TMP_Text>().text = nombrePj;

			// Asignamos la cara al personaje
			GameObject cara = bannerDialogoPrefab.transform.Find("Cara").gameObject;
			cara.GetComponent<Image>().sprite = iconoPersonaje;

			// Asignamos el fondo del banner correspondiente
			GameObject fondo = bannerDialogoPrefab.transform.Find("Fondo").gameObject;

			fondo.transform.Find("Izquierda").GetComponent<Image>().sprite = bannerIzquierda;
			fondo.transform.Find("IzquierdaMedio").GetComponent<Image>().sprite = bannerMedio;
			fondo.transform.Find("DerechaMedio").GetComponent<Image>().sprite = bannerMedio;
			fondo.transform.Find("Derecha").GetComponent<Image>().sprite = bannerDerecha;

			bannerDialogoPrefab.name = "Banner_" + nombrePj;
			instanciaBanner = Instantiate(bannerDialogoPrefab, panelDialogo.GetComponent<Transform>());
		}
		else
		{
			// Si ya existe, nos aseguramos de que esté activo
			instanciaBanner.SetActive(true);
		}
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

		if (indiceLinea < lineasDialogo.Length)
		{
			StartCoroutine(MostrarLinea());
		}
		else
		{
			if (controladorDialogos.turnoJugador)
			{
				dialogoIniciado = false;
				instanciaBanner.SetActive(false);
				panelDialogo.SetActive(false);
				controladorDialogos.HablarJugador();
				controladorDialogos.turnoJugador = false;
			}
			else
			{
				dialogoIniciado = false;
				instanciaBanner.SetActive(false);
				panelDialogo.SetActive(false);
				jugador.GetComponent<MovimientoTopDown>().enabled = true;
			}
		}
	}
}