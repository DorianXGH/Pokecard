using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum Status
{
    NONE, CURSED, SLEEPING, POISONED, BURNED, FROZEN
}

[System.Serializable]
public class Attack
{
    public string name;
    public Type type;
    public int defenderPower;
    public int attackerPower;
    public Status statusChangeDefender;
    public Status statusChangeAttacker;
    public Stats statChangeDefender;
    public Stats statChangeAttacker;
}
[System.Serializable]
public class TypeDesc
{
    public Type type;
    public float att;
    public float speAtt;
    public float def;
    public float speDef;
}

public class Game : MonoBehaviour {
    public List<Attack> attacks = new List<Attack>();
    public List<GameObject> fightingZone = new List<GameObject>();
    public List<GameObject> cards = new List<GameObject>();
    public List<int> deck1 = new List<int>(); //these are the raw deck data
    public List<int> deck2 = new List<int>();
    public Stack<int> realDeck1 = new Stack<int>(); // these are the decks after the shuffle
    public Stack<int> realDeck2 = new Stack<int>();
    public List<GameObject> hand1 = new List<GameObject>(); // the hands
    public List<GameObject> hand2 = new List<GameObject>();
    public List<GameObject> zone1 = new List<GameObject>(); // the fighting zones
    public List<GameObject> zone2 = new List<GameObject>();
    public List<TypeDesc> typeDescs = new List<TypeDesc>();
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void shuffleDecks()
    {
        Random.InitState((int)Time.time);
        int num1 = deck1.Count;
        for (int i=0; i<num1; i++)
        {
            int cardNum = Mathf.FloorToInt(Random.Range(0f, (float)deck1.Count - 1));
            realDeck1.Push(deck1[cardNum]);
            deck1.RemoveAt(cardNum);
        }
        int num2 = deck1.Count;
        for (int i = 0; i < num2; i++)
        {
            int cardNum = Mathf.FloorToInt(Random.Range(0f, (float)deck2.Count - 1));
            realDeck2.Push(deck2[cardNum]);
            deck2.RemoveAt(cardNum);
        }
    }

    void DrawInDeck(int playerID)
    {
        switch (playerID)
        {
            case 0:
                int cardID = realDeck1.Pop();
                GameObject cd = GameObject.Instantiate(cards[cardID]);
                cd.GetComponent<Card>().pId = playerID;
                hand1.Add(cd);
                break;
            case 1:
                int cardID2 = realDeck2.Pop();
                GameObject cd2 = GameObject.Instantiate(cards[cardID2]);
                cd2.GetComponent<Card>().pId = playerID;
                hand2.Add(cd2);
                break;
            default:
                break;
        }
    }
}
