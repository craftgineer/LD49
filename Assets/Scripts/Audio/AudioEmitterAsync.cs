using System.Collections;


// Base behaviour class that plays audio on a trigger using a coroutine
public abstract class AudioEmitterAsync : AudioEmitter
{
  // The function to execute on a trigger
  public override void Trigger()
  {
    CoroutineBehaviour.Run(TriggerCoroutine(), $"Trigger for {this}");
  }

  // The coroutine to execute on a trigger
  protected abstract IEnumerator TriggerCoroutine();
}