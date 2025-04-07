using UnityEngine;
using System.Collections;

public class TestCoroutineStop : MonoBehaviour
{
	private void Start()
	{
		// Запускаем несколько одинаковых корутин
		StartCoroutine("TestCoroutine", 1);
		StartCoroutine("TestCoroutine", 2);
		StartCoroutine("TestCoroutine", 3);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Debug.Log("Пытаемся остановить корутину по имени");
			StopCoroutine("TestCoroutine");
		}
	}

	private IEnumerator TestCoroutine(int id)
	{
		while (true)
		{
			Debug.Log($"Корутина {id} работает");
			yield return new WaitForSeconds(1f);
		}
	}
}