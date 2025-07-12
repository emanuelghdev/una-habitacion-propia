using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Burbuja : MonoBehaviour
{
	private Animator animator;
	private bool isPopped = false;
	public AudioClip sonidoPop;
	GameManager gameManager;

	void Start()
	{
		gameManager = FindObjectOfType<GameManager>();
		animator = GetComponent<Animator>();
		GetComponent<Button>().onClick.AddListener(Pop);
	}

	void Pop()
	{
		if (!isPopped)
		{
			isPopped = true;
			animator.SetTrigger("Pop");
			InciarSonidoPop();
			StartCoroutine(DestruirTrasAnimacion());
		}
	}

	void InciarSonidoPop()
	{
		if (sonidoPop != null)
		{
			gameManager.IniciarSonido(sonidoPop);
		}
	}

	IEnumerator DestruirTrasAnimacion()
	{
		// Espera hasta que la animación termine
		yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length*2);
		Destroy(gameObject);
	}
}