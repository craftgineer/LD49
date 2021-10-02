using System.Collections;
using UnityEngine;


// Class that runs a coroutine and destroys itself afterwards
public class CoroutineBehaviour : MonoBehaviour
{
  public static void Run(IEnumerator coroutine, string name = null)
  {
    var coroutineObject = new GameObject(name);
    var coroutineBehaviour = coroutineObject.AddComponent<CoroutineBehaviour>();
    coroutineBehaviour.StartCoroutine(RunCoroutine(coroutineObject, coroutine));
  }

  private static IEnumerator RunCoroutine(GameObject coroutineObject, IEnumerator coroutine) 
  {
    yield return coroutine;
    Destroy(coroutineObject);
  }
}