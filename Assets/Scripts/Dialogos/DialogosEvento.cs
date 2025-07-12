using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogosEvento : MonoBehaviour
{

	[Header("Texto")]
	[SerializeField] private GameObject panelDialogo;
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

	private void Update()
	{
		if (dialogoIniciado && !dialogoTerminado && (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)))
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

	public void OnEmpezarDialogo()
	{
		panelDialogo.SetActive(true);
		indiceLinea = 0;
		dialogoIniciado = true;
		dialogoTerminado = false;
		StopAllCoroutines();
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
			panelDialogo.SetActive(false);
		}
	}
}
