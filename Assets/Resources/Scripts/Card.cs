using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum Type
{
    None,
    Mecanical,
    Biological,
    Elemental,
    Mystical
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

[System.Serializable]
public enum Zone
{
    HAND, FIGHTINGZONE
}

public class Card : MonoBehaviour {
    public string cardName;
    public Stats stats;
    public Type type1;
    public Type type2;
    public int attack1ID;
    public int attack2ID;
    public Zone zone;
    public int rank;
    public int level;
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
    public int pId;
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
    void Summon()
    {
        zone = Zone.FIGHTINGZONE;
        Game g = Master.gameObject.GetComponent<Game>();
        
        switch (pId) // move the card from zone, it will be moved graphically by the master element
        {
            case 0:
                g.hand1.Remove(gameObject);
                g.zone1.Add(gameObject);
                break;
            case 1:
                g.hand2.Remove(gameObject);
                g.zone2.Add(gameObject);
                break;
            default:
                break;
        }
    }
    void OnClick()
    {
        if (zone == Zone.HAND)
        {
            Summon();
        }
    }
    void AttackCard(int attackNum, GameObject card2)
    {
        Game g = Master.gameObject.GetComponent<Game>();
        int attackID = (attackNum == 0)?attack1ID:attack2ID; // fonction qui à 0 associe l'id de la première attaque et à 1 celle de la deuxième
        Attack att = g.attacks[attackID];
        Card cd2 = card2.GetComponent<Card>();
        bool STAB = (att.type == type1) || (att.type == type2);
        int effectiveAtt = 1;
        int effectiveDef = 1;
        int effectiveDefAttacker = 1;
        for (int i=0; i <g.typeDescs.Count; i++) // apply type specificity
        {
            if (g.typeDescs[i].type == att.type)
            {
                TypeDesc tDesc = g.typeDescs[i];
                effectiveAtt = Mathf.CeilToInt(tDesc.att * stats.Att + tDesc.speAtt * stats.SpAtt);
                effectiveDef = Mathf.CeilToInt(tDesc.def * cd2.stats.Def + tDesc.speDef * cd2.stats.SpDef);
                effectiveDefAttacker = Mathf.CeilToInt(tDesc.def * stats.Def + tDesc.speDef * stats.SpDef);
            }
        }
        // compute damages
        int HPtoLose = Mathf.CeilToInt((effectiveAtt + 1f) * att.defenderPower * ((STAB) ? 1.5f : 1f) / effectiveDef);
        int HPtoLoseAttacker = Mathf.CeilToInt((effectiveAtt + 1f) * att.attackerPower * ((STAB) ? 1.5f : 1f) / effectiveDefAttacker);
        // apply them (checks for death will be done in the update callback)
        cd2.stats.HP -= HPtoLose;
        stats.HP -= HPtoLoseAttacker;
    }
}
