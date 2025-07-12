using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CambiarEscenaBoton : MonoBehaviour
{
	[SerializeField] private string escenaSig;
	[SerializeField] private TransicionEscena transicionEscena;
	[SerializeField] private AudioClip newMusicClip;
	[SerializeField] private float duracionFade;
	[SerializeField] private Button boton;

	void Start()
    {
		boton.onClick.AddListener(CambiarAOtraEscena);
	}

    public void CambiarAOtraEscena()
    {
		StartCoroutine(transicionEscena.CambiarEscena(escenaSig, newMusicClip, duracionFade));
	}
}
