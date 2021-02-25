using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GearManager : MonoBehaviour
{
   [SerializeField] private GameObject spriteGear;
   [SerializeField] private Image imageGear;
   [SerializeField] private Color[] color;
   [SerializeField] private Transform[] posGears;
   [SerializeField] private Transform[] posInventory;
   [SerializeField] private Nugget nugget;
   private List<Transform> gears = new List<Transform>();

   public void ConvertGearSprite(GameObject gearUI, Color color, Transform slot)
   {
      Destroy(gearUI);
      GameObject gear = Instantiate(spriteGear, slot.position, Quaternion.identity);
      gear.GetComponent<SpriteRenderer>().color = color;
      gear.GetComponent<SpriteRenderer>().sortingOrder = 5;
      gear.transform.SetParent(slot);
      gear.transform.localPosition = Vector2.zero;
      gear.transform.localScale = new Vector2(1, 1);
      CheckSlots();
   }

   public void ConvertGearUI(GameObject gearSprite, Color color, Transform slot)
   {
      Destroy(gearSprite);
      Image gear = Instantiate(imageGear, slot.position, Quaternion.identity);
      gear.GetComponent<Image>().color = color;
      gear.transform.SetParent(slot);
      gear.transform.localPosition = new Vector2(0, -20);
      gear.transform.localScale = new Vector2(1,1);
      CheckSlots();
   }

   public void ResetGears()
   {
      gears.Clear();

      for (int i = 0; i < posGears.Length; i++)
      {
         if (posGears[i].childCount > 0)
         {
            Destroy(posGears[i].GetChild(0).gameObject);

            posGears[i].GetComponent<CircleCollider2D>().enabled = true;
         }
      }

      for (int i = 0; i < posInventory.Length; i++)
      {
         if (posInventory[i].childCount == 0)
         {
            posInventory[i].GetComponent<BoxCollider2D>().enabled = false;
            Image gear = Instantiate(imageGear, posInventory[i].position, Quaternion.identity);
            gear.transform.SetParent(posInventory[i]);
            gear.transform.localPosition = new Vector2(0, -20);
            gear.transform.localScale = new Vector2(1, 1);  
         }

         posInventory[i].GetChild(0).GetComponent<Image>().color = color[i];
      }

   }

   private void Update()
   {
      if (gears.Count == 5)
      {
         for (int i = 0; i < gears.Count; i++)
         {
            if (i%2==0)
            {
               RotateGear(gears[i], 15);
            }
            else
            {
               RotateGear(gears[i], -15);
            }    
         }
      }
   }

   private void RotateGear(Transform obj, int direction)
   {
      obj.Rotate(Vector3.forward, direction * Time.deltaTime);
   }

   public void CheckSlots()
   {
      gears.Clear();
      
      for (int i = 0; i < posGears.Length; i++)
      {
         if (posGears[i].childCount == 0)
         {
            nugget.ResetBaloon();
            return;
         }
         gears.Add(posGears[i].GetChild(0));
      }

      if (gears.Count == 5)
      {
         nugget.SetTalk("YAY, PARABÉNS. TASK CONCLUÍDA!");
      }
   }
}
