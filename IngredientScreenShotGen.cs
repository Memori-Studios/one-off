using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace TJ
{
public class IngredientScreenShotGen : MonoBehaviour
{
	public IngredientToScreenShot[] ingredientsToScreenShot;
    Ingredient currentIngredient;
    public Ingredient[] ingredients;
    private void Start()
    {
        // for (int i = 0; i < ingredientsToScreenShot.Length; i++)
        // {
        //     CreateMyAsset(ingredientsToScreenShot[i].gameObject.name);   
        // }
        StartCoroutine(GenerateScreenShots());
        //ApplySprites();
    }
    public IEnumerator GenerateScreenShots()
    {
        yield return new WaitForSeconds(1f);
        foreach (IngredientToScreenShot item in ingredientsToScreenShot)
        {
            item.gameObject.SetActive(true);
            yield return new WaitForSeconds(.5f);
            currentIngredient = item.ingredient;
            ScreenshotHandler.TakeScreenshot_Static(512,512);
            EditorUtility.SetDirty(currentIngredient);
            
            yield return new WaitForSeconds(.5f);
            item.gameObject.SetActive(false);

        }
    }
    public void ApplySprites()
    {
        foreach (Ingredient item in ingredients)
        {
            item.ingredientIcon =  (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Art/Ingredients/Icons/"+item.name, typeof(Sprite));
            EditorUtility.SetDirty(item);
        }
        AssetDatabase.SaveAssets();
    }
    public static void CreateMyAsset(string i)
    {
        Ingredient asset = ScriptableObject.CreateInstance<Ingredient>();

        AssetDatabase.CreateAsset(asset,"Assets/Art/Ingredients/"+i.Replace(" ", "")+".asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }

    public string GetFileName()
    {
        return "Assets/Art/Ingredients/Icons/" + currentIngredient.name;
    }
}
}
