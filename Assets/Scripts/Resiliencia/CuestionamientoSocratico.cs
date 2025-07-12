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
			// Creencias Cuestinamiento Socrático I
			case "Siempre estaré solo":
				questions.Add("¿Ha habido momentos en tu vida en los que no te has sentido solo?");
				questions.Add("¿Podría ser que ahora estés pasando por una fase temporal?");
				questions.Add("¿Cómo te sentirías si descubrieras que esta creencia no es cierta?");
				break;
			case "Si algo puede salir mal, saldrá mal":
				questions.Add("¿Qué pruebas tienes de que siempre que algo puede salir mal, realmente sale mal?");
				questions.Add("¿Puedes recordar alguna ocasión en la que las cosas salieron bien incluso cuando había una posibilidad de que salieran mal?");
				questions.Add("¿Qué impacto tiene este tipo de pensamiento en cómo te sientes y actúas?");
				break;
			case "No soporto la incertidumbre":
				questions.Add("¿Has enfrentado la incertidumbre en el pasado? ¿Cómo lo manejaste?");
				questions.Add("¿Es cierto que nunca has podido soportar la incertidumbre, o hay momentos en los que lo has hecho?");
				questions.Add("¿Qué podrías hacer para sentirte más cómodo con la incertidumbre?");
				break;
			case "Me siento juzgado todo el tiempo":
				questions.Add("¿Qué pruebas tienes de que las personas realmente te están juzgando?");
				questions.Add("¿Es posible que estés interpretando las acciones de los demás de manera negativa?");
				questions.Add("¿Qué diferencia haría en tu vida si dejaras de preocuparte tanto por el juicio de los demás?");
				break;
			case "Las cosas malas siempre me pasan a mí":
				questions.Add("¿Es cierto que solo te suceden cosas malas, o es posible que estés ignorando las cosas buenas que también te suceden?");
				questions.Add("¿Qué pruebas tienes de que las cosas malas te pasan más a ti que a otras personas y no es algo común a todo el mundo?");
				questions.Add("¿Qué pasaría si te permitieras pensar que las cosas malas no siempre te ocurren a ti?");
				break;
			case "Si no controlo todo, todo se desmoronará":
				questions.Add("¿Hay momentos en los que has soltado el control y las cosas han salido bien?");
				questions.Add("¿Es realista pensar que puedes controlar todo?");
				questions.Add("¿Cómo te sentirías si aceptaras que algunas cosas están fuera de tu control y aún así pueden salir bien?");
				break;
			case "Me impongo mucha presión":
				questions.Add("¿De dónde viene esta necesidad de imponerte tanta presión?");
				questions.Add("¿Qué harías si te permitieras ser más compasivo contigo mismo?");
				questions.Add("¿Cómo afecta esta presión a tu bienestar y rendimiento?¿No es contraproducente?");
				break;
			case "Nunca cumplo las expectativas de los demás":
				questions.Add("¿Es cierto que nunca cumples las expectativas de los demás, o estás subestimando tus logros?");
				questions.Add("¿Cómo sabes que las expectativas de los demás son razonables o justas?");
				questions.Add("¿Qué significaría para ti cumplir con tus propias expectativas en lugar de las de los demás?");
				break;

			// Creencias Cuestinamiento Socrático 2
			case "No soy lo suficientemente bueno":
				questions.Add("¿Qué evidencia tienes para creer que no eres lo suficientemente bueno?");
				questions.Add("¿Qué dirías a un amigo que se sintiera de esta manera sobre sí mismo?");
				questions.Add("¿Puedes pensar en algún momento en el que te sentiste competente o capaz?¿Qué pasaría si dejeras de decirte lo contrario?");
				break;
			case "Siempre fallo en cosas importantes":
				questions.Add("¿Hay ejemplos en tu vida donde no fallaste en cosas importantes, a pesar de tus temores?");
				questions.Add("¿Qué oportunidades podrías estar perdiendo al asumir que siempre fallarás?");
				questions.Add("¿Cómo sabes con certeza que fallarás en una situación futura en particular?");
				break;
			case "Nadie me entiende":
				questions.Add("¿Es posible que haya personas que te comprendan pero no lo hayan expresado?");
				questions.Add("¿Podrías expresar tus sentimientos de una manera que ayude a los demás a comprenderte mejor?");
				questions.Add("¿Hay ocasiones en las que te has sentido comprendido? ¿Qué fue diferente en esas situaciones?");
				break;
			case "Tengo que ser perfecto en todo lo que hago":
				questions.Add("¿Es posible que la perfección sea inalcanzable?");
				questions.Add("¿Cómo te sentirías si aceptaras que cometer errores es parte de ser humano?");
				questions.Add("¿Qué beneficios podrías encontrar en aceptar tus imperfecciones?");
				break;
			case "No puedo manejar el rechazo":
				questions.Add("¿Qué es lo que más temes del rechazo?");
				questions.Add("¿Es posible que el rechazo no sea tan devastador como piensas?¿Qué podrías aprender de este?");
				questions.Add("¿Cómo sería tu vida si dejaras de temer al rechazo y lo vieras como parte del crecimiento personal?");
				break;
			case "No soy digno de amor":
				questions.Add("¿Qué pruebas tienes de que no eres digno de amor?");
				questions.Add("¿Cómo responderías si un amigo te dijera que no es digno de amor?");
				questions.Add("¿Podría esta creencia estar influenciada por experiencias pasadas que no reflejan tu valor actual?");
				break;
			case "Siempre decepciono a la gente que me importa":
				questions.Add("¿Qué evidencia tienes de que realmente has decepcionado a las personas que te importan?");
				questions.Add("¿Qué crees que esas personas pensarían si supieran que te sientes de esta manera?");
				questions.Add("¿Cómo afectaría tu relación con esas personas si empezaras a creer que no siempre las decepcionas?");
				break;
			case "Nunca seré feliz":
				questions.Add("¿Qué significa realmente para ti ser feliz? ¿Es algo que puede cambiar con el tiempo?");
				questions.Add("¿Podría esta creencia estar impidiéndote disfrutar de las cosas pequeñas?");
				questions.Add("¿Cómo podrías empezar a cambiar tu enfoque para buscar y apreciar momentos de alegría, por pequeños que sean?");
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
		// Aquí podrías guardar la respuesta del usuario si fuera necesario
		DisplaySiguientePregunta();
	}
}
