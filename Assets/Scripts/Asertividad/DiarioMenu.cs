using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DiarioMenu : MonoBehaviour
{
	[Header ("Lista Entradas")]
	public GameObject entradaPrefab;
	public Transform listaEntradasPadre;
	private ControladorDiario controladorDiario;

	[Header ("Cambiar Escena")]
	[SerializeField] private string escenaSig;
	[SerializeField] private TransicionEscena transicionEscena;
	[SerializeField] private AudioClip newMusicClip;
	[SerializeField] private float duracionFade;

	void Start()
	{
		controladorDiario = FindObjectOfType<ControladorDiario>();
		CargarEntradas();
	}

	void CargarEntradas()
	{
		List<EntradaDiario> entradas = controladorDiario.CargarDiario();
		foreach (EntradaDiario entrada in entradas)
		{
			GameObject entradaGO = Instantiate(entradaPrefab, listaEntradasPadre);

			// Accedemos al GameObject "Graficos" y los botones para ver y eliminar la entrada
			GameObject IconoVer = entradaGO.transform.Find("Icono").gameObject;
			Button botonVer = IconoVer.transform.GetComponent<Button>();
			Button botonBorrar = entradaGO.transform.Find("Eliminar").gameObject.transform.GetComponent<Button>();
			Transform graficos = entradaGO.transform.Find("Graficos");

			// Accedemos a los elementos específicos dentro de "Graficos"
			TMP_Text textoEntrada = graficos.transform.Find("Text (TMP)").gameObject.transform.GetComponent<TMP_Text>();
			TMP_Text fechaEntrada = graficos.transform.Find("Fecha").gameObject.transform.GetComponent<TMP_Text>();

			// Asignamos valores a los textos
			textoEntrada.text = entrada.texto.Substring(0, Math.Min(18, entrada.texto.Length));
			if(entrada.texto.Length > 18){
				textoEntrada.text += "...";
			}

			fechaEntrada.text = entrada.fecha;

			// Asignamos la emocion correspondiente
			switch (entrada.emocion){
				case "Alegre":
					IconoVer.transform.Find("Emocion").gameObject.SetActive(true);
					break;
				case "Contento":
					IconoVer.transform.Find("Emocion1").gameObject.SetActive(true);
					break;
				case "Bien":
					IconoVer.transform.Find("Emocion2").gameObject.SetActive(true);
					break;
				case "Regular":
					IconoVer.transform.Find("Emocion3").gameObject.SetActive(true);
					break;
				case "Triste":
					IconoVer.transform.Find("Emocion4").gameObject.SetActive(true);
					break;
				case "Desolado":
					IconoVer.transform.Find("Emocion5").gameObject.SetActive(true);
					break;
			}

			// Asignamos acciones a los botones
			botonVer.onClick.AddListener(() => OnEntryClicked(entrada.id));
			botonVer.onClick.AddListener(() => OnEntryClicked2());
			botonBorrar.onClick.AddListener(() => OnEntryDeleted(entrada.id, entradaGO));
		}
	}

	void OnEntryClicked(string entradaId)
	{
		ControladorEntradaDiario.idEntradaActual = entradaId;
	}

	void OnEntryClicked2()
	{
		StartCoroutine(transicionEscena.CambiarEscena(escenaSig, newMusicClip, duracionFade));
	}

	void OnEntryDeleted(string entradaId, GameObject entradaGO)
	{
		// Eliminamos la entrada del archivo
		controladorDiario.EliminarEntrada(entradaId);

		// Eliminamos el prefab de la lista
		Destroy(entradaGO);
	}
}
