using AYellowpaper;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [RequireInterface(typeof(IPeopleCardData)), SerializeField] 
    private ScriptableObject Character;
    [Multiline(3)] public string dialogueText = "Lorem Ipsum ...";

    public string LocutorName
    {
        get
        {
            if (Character == null) return "Unknown";
            return ((IPeopleCardData)Character).CharacterName;
        }
    }
}
