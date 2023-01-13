using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class CS_CraftTable : CS_Building
{
    [SerializeField] List<CS_Recipe> recipes;
    [SerializeField] Transform socketSpaw;
    Transform itemParent;
    List<CS_Item> ressources;

    bool isInCraft;
    bool reduceThisCraft;
    CS_Recipe currentRecipe;
    List<CS_Item> usedRessouces;

    private void Start()
    {
        ressources = new List<CS_Item>();
        usedRessouces = new List<CS_Item>();
        itemParent = GameObject.FindGameObjectWithTag("ItemsParent").transform;
    }

    public void Craft()
    {
        if (isInCraft == false)
        {
            foreach (var rec in recipes)
            {
                if (RecipeIsPossible(rec, ressources))
                {
                    isInCraft = true;

                    ressources[0].gameObject.transform.localScale = Vector3.one / 2;
                    reduceThisCraft = false;
                    //foreach (var item in ressources) //Destroy All ressources
                    //{
                    //    Destroy(item.gameObject);
                    //}
                    //ressources.Clear();

                    //for (int i = 0; i < rec.ingredients.Count; i++) //To make included recipes
                    //{
                    //    Destroy(ressources[0].gameObject);
                    //    ressources.RemoveAt(0);
                    //}
                }
            }
        }
        else
        {
            if (currentRecipe != null)
            {
                if (reduceThisCraft)
                {
                    ressources[0].gameObject.transform.localScale = Vector3.one / 2;
                }
                else
                {
                    ressources[0].GetComponentInChildren<Renderer>().enabled = false;
                    ressources[0].CanBeTakable = false;
                    usedRessouces.Add(ressources[0]);
                    ressources.RemoveAt(0);
                }
                reduceThisCraft = !reduceThisCraft;

                if (ressources.Count == 0)
                {
                    //Spawn Object
                    GameObject temp = Instantiate(currentRecipe.prefab);
                    temp.transform.rotation = Quaternion.identity; //Rotate(Vector3.zero, Space.World);
                    temp.transform.position = socketSpaw.position;
                    temp.transform.parent = itemParent;
                    

                    //Destroy ressources
                    foreach (var item in usedRessouces)
                    {
                        Destroy(item.gameObject);
                    }

                    ResetVar();

                }
            }
        }
    }

    public bool RecipeIsPossible(CS_Recipe recipe, List<CS_Item> ingredients)
    {
        List<E_Item> enumsRecipe = new List<E_Item>(recipe.ingredients);
        List<E_Item> enumsIngredient = new List<E_Item>(ToEnum(ingredients));

        foreach (var item in enumsRecipe)
        {
            if (enumsIngredient.Contains(item))
            {
                enumsIngredient.Remove(item);
            }
            else
            {
                return false;
            }
        }

        if (enumsIngredient.Count > 0)
            return false;

        //enumsIngredient = ToEnum(ingredients);      //To make included recipes

        //foreach (var item in ressources.ToList())
        //{
        //    if (enumsRecipe.Contains(item.ToEnum()))
        //    {
        //        enumsRecipe.Remove(item.ToEnum());
        //        ressources.MoveToFirst<CS_Item>(item);
        //    }
        //}

        currentRecipe = recipe;

        return true;
    }


    private void CancelCraft()
    {
        foreach (var item in usedRessouces.ToList())
        {
            item.CanBeTakable = true;
            item.GetComponentInChildren<Renderer>().enabled = true;
            ressources.Add(item);
            usedRessouces.Remove(item);
        }
        foreach (var item in ressources)
        {
            item.transform.localScale = Vector3.one;
        }
        ResetVar();
        
    }

    private void ResetVar()
    {
        isInCraft = false;
        currentRecipe = null;
        usedRessouces.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        CS_Item item = other.GetComponent<CS_Item>();
        if (item != null)
        {
            ressources.Add(item);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CS_Item item = other.GetComponent<CS_Item>();
        if (item != null)
        {
            if (isInCraft == true)
            {
                if (ressources.Contains(item) || usedRessouces.Contains(item))
                {
                    CancelCraft();
                }
            }
            ressources.Remove(item);
        }
    }

    private List<E_Item> ToEnum(List<CS_Item> items)
    {
        List<E_Item> result = new List<E_Item>();
        foreach (var item in items)
        {
            result.Add(item.ToEnum());
        }

        return result;
    }
}
