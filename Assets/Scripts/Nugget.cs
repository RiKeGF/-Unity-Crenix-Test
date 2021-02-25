using UnityEngine;
using UnityEngine.UI;

public class Nugget : MonoBehaviour
{
   [SerializeField] private Text txtBaloon;
   private string speakStart = "ENCAIXE AS ENGRENAGENS EM QUALQUER ORDEM !";

   public void SetTalk(string text)
   {
      txtBaloon.text = text;  
   }

   public void ResetBaloon()
   {
      txtBaloon.text = speakStart;
   }

}
