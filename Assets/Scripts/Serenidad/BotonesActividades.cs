using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class BotonesActividades : MonoBehaviour
{
	GameManager gameManager;
	[SerializeField] private  int numActividades;
	[SerializeField] private TipoEjercicio tipoEjercicio;

	void Start()
	{
		gameManager = FindObjectOfType<GameManager>();

		// Comprobamos que el tipo de ejercicio existe
		if (gameManager.ejerciciosCompletados.ContainsKey(tipoEjercicio))
		{
			// Comprobamos si los ejercicios se han realizado, si es asi lo marcamos
			for (int i = 0; i < numActividades; i++)
			{
				string key = tipoEjercicio.ToString() + "_Ejercicio_" + i;
				if (PlayerPrefs.GetInt(key, 0) == 1)						// Si el ejercicio está completado
				{
					// Forma el nombre del botón de ejercicio correspondiente
					string ejercicioName = "Canvas/MenuEjercicios/Espacio/Contenido/Ejercicio" + i;
					string botonName = "Canvas/MenuEjercicios/Espacio/Contenido/Ejercicio" + i + "/Boton";

					// Encuentra el componente y el boton de ejercicio por su nombre
					GameObject componeneteEjercicio = GameObject.Find(ejercicioName);
					GameObject botonEjercicio = GameObject.Find(botonName);

					if (componeneteEjercicio != null && botonEjercicio != null)
					{
						// Encuentra el componente Checkbox_Completado en el botón de ejercicio
						GameObject componente = componeneteEjercicio.transform.Find("Checkbox_Completado").gameObject;

						if (componente != null)
						{
							// Activa el componente y modifica el boton
							componente.SetActive(true);
						}
					}
				}
			}
		}
	}
}
