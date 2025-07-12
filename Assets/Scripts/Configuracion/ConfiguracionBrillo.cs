using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfiguracionBrillo : MonoBehaviour
{
	public Slider sliderBrillo;
	public GameObject panelBrillo;

	private float valorPredeterminado = 0.75f;
	private float alphaMax = 0.6f;				// 70% de opacidad
	private float alphaMin = 0.0f;				// 0% de opacidad

	private void Awake()
	{
		// Configuramos el valor inicial del slider basado en el brillo guardado
		float brillo = PlayerPrefs.GetFloat("BrilloJuego", valorPredeterminado);
		sliderBrillo.value = brillo;
		SetBrillo(brillo);

		// Añadimos un listener al slider para que el brillo cambie en tiempo real
		sliderBrillo.onValueChanged.AddListener(SetBrillo);
	}

	public void SetBrillo(float brillo)
	{
		// Ajustamos la intensidad de un panel de color negro encima
		Color color = panelBrillo.GetComponent<Image>().color;

		// Convertimos el valor del slider (0 a 1) a un rango de alpha (70% a 0%)
		float alphaValue = Mathf.Lerp(alphaMax, alphaMin, brillo);

		// Asignamos el nuevo valor de alpha al color del panel
		color.a = alphaValue;
		panelBrillo.GetComponent<Image>().color = color;

		// Guardamos el brillo ajustado
		PlayerPrefs.SetFloat("BrilloJuego", brillo);
	}
}
