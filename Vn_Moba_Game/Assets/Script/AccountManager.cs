using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AccountManager : MonoBehaviour
{
   [SerializeField] private GameObject panelRoomManager = null;
   [SerializeField]
   private TMP_InputField inputUsername = null;
   [SerializeField]
   private TMP_InputField inputPassword = null;
   [SerializeField] private Button buttonLogin = null;
   public GameObject statusLoginText = null;
   private string _username;
   private string _password;
   
   void Update()
   {
      if (!string.IsNullOrEmpty(inputPassword.text) && !string.IsNullOrEmpty(inputUsername.text))
      {
         buttonLogin.interactable = true;
         _username = inputUsername.text;
         _password = inputPassword.text;
      }
   }
   
   public void DoLogin()
   {
      if (_password.Equals("viet1998") && _username.Equals("ben1998em"))
      {
         Debug.Log("Login!");
         panelRoomManager.active = true;
         gameObject.active = false;
      }
      else
      {
         Debug.Log("Login fail please try again!");
         statusLoginText.GetComponent<TMP_Text>().text = "Login fail please try again!";
         inputPassword.text = "";
         statusLoginText.active = true;
         StartCoroutine(HiddenLoginStatus(3));
      }
   }

   IEnumerator HiddenLoginStatus(float timeWait)
   {
      yield return new WaitForSeconds(timeWait);
      statusLoginText.active = false;
   }
}
