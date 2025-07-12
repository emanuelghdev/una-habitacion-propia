using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class CreenciaTestUI : MonoBehaviour
{
	[Header("General")]
	public CuestionamientoSocratico controladorCuestionamientoSocratico;
	public GameObject menuEvaluacionCreencias;

	[Header("Botones")]
	public CanvasGroup grupoBotones;
	public Button[] botonesRespuesta;
	
	[Header("Creencias")]
	public TMP_Text textoCreencia;
	public string[] creencias = {
		"Siempre estar� solo",
		"Si algo puede salir mal, saldr� mal",
		"No soporto la incertidumbre",
		"Me siento juzgado todo el tiempo",
		"Las cosas malas siempre me pasan a m�",
		"Si no controlo todo, todo se desmoronar�",
		"Me impongo mucha presi�n",
		"Nunca cumplo las expectativas de los dem�s"
	};

	private int[] respuestasUsuario;
	private int actualIndiceCreencia = 0;

	void Start()
	{
		respuestasUsuario = new int[creencias.Length];
		StartCoroutine(DisplaySiguienteCreencia());

		for (int i = 0; i < botonesRespuesta.Length; i++)
		{
			int index = i;						// Necesario para evitar cierre de variable en la lambda
			botonesRespuesta[i].onClick.AddListener(() => OnRespuestaSeleccionada(index + 1));
		}
	}

	public IEnumerator DisplaySiguienteCreencia()
	{
		yield return new WaitForSeconds(0.5f);

		if (actualIndiceCreencia < creencias.Length)
		{
			textoCreencia.text = creencias[actualIndiceCreencia];
			ResetBotonesSeleccionados();        // Nos aseguraramos de que los botones se deseleccionen
			HabilitarBotones();
		}
		else
		{
			menuEvaluacionCreencias.SetActive(false);

			// Aqu� se inicia el cuestionamiento socr�tico
			controladorCuestionamientoSocratico.EmpezarCuestionamientoSocratico(respuestasUsuario, creencias);
		}
	}

	void OnRespuestaSeleccionada(int respuesta)
	{
		respuestasUsuario[actualIndiceCreencia] = respuesta;
		DeshabilitarBotones();

		// Pasamos a la siguiente creencia
		actualIndiceCreencia++;
		StartCoroutine(DisplaySiguienteCreencia());
	}

	void HabilitarBotones()
	{
		grupoBotones.blocksRaycasts = true; 
	}

	void DeshabilitarBotones()
	{
		grupoBotones.blocksRaycasts = false;
	}

	void ResetBotonesSeleccionados()
	{
		// Desactivar y reactivar botones para eliminar la selecci�n
		foreach (var button in botonesRespuesta)
		{
			button.interactable = false; // Desactivar bot�n temporalmente
		}

		foreach (var button in botonesRespuesta)
		{
			button.interactable = true; // Reactivar bot�n
		}
	}
}
