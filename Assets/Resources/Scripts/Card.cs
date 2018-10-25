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
    public Transform statsObject;
    public Transform Master;
    public Vector3 statsX;

	// Use this for initialization
	void Start () {
        nameObject.gameObject.GetComponent<TMPro.TextMeshPro>().text = cardName; // set the name

        GameObject attackTMP = (GameObject)Resources.Load("Prefabs/AttackName"); // Get the prefab from the resources

        GameObject statTMP = (GameObject)Resources.Load("Prefabs/Statlist"); // Get the prefab from the resources

        // create as many instances as stats and attacks
        GameObject HPObj = Instantiate(statTMP); 
        GameObject AttObj = Instantiate(statTMP);
        GameObject DefObj = Instantiate(statTMP);
        GameObject SpAttObj = Instantiate(statTMP);
        GameObject SpDefObj = Instantiate(statTMP);
        GameObject SpeObj = Instantiate(statTMP);
        GameObject Move1Obj = Instantiate(attackTMP);
        GameObject Move2Obj = Instantiate(attackTMP);

        // make the card their parent
        HPObj.transform.SetParent(transform);
        AttObj.transform.SetParent(transform);
        DefObj.transform.SetParent(transform);
        SpAttObj.transform.SetParent(transform);
        SpDefObj.transform.SetParent(transform);
        SpeObj.transform.SetParent(transform);
        Move1Obj.transform.SetParent(attack1Object);
        Move2Obj.transform.SetParent(attack2Object);

        // Put them into position
        HPObj.transform.localPosition = new Vector3(-statsX.x, statsObject.localPosition.y, 0);
        AttObj.transform.localPosition = new Vector3(-statsX.y, statsObject.localPosition.y, 0);
        DefObj.transform.localPosition = new Vector3(-statsX.z, statsObject.localPosition.y, 0);
        SpAttObj.transform.localPosition = new Vector3(statsX.z, statsObject.localPosition.y, 0);
        SpDefObj.transform.localPosition = new Vector3(statsX.y, statsObject.localPosition.y, 0);
        SpeObj.transform.localPosition = new Vector3(statsX.x, statsObject.localPosition.y, 0);
        Move1Obj.transform.localPosition = Move1Obj.transform.position;
        Move2Obj.transform.localPosition = Move2Obj.transform.position;

        // Set their text to the corresponding stat and move name
        HPObj.GetComponent<TMPro.TextMeshPro>().text = stats.HP.ToString();
        AttObj.GetComponent<TMPro.TextMeshPro>().text = stats.Att.ToString();
        DefObj.GetComponent<TMPro.TextMeshPro>().text = stats.Def.ToString();
        SpAttObj.GetComponent<TMPro.TextMeshPro>().text = stats.SpAtt.ToString();
        SpDefObj.GetComponent<TMPro.TextMeshPro>().text = stats.SpDef.ToString();
        SpeObj.GetComponent<TMPro.TextMeshPro>().text = stats.Speed.ToString();
        Move1Obj.GetComponent<TMPro.TextMeshPro>().text = Master.gameObject.GetComponent<Game>().attacks[attack1ID].name;
        Move2Obj.GetComponent<TMPro.TextMeshPro>().text = Master.gameObject.GetComponent<Game>().attacks[attack2ID].name;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
