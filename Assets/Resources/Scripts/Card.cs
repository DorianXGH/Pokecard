using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum Type
{
    None,
    Normal,
    Psy,
    Dark,
    Dragon,
    Fighting,
    Ghost,
    Flying,
    Poison,
    Ground,
    Rock,
    Bug,
    Steel,
    Fire,
    Water,
    Grass,
    Electric,
    Ice,
    Fairy
}

[System.Serializable]
public class Stats
{
    public int HP;
    public int Att;
    public int Def;
    public int SpAtt;
    public int SpDef;
    public int Speed;
}

public class Card : MonoBehaviour {
    public string cardName;
    public Stats stats;
    public Type type1;
    public Type type2;
    public int attack1ID;
    public int attack2ID;
    public int rank;
    public Status status;
    public Transform nameObject;
    public Transform attack1Object;
    public Transform attack2Object;
    public Transform rankObject;
    public Transform type1Object;
    public Transform type2Object;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
