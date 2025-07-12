using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MantenerAspectRatio : MonoBehaviour
{
	public float orthographicSize = 5f;				// Tamaño ortográfico base
	public float targetAspectRatio = 16f / 9f;      // Relación de aspecto deseada

	void Start()
	{
		AdjustCameraSize();
	}

	void AdjustCameraSize()
	{
		// Obtener la relación de aspecto de la pantalla
		float screenAspect = (float)Screen.width / (float)Screen.height;

		// Ajustar el tamaño ortográfico basado en la relación de aspecto
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
		// Para reflejar cambios en tiempo real durante el modo de edición
		AdjustCameraSize();
	}
}
