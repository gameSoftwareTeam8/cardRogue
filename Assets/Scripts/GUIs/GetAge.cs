using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class GetAge : MonoBehaviour
{
    public GameObject destoryTarget;
    public GameObject leftCard;
    public GameObject checkButton;
    public float ageValue;
    public float onOffValue;
    public Material material;

    public float ageInc = 0.5f;
    public float maxAge = 13f;
    private float delay = 0.1f;

    private ShelterCardSelectionHandler cardSelection;


    void Start()
    {
        ageValue = material.GetFloat("_Age");
        onOffValue = material.GetFloat("_OnOff");
        material.SetFloat("_OnOff", 0);

    }
    

    public void StartFlipbook()
    {
        StartCoroutine(AnimateFlipbook());
        Button buttonComponent = checkButton.GetComponent<Button>();

        if (buttonComponent != null)
        {
            Destroy(buttonComponent);
        }
    }

    IEnumerator AnimateFlipbook()
    {
        if (destoryTarget.name == "HealCard")
        {
            // yield return new WaitForSeconds(1.0f);
        }
           
        else if (destoryTarget.name == "DeckRemoveCard")
        {
            // yield return new WaitForSeconds(1.5f);
        }
            
       
        material.SetFloat("OnOff", 1);
        Color color = material.color;
        color.a = 1f; 
        material.color = color;
        while(ageValue < maxAge)
        {
            ageValue += ageInc;
            ageValue = Mathf.Min(ageValue, maxAge);
            material.SetFloat("_Age", ageValue);
            yield return new WaitForSeconds(delay);
        }
        
        StartCoroutine(WaitTime());
        
            
    }
    IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(0.03f);
        destoryTarget.transform.position = new Vector3(10000, 0, 0);
        material.SetFloat("_Age", 7);
        if(destoryTarget.name == "DeckRemoveCard")
            DisplayDeckToElimination();
    }

    void DisplayDeckToElimination()
    {
        DeckEliminator deckEliminator = GetComponent<DeckEliminator>();
        deckEliminator.DisplayCards();
    }
}