using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogosEntradaMenu : MonoBehaviour
{
	private bool jugadorEnRango;

	[Header("Jugador")]
	[SerializeField] private GameObject jugador;

	[Header("Texto")]
	[SerializeField] private GameObject panelDialogo;
	[SerializeField] private GameObject banner;
	[SerializeField] private TMP_Text textoDialogo;
	[SerializeField] private float tiempoTyping = 0.04f;
	[SerializeField, TextArea(4, 6)] private string[] lineasDialogo;
	private bool dialogoIniciado;
	private bool dialogoTerminado;
	private int indiceLinea;

	[Header("Sonido")]
	[SerializeField] private AudioClip voz;
	[SerializeField] private AudioSource audioSource;
	[SerializeField] private int letrasPorSonido;

	[Header("Transicion Escena")]
	[SerializeField] private string escenaSig;
	[SerializeField] private int numMenu;                           // 1: serenidad, 2: asertividad, 3: resiliencia, 4: tienda
	[SerializeField] private TransicionEscena transicionEscena;
	[SerializeField] private AudioClip newMusicClip;
	[SerializeField] private float duracionFade;


	private void Update()
	{
		if(jugadorEnRango){
			if (!dialogoIniciado && !dialogoTerminado && Input.GetKeyDown(KeyCode.E))
			{
				EmpezarDialogo();
			}
			else if (!dialogoTerminado && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)))
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
		if(collision.gameObject.CompareTag("Player"))
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

	private void EmpezarDialogo()
	{
		jugador.GetComponent<MovimientoTopDown>().enabled = false;
		dialogoIniciado = true;
		banner.SetActive(true);
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

		if (indiceLinea < lineasDialogo.Length)
		{
			StartCoroutine(MostrarLinea());
		}
		else
		{
			dialogoIniciado = false;
			dialogoTerminado = true;
			banner.SetActive(false);
			panelDialogo.SetActive(false);

			// Antes de cambiar de escena guardamos la posicion del personaje indicando el menu al que entra
			PlayerPrefs.SetFloat("EntrandoEscena", numMenu);

			StartCoroutine(transicionEscena.CambiarEscena(escenaSig, newMusicClip, duracionFade));
			jugador.GetComponent<MovimientoTopDown>().enabled = true;
		}
	}
}
