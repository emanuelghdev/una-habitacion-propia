using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EscenaMundoInitializer : MonoBehaviour
{
	[Header("Dialogos")]
	public GameObject dialogoBoxCarteles;
	public TextMeshProUGUI dialogoTextCarteles;

	[Header("Canvas")]
	public GameObject barraCarga;

	[Header("General")]
	public MenuPausa menuPausa;
	public TransicionEscena transicionEscena;
	public GameObject jugador;
	public InventarioUI inventarioUI;
	public TMP_Text puntosUI;
	public TMP_Text puntosUIPausa;
	public TMP_Text nombreJugador;
	public Cartel cartelCasaJugador;

	[Header("Configuracion")]
	public Slider sliderVolumen;
	public Slider sliderBrillo;
	public GameObject panelBrillo;
	public Toggle togglePantallaCompleta;
	public TMP_Dropdown resolucionesDropdown;
	public TMP_Dropdown calidadesDropdown;

	GameManager gameManager;
	ControladorDatosJuego controladorDatosJuego;

	void Start()
	{
		gameManager = FindObjectOfType<GameManager>();
		gameManager.SetVisibilidadCursor(false);

		controladorDatosJuego = FindObjectOfType<ControladorDatosJuego>();
		jugador = GameObject.FindGameObjectWithTag("Player");

		if (GameManager.instance != null)
		{
			GameManager.instance.dialogoBoxCarteles = dialogoBoxCarteles;
			GameManager.instance.dialogoTextCarteles = dialogoTextCarteles;

			GameManager.instance.barraCarga = barraCarga;

			GameManager.instance.menuPausa = menuPausa;
			GameManager.instance.transicionEscena = transicionEscena;

			controladorDatosJuego.jugador = jugador;

			puntosUI.text = GameManager.instance.PB.ToString();
			puntosUIPausa.text = GameManager.instance.PB.ToString();
			nombreJugador.text = PlayerPrefs.GetString("NombrePersonaje", "");
			cartelCasaJugador.text = PlayerPrefs.GetString("CasaJugador", "Se vende");

			GameManager.instance.GetComponent<ConfiguracionVolumen>().sliderVolumen = sliderVolumen;
			GameManager.instance.GetComponent<ConfiguracionVolumen>().sliderVolumen.onValueChanged.AddListener(GameManager.instance.GetComponent<ConfiguracionVolumen>().SetVolumen);

			GameManager.instance.GetComponent<ConfiguracionBrillo>().sliderBrillo = sliderBrillo;
			GameManager.instance.GetComponent<ConfiguracionBrillo>().panelBrillo = panelBrillo;
			GameManager.instance.GetComponent<ConfiguracionBrillo>().sliderBrillo.onValueChanged.AddListener(GameManager.instance.GetComponent<ConfiguracionBrillo>().SetBrillo);

			GameManager.instance.GetComponent<ConfiguracionPantalla>().toggle = togglePantallaCompleta;
			GameManager.instance.GetComponent<ConfiguracionPantalla>().resolucionesDropdown = resolucionesDropdown;
			GameManager.instance.GetComponent<ConfiguracionPantalla>().resolucionesDropdown.onValueChanged.AddListener(GameManager.instance.GetComponent<ConfiguracionPantalla>().CambiarResolucion);
			GameManager.instance.GetComponent<ConfiguracionPantalla>().toggle.onValueChanged.AddListener(GameManager.instance.GetComponent<ConfiguracionPantalla>().ActivarPantallaCompleta);

			GameManager.instance.GetComponent<ConfiguracionCalidad>().calidadesDropdown = calidadesDropdown;
			GameManager.instance.GetComponent<ConfiguracionCalidad>().calidadesDropdown.onValueChanged.AddListener(GameManager.instance.GetComponent<ConfiguracionCalidad>().AjustarCalidad);

			// Actualizamos el inventario
			inventarioUI.UpdateInventorioUI();
		}
	}
}
