using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AbrirCofre : MonoBehaviour
{
	[Header("Objeto")]
	public Objeto objetoDentroDelCofre;				// El objeto que recibirá el jugador
	private bool jugadorCerca = false;
	private Animator animator;
	public AudioClip audioCofre;

	[Header("Recargar Escena")]
	public GameObject jugador;
	public TransicionEscena transicionEscena;

	private bool yaAbierto;

	GameManager gameManager;

	void Start()
	{
		animator = GetComponent<Animator>();
		gameManager = FindObjectOfType<GameManager>();

		// Recuperar el estado del cofre
		yaAbierto = PlayerPrefs.GetInt("Cofre_" + gameObject.name, 0) == 1;

		if (yaAbierto)
		{
			jugador.GetComponent<MovimientoTopDown>().enabled = true;
			gameObject.SetActive(false);
		}
	}

	void Update()
	{
		// Si el jugador está cerca y presiona la tecla E
		if (jugadorCerca && Input.GetKeyDown(KeyCode.E) && !yaAbierto)
		{
			Abrir();
		}
	}

	private void Abrir()
	{
		// Dispara la animación de apertura
		animator.SetBool("Abrir", true);

		jugador.GetComponent<MovimientoTopDown>().enabled = false;

		gameManager.AñadirObjeto(objetoDentroDelCofre.idObjeto, objetoDentroDelCofre.nombreIconoObjeto, objetoDentroDelCofre.cantidadObjeto);
		yaAbierto = true;

		// Guardar el estado del cofre
		PlayerPrefs.SetInt("Cofre_" + gameObject.name, yaAbierto ? 1 : 0);

		StartCoroutine(TerminarAnimacion());
	}

	private IEnumerator TerminarAnimacion()
	{
		gameManager.IniciarSonido(audioCofre);
		yield return new WaitForSeconds(1f);

		jugador.GetComponent<MovimientoTopDown>().GuardarPosicionJugador();
		StartCoroutine(transicionEscena.CambiarEscena(SceneManager.GetActiveScene().name));
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			jugadorCerca = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			jugadorCerca = false;
		}
	}
}
