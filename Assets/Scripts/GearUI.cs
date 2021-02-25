using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GearUI : MonoBehaviour
{
   private GearManager gearManager;
   private Image image;
   private Transform selected;
   private Transform parentAnt;
   private CircleCollider2D collider;

   private void Start()
   {
      image = this.GetComponent<Image>();
      gearManager = GameObject.FindGameObjectWithTag("GearManager").GetComponent<GearManager>();
      selected = GameObject.FindGameObjectWithTag("Selected").transform;
      collider = this.GetComponent<CircleCollider2D>();
   }

   private void OnMouseDown()
   {
      if (this.transform.parent != null)
      {
         parentAnt = this.transform.parent;
         this.transform.SetParent(selected);
      }

      parentAnt.GetComponent<Collider2D>().enabled = true;
   }

   private void OnMouseDrag()
   {
      Vector2 posMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);

      this.transform.position = posMouse;
   }

   private void OnMouseUp()
   {
      collider.enabled = false;

      RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.zero, 1f, LayerMask.GetMask("Slots"));
      RaycastHit2D hitUI = Physics2D.Raycast(this.transform.position, Vector2.zero, 1f, LayerMask.GetMask("UI"));

      if (hit && hit.transform.CompareTag("Position") && hit.transform.childCount == 0 && hit.transform != parentAnt)
      {
         gearManager.ConvertGearSprite(this.gameObject, image.color, hit.transform);
         hit.transform.GetComponent<CircleCollider2D>().enabled = false;
      }
      else if (hitUI && hitUI.transform.CompareTag("PositionUI") && hitUI.transform.childCount == 0 && hit.transform != parentAnt)
      {
         this.transform.SetParent(hitUI.transform);
         this.transform.localPosition = new Vector2(0, -20);
         hitUI.transform.GetComponent<BoxCollider2D>().enabled = false;
      }
      else
      {
         ReturnPeace();
      }

      collider.enabled = true;
   }

   public void ReturnPeace()
   {
      parentAnt.GetComponent<Collider2D>().enabled = false;
      this.transform.SetParent(parentAnt);
      this.transform.localPosition = new Vector2(0, -20);
   }
}