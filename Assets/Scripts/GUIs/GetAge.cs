using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class GetAge : MonoBehaviour
{
    public GameObject destoryTarget;
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
    }

    IEnumerator AnimateFlipbook()
    {
        if (destoryTarget.name == "HealCard")
        {
            yield return new WaitForSeconds(1.5f);
        }
           
        else if (destoryTarget.name == "DeckRemoveCard")
        {
            yield return new WaitForSeconds(1.5f);
        }
            
       
        material.SetFloat("OnOff", 1);
        Color color = material.color;
        color.a = 1f; // 알파값을 1로 설정하여 원래 상태로 복원합니다.
        material.color = color;
        while(ageValue < maxAge)
        {
            ageValue += ageInc;
            ageValue = Mathf.Min(ageValue, maxAge);
            material.SetFloat("_Age", ageValue);
            yield return new WaitForSeconds(delay);
        }

        if (destoryTarget.name != "DeckRemoveCard")
        {
            Destroy(destoryTarget);
            material.SetFloat("_Age", 7);
        }



        

            
    }
}