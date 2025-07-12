using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;


public class DialogoJugador : MonoBehaviour
{
	public ControladorDialogos controladorDialogos;
	public DialogosNPC dialogoNPC;
	public GameObject panelDialogo;
	public Button botonOpcion1;
	public Button botonOpcion2;
	[TextArea(4, 6)] public string textoOpcion1;
	[TextArea(4, 6)] public string textoOpcion2;
	[TextArea(4, 6)] public string textoConfirmarMision;
	[TextArea(4, 6)] public string textoDenegarMision;

	private Button selectedButton;

	void Start()
	{
		// Desactivar el panel de diálogo al inicio
		panelDialogo.SetActive(false);

		// Configurar los botones con el texto correspondiente
		botonOpcion1.GetComponentInChildren<TMP_Text>().text = "";
		botonOpcion2.GetComponentInChildren<TMP_Text>().text = "";
	}

	void Update()
	{
		if (panelDialogo.activeSelf)
		{
			// Mover la selección con las flechas del teclado
			if (Input.GetKeyDown(KeyCode.RightArrow))
			{
				if (selectedButton == botonOpcion1)
				{
					DeselecccionarBoton(botonOpcion1);
					SelecccionarBoton(botonOpcion2);
				}
				else
				{
					DeselecccionarBoton(botonOpcion2);
					SelecccionarBoton(botonOpcion1);
				}
			}
			else if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				if (selectedButton == botonOpcion2)
				{
					DeselecccionarBoton(botonOpcion2);
					SelecccionarBoton(botonOpcion1);
				}
				else
				{
					DeselecccionarBoton(botonOpcion1);
					SelecccionarBoton(botonOpcion2);
				}
			}

			if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
			{
				// Confirmar la selección del botón actualmente seleccionado
				if (selectedButton != null)
				{
					selectedButton.onClick.Invoke();
				}
			}
		}
	}

	public void MostrarDialogo()
	{
		botonOpcion1.GetComponentInChildren<TMP_Text>().text = textoOpcion1;
		botonOpcion2.GetComponentInChildren<TMP_Text>().text = textoOpcion2;

		DeselecccionarBoton(botonOpcion1);
		DeselecccionarBoton(botonOpcion2);

		botonOpcion1.onClick.RemoveAllListeners();
		botonOpcion2.onClick.RemoveAllListeners();

		// Configurar el evento de clic para cada botón
		botonOpcion1.onClick.AddListener(() => FinalizarDialogo(botonOpcion1));
		botonOpcion2.onClick.AddListener(() => FinalizarDialogo(botonOpcion2));

		panelDialogo.SetActive(true);
		dialogoNPC.puedeHablar = false;
		//SelectButton(botonOpcion1);			// Seleccionar el primer botón por defecto
	}

	public void MostrarDialogoMision()
	{
		botonOpcion1.GetComponentInChildren<TMP_Text>().text = textoConfirmarMision;
		botonOpcion2.GetComponentInChildren<TMP_Text>().text = textoDenegarMision;

		DeselecccionarBoton(botonOpcion1);
		DeselecccionarBoton(botonOpcion2);

		botonOpcion1.onClick.RemoveAllListeners();
		botonOpcion2.onClick.RemoveAllListeners();

		botonOpcion1.onClick.AddListener(() => ConfirmarMision(botonOpcion1));
		botonOpcion2.onClick.AddListener(() => ConfirmarMision(botonOpcion2));

		panelDialogo.SetActive(true);
		dialogoNPC.puedeHablar = false;
	}

	private void SelecccionarBoton(Button button)
	{
		if (button != null)
		{
			button.GetComponent<Image>().sprite = button.spriteState.selectedSprite;
			selectedButton = button;
		}
	}

	private void DeselecccionarBoton(Button button)
	{
		if (button != null)
		{
			button.GetComponent<Image>().sprite = button.spriteState.highlightedSprite;
			selectedButton = null;
		}
	}


	private void FinalizarDialogo(Button selectedOption)
	{
		panelDialogo.SetActive(false);
		dialogoNPC.EmpezarDialogo();
		dialogoNPC.puedeHablar = true;
	}

	private void ConfirmarMision(Button selectedOption)
	{
		panelDialogo.SetActive(false);
		controladorDialogos.turnoJugador = false;
		controladorDialogos.turnoResponderMision = false;
		dialogoNPC.puedeHablar = true;
		dialogoNPC.jugador.GetComponent<MovimientoTopDown>().enabled = true;

		if (selectedOption == botonOpcion1)
		{
			controladorDialogos.ConfirmarMision();
		}
	}
}

