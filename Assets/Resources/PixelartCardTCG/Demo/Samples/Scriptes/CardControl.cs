using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardControl : MonoBehaviour
{
     enum State
    {
        evade = 0,
        droped = 1,
        selected = 2
    }


     Vector3 _setPoint;
     State state = State.evade;

    public GameObject Effects;
    public List<GameObject> Templates;
    public List<Sprite> CardShirts;

     GameObject _template;
    GameObject _cardShirt;

    public bool IsBack = true;

    public Vector3 SetPoint
    {
        set { _setPoint = value; }
        get { return _setPoint; }
    }

    private void Start()
    {
        _template = Instantiate(Templates[(int)Random.Range(0,100)%Templates.Count], transform.position, Quaternion.identity) as GameObject;
        _template.transform.SetParent(transform);

        IsBack = true;


        foreach (SpriteRenderer sp in _template.GetComponentsInChildren<SpriteRenderer>())
        {
            sp.sortingOrder *= -1;
        }

            //_template.SetActive(!IsBack);
            //_cardShirt.SetActive(IsBack);

            gameObject.SetActive(false);
        GameObject obj = Instantiate(Effects, transform.position, Quaternion.identity) as GameObject;
        obj.GetComponent<EffectsControl>().sender = gameObject;
    }

    void Update()
    {
        if (state == State.droped)
            transform.position = Vector3.Lerp(transform.position, SetPoint, Time.deltaTime*10);
        if (Vector3.Magnitude(transform.position - SetPoint) < Time.deltaTime * 10)
            state = State.evade;        
    }

    public void OnBurn()
    {
        GameObject obj = Instantiate(Effects, transform.position, Quaternion.identity) as GameObject;
        obj.GetComponent<EffectsControl>().sender = gameObject;
        obj.GetComponent<Animator>().SetTrigger("Burn");
    }

    public void OnDamage()
    {
        GameObject obj = Instantiate(Effects, transform.position, Quaternion.identity) as GameObject;
        obj.GetComponent<EffectsControl>().sender = gameObject;
        obj.GetComponent<Animator>().SetTrigger("Damage");
    }


    public void OnSelect() 
    {
        if (state == State.evade)
            SetPoint = transform.position;        
        state = State.selected;
    }

    public void OnDrop() 
    {
        state = State.droped;
    }

    public void FlipCard() 
    {
        GetComponent<Animator>().SetTrigger("Flip");
    }

    public void OnFlip()
    {
        IsBack = !IsBack;

        foreach (SpriteRenderer sp in _template.GetComponentsInChildren<SpriteRenderer>()) {
            sp.sortingOrder *= -1;
        }


//_template.SetActive(!IsBack);
//_cardShirt.SetActive(IsBack);

    }
}
