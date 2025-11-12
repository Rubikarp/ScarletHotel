using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericClientSpawner : MonoBehaviour
{

    [SerializeField]
    private ClientCardData scandalClient;
    [SerializeField]
    private ClientCardData luxeClient;
    [SerializeField]
    private ClientCardData susClient;
    [SerializeField]
    private ClientCardData mythClient;
    public enum ClientType
    {
        Scandal,
        Luxe,
        Suspicion,
        Myth
    }

    public void generateClient()
    {
        float totalWeight = 0;

        totalWeight = ScoresManager.Instance.luxeScore + ScoresManager.Instance.mythScore + ScoresManager.Instance.scandalScore + ScoresManager.Instance.susScore;
        
        float randomValue = Random.value * totalWeight;
        Debug.Log(totalWeight);
        if (randomValue > 0)
        {
            if (randomValue < ScoresManager.Instance.luxeScore) 
            {
                //luxeclient
                spawnGenericClient(ClientType.Luxe);

            }
            else if(randomValue < ScoresManager.Instance.luxeScore+ScoresManager.Instance.mythScore)
            {
                //mythclient
                spawnGenericClient(ClientType.Myth);

            }
            else if (randomValue < ScoresManager.Instance.luxeScore + ScoresManager.Instance.mythScore+ScoresManager.Instance.scandalScore)
            {
                //scandalclient
                spawnGenericClient(ClientType.Scandal);

            }
            else if (randomValue < totalWeight)
            {
                //susclient
                spawnGenericClient(ClientType.Suspicion);
            }
    
      
        }

            
        
    }

    void spawnGenericClient(ClientType clientType)
    {
        switch (clientType)
        {
            case ClientType.Scandal:
                CardHandler.Instance.SpawnCard(scandalClient);
                break;
            case ClientType.Luxe:
                CardHandler.Instance.SpawnCard(luxeClient);
                break;
            case ClientType.Suspicion:
                CardHandler.Instance.SpawnCard(susClient);
                break;
            case ClientType.Myth:
                CardHandler.Instance.SpawnCard(mythClient);
                break;
            default:
                break;

        }
    }
}
