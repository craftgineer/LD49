using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


// Base class for a set of audio assets
public abstract class AudioAssetSet<T> : ScriptableObject, IEnumerable<T> where T : AudioAsset
{
  // Class that defines an entry
  [Serializable]
  public class Entry
  {
    // The asset of this entry
    public T asset;

    // The asset alias of this entry
    public string assetAlias;


    // Return the name of this entry
    public string Name => !string.IsNullOrEmpty(assetAlias) ? assetAlias : asset.name;
  }


  // The list of audio asset entries
  public List<Entry> entries;


  // Return an asset with the specified index
  public T GetAssetByIndex(int index)
  {
    if (index >= 0 && index < entries.Count)
      return entries[index].asset;
    else
      return null;
  }

  // Return an asset with the specified name
  public T GetAssetByName(string name)
  {
    return entries.Find(entry => entry.Name == name)?.asset;
  }

  // Return a generic enumerator over the music assets
  public IEnumerator<T> GetEnumerator()
  {
    return entries.Select(entry => entry.asset).GetEnumerator();
  }

  // Return an enumerator over the music assets
  IEnumerator IEnumerable.GetEnumerator()
  {
    return GetEnumerator();
  }
}