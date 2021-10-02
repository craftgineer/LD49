using System;
using UnityEngine;


// Base behaviour class that plays audio on a trigger
public abstract class AudioEmitter : MonoBehaviour
{
  // Enum flags for the condition when the emitter is triggered
  [Flags]
  public enum Condition
  {
    None              = 0,
    ObjectStart       = 1 << 0,
    ObjectDestroy     = 1 << 1,
    CollisionEnter    = 1 << 2,
    CollisionExit     = 1 << 3,
    CollisionEnter2D  = 1 << 4,
    CollisionExit2D   = 1 << 5,
    TriggerEnter      = 1 << 6,
    TriggerExit       = 1 << 7,
    TriggerEnter2D    = 1 << 8,
    TriggerExit2D     = 1 << 9,
  }


  // The trigger condition
  public Condition triggerCondition = Condition.ObjectStart;


  // The function to execute on a trigger
  public abstract void Trigger();

  // Events that trigger the emitter based on the trigger condition
  protected void Start()
  {
    if (triggerCondition.HasFlag(Condition.ObjectStart))
      Trigger();
  }
  protected void OnDestroy()
  {
    if (triggerCondition.HasFlag(Condition.ObjectDestroy))
      Trigger();
  }
  protected void OnCollisionEnter(Collision collision)
  {
    if (triggerCondition.HasFlag(Condition.CollisionEnter))
      Trigger();
  }
  protected void OnCollisionExit(Collision collision)
  {
    if (triggerCondition.HasFlag(Condition.CollisionExit))
      Trigger();
  }
  protected void OnCollisionEnter2D(Collision2D collision)
  {
    if (triggerCondition.HasFlag(Condition.CollisionEnter2D))
      Trigger();
  }
  protected void OnCollisionExit2D(Collision2D collision)
  {
    if (triggerCondition.HasFlag(Condition.CollisionExit2D))
      Trigger();
  }
  protected void OnTriggerEnter(Collider other)
  {
    if (triggerCondition.HasFlag(Condition.TriggerEnter))
      Trigger();
  }
  protected void OnTriggerExit(Collider other)
  {
    if (triggerCondition.HasFlag(Condition.TriggerExit))
      Trigger();
  }
  protected void OnTriggerEnter2D(Collider2D other)
  {
    if (triggerCondition.HasFlag(Condition.TriggerEnter2D))
      Trigger();
  }
  protected void OnTriggerExit2D(Collider2D other)
  {
    if (triggerCondition.HasFlag(Condition.TriggerExit2D))
      Trigger();
  }
}