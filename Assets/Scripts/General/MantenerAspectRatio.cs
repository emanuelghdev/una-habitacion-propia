using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MantenerAspectRatio : MonoBehaviour
{
	public float orthographicSize = 5f;				// Tama�o ortogr�fico base
	public float targetAspectRatio = 16f / 9f;      // Relaci�n de aspecto deseada

	void Start()
	{
		AdjustCameraSize();
	}

	void AdjustCameraSize()
	{
		// Obtener la relaci�n de aspecto de la pantalla
		float screenAspect = (float)Screen.width / (float)Screen.height;

		// Ajustar el tama�o ortogr�fico basado en la relaci�n de aspecto
		if (screenAspect > targetAspectRatio || screenAspect < targetAspectRatio)
		{
			float differenceInSize = targetAspectRatio / screenAspect;
			Camera.main.orthographicSize = orthographicSize * differenceInSize;
		}
		else
		{
			Camera.main.orthographicSize = orthographicSize;
		}
	}

	void Update()
	{
		// Para reflejar cambios en tiempo real durante el modo de edici�n
		AdjustCameraSize();
	}
}
