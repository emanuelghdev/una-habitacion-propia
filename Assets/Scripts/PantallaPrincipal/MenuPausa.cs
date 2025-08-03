using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPausa : MonoBehaviour
{
	[Header("General")]
	public GameObject menuPausa;
	public GameObject panelSalir;
	public GameObject panelInventario;
	public GameObject panelMapa;
	public GameObject panelConfig;
	GameManager gameManager;

	[Header("Consistencia Botones")]
	public Button botonGuardar;
	public Button botonGuardarSalir;

	[Header("MenuSalir")]
	public GameObject contenedorSalir;
	public GameObject contenedorConfirmarSalir;

	[Header("Interfaz")]
	public GameObject interfazAQuitar1;
	public GameObject interfazAQuitar2;
	public GameObject interfazAAñadir;

	[Header("Efectos de sonido")]
	public AudioClip clickClip;
	private AudioSource audioSource;

	private void Awake()
	{
		gameManager = FindObjectOfType<GameManager>();
		audioSource = GetComponent<AudioSource>();
	}

	private void Start()
	{
		gameManager = FindObjectOfType<GameManager>();

		botonGuardar.onClick.AddListener(gameManager.GetComponent<ControladorDatosJuego>().GuardarDatos);
		botonGuardar.onClick.AddListener(gameManager.MostrarBarraCarga);

		botonGuardarSalir.onClick.AddListener(gameManager.GetComponent<ControladorDatosJuego>().GuardarDatos);
		botonGuardarSalir.onClick.AddListener(gameManager.MostrarBarraCarga);
	}

	public void Pausa()
	{
		Time.timeScale = 0f;
		menuPausa.SetActive(true);
		interfazAAñadir.SetActive(true);
		interfazAQuitar1.SetActive(false);
		interfazAQuitar2.SetActive(false);
		gameManager.SetVisibilidadCursor(true);
	}

	public void Reanudar()
	{
		Time.timeScale = 1f;
		menuPausa.SetActive(false);
		interfazAAñadir.SetActive(false);
		interfazAQuitar1.SetActive(true);
		interfazAQuitar2.SetActive(true);
		gameManager.SetVisibilidadCursor(false);
		gameManager.pausado = false;
	}

	public void Cerrar()
	{
		Application.Quit();
	}

	// Menu Salir
	public void MenuSalir()
	{
		audioSource.PlayOneShot(clickClip);
		panelSalir.SetActive(true);
		panelInventario.SetActive(false);
		panelMapa.SetActive(false);
		panelConfig.SetActive(false);

	}

	public void onBotonSalir()
	{
		contenedorSalir.SetActive(false);
		contenedorConfirmarSalir.SetActive(true);
	}

	public void onBotonCancelarSalir()
	{
		contenedorSalir.SetActive(true);
		contenedorConfirmarSalir.SetActive(false);
	}

	// Menu Inventario
	public void MenuInventario()
	{
		audioSource.PlayOneShot(clickClip);
		panelInventario.SetActive(true);
		panelMapa.SetActive(false);
		panelConfig.SetActive(false);
		panelSalir.SetActive(false);

	}

	// Menu Mapa
	public void MenuMapa()
	{
		audioSource.PlayOneShot(clickClip);
		panelMapa.SetActive(true);
		panelConfig.SetActive(false);
		panelSalir.SetActive(false);
		panelInventario.SetActive(false);
	}

	// Menu Configuración
	public void MenuConfiguración()
	{
		audioSource.PlayOneShot(clickClip);
		panelConfig.SetActive(true);
		panelSalir.SetActive(false);
		panelInventario.SetActive(false);
		panelMapa.SetActive(false);
	}
}
