using UnityEditor;
using UnityEngine;
 
using VInspector;

public class CardHandler : Singleton<CardHandler>
{
    public Card cardPrefab;

    [Button]
    public void AddCard()
    {
        if (Application.isPlaying)
        {
            var card = Instantiate(cardPrefab, transform);
        }
        else
        {
            var card = PrefabUtility.InstantiatePrefab(cardPrefab);
        }
    }

}
