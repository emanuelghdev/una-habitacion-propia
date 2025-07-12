using UnityEngine;
using TMPro;

public class GameManagerInitializer : MonoBehaviour
{
	public TransicionEscena transicionEscena;

	void Start()
	{
		if (GameManager.instance != null)
		{
			GameManager.instance.transicionEscena = transicionEscena;
		}
	}
}
