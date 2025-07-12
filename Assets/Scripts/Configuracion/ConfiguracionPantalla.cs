using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConfiguracionPantalla : MonoBehaviour
{
	public Toggle toggle;
	public TMP_Dropdown resolucionesDropdown;
	Resolution[] resoluciones;

	private void Awake()
	{
		if(Screen.fullScreen)
		{
			toggle.isOn = true;
		}
		else
		{
			toggle.isOn = false;
		}

		RevisarResolucion();

		toggle.onValueChanged.AddListener(ActivarPantallaCompleta);
		resolucionesDropdown.onValueChanged.AddListener(CambiarResolucion);
	}

	public void ActivarPantallaCompleta(bool pantallaCompleta)
	{
		Screen.fullScreen = pantallaCompleta;
	}

	public void RevisarResolucion()
	{
		resoluciones = Screen.resolutions;
		resolucionesDropdown.ClearOptions();
		List<string> opciones = new List<string>();
		int resolucionActual = 0;

		for (int i = 0; i < resoluciones.Length; i++)
		{
			// Creamos un identificador única basada en el widt, el height y el refreshRateRatio de la resolucion
			string opcion = resoluciones[i].width + " x " + resoluciones[i].height + " @ " + resoluciones[i].refreshRateRatio + "Hz";
			opciones.Add(opcion);

			// Comprobar si esta es la resolución actual
			if (Screen.fullScreen && resoluciones[i].width == Screen.currentResolution.width &&
				resoluciones[i].height == Screen.currentResolution.height &&
				resoluciones[i].refreshRateRatio.Equals(Screen.currentResolution.refreshRateRatio))
			{
				resolucionActual = i;
			}
		}

		resolucionesDropdown.AddOptions(opciones);
		resolucionesDropdown.value = PlayerPrefs.GetInt("ResolucionJuego", resolucionActual);
		resolucionesDropdown.RefreshShownValue();
	}

	public void CambiarResolucion(int indiceResolucion)
	{
		PlayerPrefs.SetInt("ResolucionJuego", resolucionesDropdown.value);

		Resolution resolucion = resoluciones[indiceResolucion];
		Screen.SetResolution(resolucion.width, resolucion.height, Screen.fullScreen);
	}
}
