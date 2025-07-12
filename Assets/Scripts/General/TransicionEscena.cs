using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransicionEscena : MonoBehaviour
{
	public Animator animator;
	[SerializeField] private AnimationClip animacionFinal;
	GameManager gameManager;

	void Start()
    {
		gameManager = FindObjectOfType<GameManager>();
	}

    void Update()
    {
        
    }

	public IEnumerator CambiarEscena(string escena, AudioClip newMusicClip = null, float duracionFade = 0)
	{
		animator.SetTrigger("Iniciar");

		// Si hay una nueva música se inicia una corrutina
		if (newMusicClip != null)
		{
			StartCoroutine(gameManager.FadeOutAndIn(newMusicClip, duracionFade));
		}

		gameManager.transicionTerminada = false;
		yield return new WaitForSeconds(animacionFinal.length > duracionFade ? animacionFinal.length : duracionFade);

		SceneManager.LoadScene(escena);
	}

	public void AlTerminarAnimacion()
	{
		if(gameManager.juegoIniciado)
		{
			gameManager.transicionTerminada = true;
		}
	}
}
