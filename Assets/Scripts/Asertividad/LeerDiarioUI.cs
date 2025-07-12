using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class LeerDiarioUI : MonoBehaviour
{
	public TMP_Dropdown campoEmocion;
	public TMP_InputField campoTexto;
	private ControladorDiario controladorDiario;

	void Start()
    {
		controladorDiario = FindObjectOfType<ControladorDiario>();

		string entradaId = ControladorEntradaDiario.idEntradaActual;
		EntradaDiario entrada = controladorDiario.CargarEntradaDiario(entradaId);

		// Buscamos el �ndice de la opci�n que coincide con la emoci�n
		int indice = campoEmocion.options.FindIndex(option => option.text == entrada.emocion);

		// Si se encuentra el �ndice, selecciona esa opci�n en el Dropdown
		if (indice >= 0)
		{
			campoEmocion.value = indice;
		}

		// Establecemos el texto en el campo correspondiente
		campoTexto.text = entrada.texto;
	}
}