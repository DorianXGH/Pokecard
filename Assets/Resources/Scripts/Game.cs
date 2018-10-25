using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum Status
{
    NONE, PARALYSED, SLEEPING, POISONED, BURNED, FROZEN
}

[System.Serializable]
public class Attack
{
    public string name;
    public int defenderPower;
    public int attackerPower;
    public Status statusChangeDefender;
    public Status statusChangeAttacker;
    public Stats statChangeDefender;
    public Stats statChangeAttacker;
}

public class Game : MonoBehaviour {
    public List<Attack> attacks = new List<Attack>();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
