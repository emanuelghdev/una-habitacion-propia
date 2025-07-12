using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
	public GameObject contenedorTutoriales;
	public GameObject[] panelesTutorial;
	private int indiceActualPanel = 0; 
	public float tiempoPanelDisplay = 5f;

	GameManager gameManager;

	void Start()
	{
		gameManager = FindObjectOfType<GameManager>();

		// Verificar si el tutorial ya ha sido mostrado
		if (!PlayerPrefs.HasKey("TutorialBotones") && PlayerPrefs.GetInt("TutorialBotones", 0) != 1 && gameManager.tutorialHabilitado == true)
		{
			PlayerPrefs.SetInt("TutorialBotones", 1);	// Marcar que el tutorial ha sido mostrado
			PlayerPrefs.Save();

			contenedorTutoriales.SetActive(true);

			// Iniciar el tutorial mostrando el primer panel
			StartCoroutine(MostrarPanelSecuencialmente());
		}
		else
		{
			contenedorTutoriales.SetActive(false);
		}
	}

	// Corrutina para mostrar los paneles secuencialmente
	private IEnumerator MostrarPanelSecuencialmente()
	{
		while (indiceActualPanel < panelesTutorial.Length)
		{
			// Activar el panel actual
			panelesTutorial[indiceActualPanel].SetActive(true);

			// Esperar el tiempo determinado
			yield return new WaitForSecondsRealtime(tiempoPanelDisplay);

			// Desactivar el panel actual
			panelesTutorial[indiceActualPanel].SetActive(false);

			// Pasar al siguiente panel
			indiceActualPanel++;
		}
	}
}
