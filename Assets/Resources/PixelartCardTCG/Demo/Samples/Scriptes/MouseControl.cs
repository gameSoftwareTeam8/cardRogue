using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : MonoBehaviour
{
    public GameObject CreateFild;
    public Collider2D DamageFild;
    public Collider2D BurnFild;


    public GameObject CardBlank;

    GameObject objSelected = null;
    Vector3 OffSet = new Vector3();
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            CheckHitObject();

        if (Input.GetMouseButton(0) && objSelected != null)
            DragObject();

        if (Input.GetMouseButtonUp(0) && objSelected != null) 
            DropObject();
    }

    void CheckHitObject()
    {
        RaycastHit2D hit2D = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
        if (hit2D.collider != null)
        {
            if (hit2D.transform.gameObject.GetComponent<CardControl>() != null)
            {
                if (hit2D.transform.gameObject.GetComponent<CardControl>().IsBack)
                {
                    hit2D.transform.gameObject.GetComponent<CardControl>().FlipCard();
                }
                else
                {
                    objSelected = hit2D.transform.gameObject;
                    Vector2 v2temp = objSelected.transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    OffSet = new Vector3(v2temp.x, v2temp.y, 0);
                    objSelected.GetComponent<CardControl>().OnSelect();
                }
            }
            //else if (hit2D.collider.Equals(CreateFild)) 
            //{
            //    GameObject obj = Instantiate(CardBlank, new Vector3(CreateFild.transform.position.x, CreateFild.transform.position.y,0), Quaternion.identity) as GameObject;

            //}
        }
    }

    void DragObject() 
    {
        objSelected.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x , Input.mousePosition.y ,10)) + OffSet;
    }

    void DropObject()
    {

        if (DamageFild.OverlapPoint(objSelected.transform.position))
        {
            objSelected.GetComponent<CardControl>().OnDamage();
            objSelected.GetComponent<CardControl>().SetPoint = DamageFild.transform.position;
        }

        else if (BurnFild.OverlapPoint(objSelected.transform.position))
        {
            objSelected.GetComponent<CardControl>().OnBurn();
            objSelected.GetComponent<CardControl>().SetPoint = BurnFild.transform.position;
        }

//        else
            objSelected.GetComponent<CardControl>().OnDrop();

        objSelected = null;
    }

    public void AddCard()
    {
        GameObject obj = Instantiate(CardBlank, new Vector3(CreateFild.transform.position.x, CreateFild.transform.position.y, 0), Quaternion.identity) as GameObject;
    }
}