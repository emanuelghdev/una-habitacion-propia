using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class BurbujasSpawner : MonoBehaviour
{
	[Header("General")]
	public DialogosFinal reflexionNarrador;

	[Header("Burbujas")]
	public GameObject burbujaPrefab;
	public RectTransform panelTransform;
	public float intervaloSpawn;
	public List<string> pensamientos;
	
	private float timer;
	private float offsetBurbuja = 200f;
	private Queue<string> pensamientosCola;
	private List<GameObject> burbujasActivas = new List<GameObject>();
	private bool puntuado = false;

	void Start()
	{
		// Inicializamos la cola de pensamientos y mezclarla para que no se repitan
		pensamientosCola = new Queue<string>(MezclarLista(pensamientos));
	}

	void Update()
	{

		// Detenemos la generación de burbujas si la cola de pensamientos está vacía
		if (pensamientosCola.Count == 0 && burbujasActivas.Count == 0 && !puntuado)
		{
			Invoke("TerminarSpawnear", 1f);
			puntuado = true;
		}
		else if(pensamientosCola.Count != 0)
		{
			timer += Time.deltaTime;
			if (timer >= intervaloSpawn)
			{
				SpawnBurbuja();
				timer = 0;
			}
		}
	}

	void SpawnBurbuja()
	{
		// Instanciamos la burbuja y la posicionamos aleatoriamente
		float x = Random.Range(offsetBurbuja, panelTransform.rect.width - offsetBurbuja);
		float y = Random.Range(offsetBurbuja, panelTransform.rect.height - offsetBurbuja);

		Vector3 posicion = new Vector3(x, y, 0);

		GameObject burbuja = Instantiate(burbujaPrefab, panelTransform);
		Animator animator = burbuja.GetComponent<Animator>();
		burbuja.GetComponent<Transform>().position = posicion;

		// Escalamos aleatoriamente la burbuja
		float escalaAleatoria = Random.Range(0.7f, 0.9f);
		burbuja.GetComponent<Transform>().localScale = new Vector3(escalaAleatoria, escalaAleatoria, 1);

		// Asignamos un pensamiento aleatorio
		if (pensamientosCola.Count > 0)
		{
			string pensamientoRandom = pensamientosCola.Dequeue();
			burbuja.GetComponentInChildren<TMP_Text>().text = pensamientoRandom;
		}

		// Añadir burbuja a la lista de burbujas activas
		burbujasActivas.Add(burbuja);

		// Si la burbuja tiene el animator
		if (animator != null)
		{
			animator.SetTrigger("Spawn");
		}

		// Agregamos el evento de clic para la burbuja
		burbuja.GetComponent<Button>().onClick.AddListener(() => ExplotarBurbuja(burbuja));
	}

	void ExplotarBurbuja(GameObject burbuja)
	{
		// Removemos la burbuja de la lista de burbujas activas
		burbujasActivas.Remove(burbuja);
	}

	List<string> MezclarLista(List<string> lista)
	{
		System.Random random = new System.Random();
		for (int i = lista.Count - 1; i > 0; i--)
		{
			int indice = random.Next(i + 1);
			string temp = lista[indice];
			lista[indice] = lista[i];
			lista[i] = temp;
		}
		return lista;
	}

	void TerminarSpawnear()
	{
		reflexionNarrador.OnEmpezarDialogo();
	}
}
