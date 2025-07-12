using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class CuestionamientoSocratico : MonoBehaviour
{
	[Header("General")]
	public DialogosFinal reflexionNarrador;
	public GameObject menuCuestionamientoSocratico;

	[Header("Preguntas")]
	public TMP_Text textoPregunta;
	public TMP_InputField inputFieldRespuesta;
	public Button botonSiguiente;
	
	private Queue<string> colaPreguntas = new Queue<string>();

	void Start()
	{
		botonSiguiente.onClick.AddListener(OnBotonSiguienteClick);
	}

	public void EmpezarCuestionamientoSocratico(int[] userResponses, string[] beliefs)
	{
		menuCuestionamientoSocratico.SetActive(true);

		for (int i = 0; i < userResponses.Length; i++)
		{
			if (userResponses[i] <= 2)
			{
				List<string> questions = GetSocraticQuestionsForBelief(beliefs[i]);
				foreach (var question in questions)
				{
					colaPreguntas.Enqueue(question);
				}
			}
		}

		DisplaySiguientePregunta();
	}

	List<string> GetSocraticQuestionsForBelief(string creencia)
	{
		List<string> questions = new List<string>();

		switch (creencia)
		{
			// Creencias Cuestinamiento Socr�tico I
			case "Siempre estar� solo":
				questions.Add("�Ha habido momentos en tu vida en los que no te has sentido solo?");
				questions.Add("�Podr�a ser que ahora est�s pasando por una fase temporal?");
				questions.Add("�C�mo te sentir�as si descubrieras que esta creencia no es cierta?");
				break;
			case "Si algo puede salir mal, saldr� mal":
				questions.Add("�Qu� pruebas tienes de que siempre que algo puede salir mal, realmente sale mal?");
				questions.Add("�Puedes recordar alguna ocasi�n en la que las cosas salieron bien incluso cuando hab�a una posibilidad de que salieran mal?");
				questions.Add("�Qu� impacto tiene este tipo de pensamiento en c�mo te sientes y act�as?");
				break;
			case "No soporto la incertidumbre":
				questions.Add("�Has enfrentado la incertidumbre en el pasado? �C�mo lo manejaste?");
				questions.Add("�Es cierto que nunca has podido soportar la incertidumbre, o hay momentos en los que lo has hecho?");
				questions.Add("�Qu� podr�as hacer para sentirte m�s c�modo con la incertidumbre?");
				break;
			case "Me siento juzgado todo el tiempo":
				questions.Add("�Qu� pruebas tienes de que las personas realmente te est�n juzgando?");
				questions.Add("�Es posible que est�s interpretando las acciones de los dem�s de manera negativa?");
				questions.Add("�Qu� diferencia har�a en tu vida si dejaras de preocuparte tanto por el juicio de los dem�s?");
				break;
			case "Las cosas malas siempre me pasan a m�":
				questions.Add("�Es cierto que solo te suceden cosas malas, o es posible que est�s ignorando las cosas buenas que tambi�n te suceden?");
				questions.Add("�Qu� pruebas tienes de que las cosas malas te pasan m�s a ti que a otras personas y no es algo com�n a todo el mundo?");
				questions.Add("�Qu� pasar�a si te permitieras pensar que las cosas malas no siempre te ocurren a ti?");
				break;
			case "Si no controlo todo, todo se desmoronar�":
				questions.Add("�Hay momentos en los que has soltado el control y las cosas han salido bien?");
				questions.Add("�Es realista pensar que puedes controlar todo?");
				questions.Add("�C�mo te sentir�as si aceptaras que algunas cosas est�n fuera de tu control y a�n as� pueden salir bien?");
				break;
			case "Me impongo mucha presi�n":
				questions.Add("�De d�nde viene esta necesidad de imponerte tanta presi�n?");
				questions.Add("�Qu� har�as si te permitieras ser m�s compasivo contigo mismo?");
				questions.Add("�C�mo afecta esta presi�n a tu bienestar y rendimiento?�No es contraproducente?");
				break;
			case "Nunca cumplo las expectativas de los dem�s":
				questions.Add("�Es cierto que nunca cumples las expectativas de los dem�s, o est�s subestimando tus logros?");
				questions.Add("�C�mo sabes que las expectativas de los dem�s son razonables o justas?");
				questions.Add("�Qu� significar�a para ti cumplir con tus propias expectativas en lugar de las de los dem�s?");
				break;

			// Creencias Cuestinamiento Socr�tico 2
			case "No soy lo suficientemente bueno":
				questions.Add("�Qu� evidencia tienes para creer que no eres lo suficientemente bueno?");
				questions.Add("�Qu� dir�as a un amigo que se sintiera de esta manera sobre s� mismo?");
				questions.Add("�Puedes pensar en alg�n momento en el que te sentiste competente o capaz?�Qu� pasar�a si dejeras de decirte lo contrario?");
				break;
			case "Siempre fallo en cosas importantes":
				questions.Add("�Hay ejemplos en tu vida donde no fallaste en cosas importantes, a pesar de tus temores?");
				questions.Add("�Qu� oportunidades podr�as estar perdiendo al asumir que siempre fallar�s?");
				questions.Add("�C�mo sabes con certeza que fallar�s en una situaci�n futura en particular?");
				break;
			case "Nadie me entiende":
				questions.Add("�Es posible que haya personas que te comprendan pero no lo hayan expresado?");
				questions.Add("�Podr�as expresar tus sentimientos de una manera que ayude a los dem�s a comprenderte mejor?");
				questions.Add("�Hay ocasiones en las que te has sentido comprendido? �Qu� fue diferente en esas situaciones?");
				break;
			case "Tengo que ser perfecto en todo lo que hago":
				questions.Add("�Es posible que la perfecci�n sea inalcanzable?");
				questions.Add("�C�mo te sentir�as si aceptaras que cometer errores es parte de ser humano?");
				questions.Add("�Qu� beneficios podr�as encontrar en aceptar tus imperfecciones?");
				break;
			case "No puedo manejar el rechazo":
				questions.Add("�Qu� es lo que m�s temes del rechazo?");
				questions.Add("�Es posible que el rechazo no sea tan devastador como piensas?�Qu� podr�as aprender de este?");
				questions.Add("�C�mo ser�a tu vida si dejaras de temer al rechazo y lo vieras como parte del crecimiento personal?");
				break;
			case "No soy digno de amor":
				questions.Add("�Qu� pruebas tienes de que no eres digno de amor?");
				questions.Add("�C�mo responder�as si un amigo te dijera que no es digno de amor?");
				questions.Add("�Podr�a esta creencia estar influenciada por experiencias pasadas que no reflejan tu valor actual?");
				break;
			case "Siempre decepciono a la gente que me importa":
				questions.Add("�Qu� evidencia tienes de que realmente has decepcionado a las personas que te importan?");
				questions.Add("�Qu� crees que esas personas pensar�an si supieran que te sientes de esta manera?");
				questions.Add("�C�mo afectar�a tu relaci�n con esas personas si empezaras a creer que no siempre las decepcionas?");
				break;
			case "Nunca ser� feliz":
				questions.Add("�Qu� significa realmente para ti ser feliz? �Es algo que puede cambiar con el tiempo?");
				questions.Add("�Podr�a esta creencia estar impidi�ndote disfrutar de las cosas peque�as?");
				questions.Add("�C�mo podr�as empezar a cambiar tu enfoque para buscar y apreciar momentos de alegr�a, por peque�os que sean?");
				break;
		}

		return questions;
	}

	void DisplaySiguientePregunta()
	{
		if (colaPreguntas.Count > 0)
		{
			textoPregunta.text = colaPreguntas.Dequeue();
			inputFieldRespuesta.text = "";					// Limpiamos el campo de entrada
		}
		else
		{
			reflexionNarrador.OnEmpezarDialogo();
		}
	}

	void OnBotonSiguienteClick()
	{
		// Aqu� podr�as guardar la respuesta del usuario si fuera necesario
		DisplaySiguientePregunta();
	}
}
