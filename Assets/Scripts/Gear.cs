using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gear : MonoBehaviour
{
   [SerializeField] private Sprite spriteUI;
   private GearManager gearManager;
   private SpriteRenderer spriteRenderer;
   private Sprite spriteAnt;
   private Transform parentAnt;
   private CircleCollider2D collider;

   private void Start()
   {
      gearManager = GameObject.FindGameObjectWithTag("GearManager").GetComponent<GearManager>();
      spriteRenderer = this.GetComponent<SpriteRenderer>();
      spriteAnt = spriteRenderer.sprite;
      collider = this.GetComponent<CircleCollider2D>();
   }

   private void OnMouseDown()
   {
      if (this.transform.parent != null)
      {
         parentAnt = this.transform.parent;
         this.transform.SetParent(null);
      }

      spriteRenderer.sprite = spriteUI;
      spriteRenderer.sortingOrder = 100;
      this.transform.localScale = new Vector2(0.5f, 0.5f);     
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
         this.transform.SetParent(hit.transform);
         spriteRenderer.sprite = spriteAnt;
         this.transform.localPosition = new Vector2(0, 0);
         this.transform.localScale = new Vector2(1, 1);
         hit.transform.GetComponent<CircleCollider2D>().enabled = false;
      }
      else if (hitUI && hitUI.transform.CompareTag("PositionUI") && hitUI.transform.childCount == 0 && hit.transform != parentAnt)
      {
         gearManager.ConvertGearUI(this.gameObject, spriteRenderer.color, hitUI.transform);
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
      spriteRenderer.sprite = spriteAnt;
      this.transform.localPosition = new Vector2(0, 0);
      this.transform.localScale = new Vector2(1, 1);
   }

}