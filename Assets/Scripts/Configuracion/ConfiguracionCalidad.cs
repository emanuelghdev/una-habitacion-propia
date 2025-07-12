using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConfiguracionCalidad : MonoBehaviour
{
	public TMP_Dropdown calidadesDropdown;
	public int calidad;

    private void Awake()
    {
		calidad = PlayerPrefs.GetInt("CalidadJuego", 5);
		calidadesDropdown.value = calidad;
		AjustarCalidad(calidad);

		calidadesDropdown.onValueChanged.AddListener(AjustarCalidad);
	}

	public void AjustarCalidad(int indice)
	{
		QualitySettings.SetQualityLevel(indice);
		PlayerPrefs.SetInt("CalidadJuego", indice);
		calidad = indice;
	}
}
