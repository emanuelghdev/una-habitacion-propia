using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro; 

public class DiarioUI : MonoBehaviour
{
	public TMP_Dropdown  campoEmocion;
	public TMP_InputField campoTexto;
	public int puntos;
	private ControladorDiario controladorDiario;

	GameManager gameManager;

	void Start()
	{
		controladorDiario = FindObjectOfType<ControladorDiario>();
		gameManager = FindObjectOfType<GameManager>();
	}

	public void OnSaveButtonClicked()
	{
		EntradaDiario nuevaEntrada = new EntradaDiario();
		nuevaEntrada.id = DateTime.Now.ToString("yyyyMMddHHmmss");		// Guardamos como identificador único la fecha y hora de la entrada
		nuevaEntrada.fecha = DateTime.Now.ToString("dd/MM/yyyy");
		nuevaEntrada.emocion = campoEmocion.options[campoEmocion.value].text;
		nuevaEntrada.texto = campoTexto.text;

		controladorDiario.GuardarEntradaDiario(nuevaEntrada);
		gameManager.CompletarActividadDiario(puntos);
	}
}
