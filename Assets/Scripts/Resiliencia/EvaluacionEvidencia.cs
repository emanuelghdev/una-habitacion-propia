using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EvaluacionEvidencia : MonoBehaviour
{
	[Header("General")]
	public GameObject preguntaCreencia;
	public TMP_InputField inputFieldCreencia;
	public TMP_Text textoCreencia;
	public GameObject cuestionamientoSocratico;
	public DialogosFinal reflexionNarrador;
	public Button botonConfirmar;				// Boton para confirmar la creencia, con la que empezar el cuestionamiento socrático

	[Header("Preguntas")]
	public TextMeshProUGUI textoPregunta;
	public TMP_InputField inputFieldRespuesta;
	public Button botonSiguiente;

	[Header("IU")]
	public float escaladoCreencia = 0.5f;
	public Vector3 posicionCreencia;

	public List<string> preguntas;
	private int actualIndicePregunta = 0;
	private List<string> respuestasUsuario = new List<string>();

	void Start()
	{
		InicializarPreguntasSocraticas();
		botonConfirmar.onClick.AddListener(OnBotonConfirmarClick);
		botonSiguiente.onClick.AddListener(OnBotonSiguienteClick);
	}

	void EmpezarActividad()
	{
		cuestionamientoSocratico.SetActive(true);
		SetCreenciaVisual();
		MostrarPregunta();
	}

	void SetCreenciaVisual()
	{
		preguntaCreencia.transform.localScale = new Vector3(escaladoCreencia, escaladoCreencia, 1);
		preguntaCreencia.transform.position = posicionCreencia;
		inputFieldCreencia.readOnly = true;
		textoCreencia.enableAutoSizing = true;

		botonConfirmar.gameObject.SetActive(false);
	}

	void MostrarPregunta()
	{
		if (actualIndicePregunta < preguntas.Count)
		{
			textoPregunta.text = preguntas[actualIndicePregunta];
			inputFieldRespuesta.text = "";
		}
		else
		{
			preguntaCreencia.SetActive(false);

			// Reflexion final
			reflexionNarrador.OnEmpezarDialogo();
		}
	}

	void OnBotonConfirmarClick()
	{
		string inputText = inputFieldCreencia.text.Trim();	// Elimina los espacios en blanco al inicio y al final

		// Verifica si el campo está vacío
		if (string.IsNullOrEmpty(inputText))
		{
			return;
		}

		EmpezarActividad();
	}

	void OnBotonSiguienteClick()
	{
		respuestasUsuario.Add(inputFieldRespuesta.text);
		actualIndicePregunta++;
		MostrarPregunta();
	}

	void InicializarPreguntasSocraticas()
	{
		// Examinar las pruebas
		preguntas.Add("¿Qué pruebas hay a favor de este sentimiento?");
		preguntas.Add("¿Ha ocurrido anteriormente o en un tiempo pasado esto que piensas?");
		preguntas.Add("¿Conoces a otras personas que les ha ocurrido algo parecido?¿Qué le dirías a un amigo que te dijera que piensa eso?");
		preguntas.Add("¿Estás basando tus pensamientos en estados de ánimos o emociones?");
		preguntas.Add("¿Realmente esto que ocurre depende de ti?");

		// Explorar si hay opciones de pensamientos alternativos 
		preguntas.Add("¿Es probable que no estés interpretando de forma adecuada esta situación?");
		preguntas.Add("¿Existe alguna explicación alternativa por descabellada que te parezca?");
		preguntas.Add("¿Hay otro modo de enfocarlo?");
		preguntas.Add("¿Es posible que estés pensando en términos de «todo o nada»?");

		// Valorar las consecuencias de mantener las creencias
		preguntas.Add("¿Cómo te hace sentir pensar eso?");
		preguntas.Add("¿Pensar así te ayuda a estar más cerca de lo que quieres lograr o a solucionar el problema que tienes?");
		preguntas.Add("¿Pensar así te ayuda a sentirte como te gustaría?");
		preguntas.Add("¿Pensar de esa manera te ayuda a relacionarte satisfactoriamente con los demás?");

		// Plantearse qué ocurriría si eso tan negativo que se piensa es real y cómo actuar entonces
		preguntas.Add("¿Qué ocurriría si esto que piensas acabara ocurriendo?¿Qué es lo peor que podría suceder?");
		preguntas.Add("¿Sería realmente grave o solo un contratiempo pasajero?");
		preguntas.Add("Si esto le ocurriera a otro, ¿qué le dirías para que pudiera afrontarlo?");
		preguntas.Add("¿Realmente esto es importante para ti? ¿Tan importante es en tu vida?");
	}
}
