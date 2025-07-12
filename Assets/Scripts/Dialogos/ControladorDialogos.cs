using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorDialogos : MonoBehaviour
{
	[Header("General")]
	public DialogosNPC dialogoNPC;
	public DialogoJugador dialogoJugador;
	public List<string> condiciones = new List<string>();
	public bool turnoJugador;
	public bool turnoResponderMision;

	[Header("Texto Dialogos")]
	[SerializeField, TextArea(4, 6)] private string[] lineasDialogoInicial;
	[SerializeField, TextArea(4, 6)] private string[] lineasDialogoSegundaVez;
	[SerializeField, TextArea(4, 6)] private string[] lineasDialogoProblema;
	[SerializeField, TextArea(4, 6)] private string[] lineasDialogoProblemaSolucionado;
	[SerializeField, TextArea(4, 6)] private string[] lineasDialogoMision;
	[SerializeField, TextArea(4, 6)] private string[] lineasDialogoEsperandoMision;
	[SerializeField, TextArea(4, 6)] private string[] lineasDialogoEntregarMision;
	[SerializeField, TextArea(4, 6)] private string[] lineasDialogoMisionCompletada;
	[SerializeField, TextArea(4, 6)] private string[] lineasDialogoFinal;

	[Header("Mision")]
	public string idObjetoSolicitado;
	public int cantidadObjetoSolicitado;
	public TransicionEscena transicionEscena;

	GameManager gameManager;
	private string nombreNPC;


	private void Start()
	{
		gameManager = FindObjectOfType<GameManager>();
		nombreNPC = dialogoNPC.nombrePj;

		InicializarCondiciones();
	}

	public string[] ObtenerLineasDialogo()
	{
		string[] lineasDialogo;

		if (PlayerPrefs.GetInt(condiciones[0], 0) == 0)
		{
			lineasDialogo = lineasDialogoInicial;

			// Marcar como hablado por primera vez
			PlayerPrefs.SetInt(condiciones[0], 1);
		}
		else if (PlayerPrefs.GetInt(condiciones[1], 0) == 0)
		{
			lineasDialogo = lineasDialogoSegundaVez;

			// Marcar como hablado por segunda vez
			PlayerPrefs.SetInt(condiciones[1], 1);
		}
		else if (PlayerPrefs.GetInt(condiciones[2], 0) == 0)
		{
			lineasDialogo = lineasDialogoProblema;
			turnoJugador = true;

			// Marcar como hablado de los problemas
			PlayerPrefs.SetInt(condiciones[2], 1);
		}
		else if (PlayerPrefs.GetInt(condiciones[3], 0) == 0)
		{
			lineasDialogo = lineasDialogoProblemaSolucionado;

			// Marcar como hablado de la solucion a los problemas
			PlayerPrefs.SetInt(condiciones[3], 1);
		}
		else if (PlayerPrefs.GetInt(condiciones[4], 0) == 0)
		{
			lineasDialogo = lineasDialogoMision;
			ComprobarMision();

			// Marcar como hablado de la mision
			PlayerPrefs.SetInt(condiciones[4], 1);
		}
		else if (PlayerPrefs.GetInt(condiciones[5], 0) == 0)
		{
			lineasDialogo = lineasDialogoEsperandoMision;
			ComprobarMision();
		}
		else if (PlayerPrefs.GetInt(condiciones[5], 0) == 1 && PlayerPrefs.GetInt(condiciones[6], 0) == 0)
		{
			lineasDialogo = lineasDialogoEntregarMision;
			turnoJugador = true;
			CambiarRespuestasJugador();
		}
		else if (PlayerPrefs.GetInt(condiciones[7], 0) == 0)
		{
			lineasDialogo = lineasDialogoMisionCompletada;

			// Marcar como hablado despues de completar la mision
			PlayerPrefs.SetInt(condiciones[7], 1);
		}
		else
		{
			lineasDialogo = lineasDialogoFinal;
		}

		return lineasDialogo;
	}

	public void InicializarCondiciones()
	{
		condiciones[0] = nombreNPC + "PrimeraInteraccion";
		condiciones[1] = nombreNPC + "SegundaInteraccion";
		condiciones[2] = nombreNPC + "ProblemaPlanteado";
		condiciones[3] = nombreNPC + "ProblemaSolucionado";
		condiciones[4] = nombreNPC + "MisionPlanteada";
		condiciones[5] = nombreNPC + "MisionCompletada";
		condiciones[6] = nombreNPC + "MisionFinalizada";
		condiciones[7] = nombreNPC + "PostMision";
	}

	public void HablarJugador()
	{
		if (turnoResponderMision)
		{
			dialogoJugador.MostrarDialogoMision();
		}
		else
		{
			dialogoJugador.MostrarDialogo();
		}
	}

	public void CambiarRespuestasJugador()
	{
		turnoResponderMision = true;
	}

	public void ComprobarMision()
	{
		if (gameManager.TieneObjeto(idObjetoSolicitado, cantidadObjetoSolicitado))
		{
			// Marcar como que se ha completado la mision
			PlayerPrefs.SetInt(condiciones[5], 1);
		}
	}

	public void ConfirmarMision()
	{
		gameManager.EliminarObjeto(idObjetoSolicitado, cantidadObjetoSolicitado);

		// Marcar como finalizada la mision
		PlayerPrefs.SetInt(condiciones[6], 1);

		dialogoNPC.jugador.GetComponent<MovimientoTopDown>().GuardarPosicionJugador();
		StartCoroutine(transicionEscena.CambiarEscena(SceneManager.GetActiveScene().name));
	}
}