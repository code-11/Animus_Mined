using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class msgManager : MonoBehaviour
{

		public class Message
		{
				private bool m_read;
				private bool m_locked;
				private string m_msg;
				private string m_sub;
				public Message (bool initRead, bool initLocked, string subject, string theMessage)
				{
						m_read = initRead;
						m_locked = initLocked;
						m_msg = theMessage;
						m_sub = subject;
				}
				public void markAsRead ()
				{
						m_read = true;
				}
				public void unlock ()
				{
						m_locked = false;
				}
				public bool isLocked ()
				{
						return m_locked;
				}
				public bool hasBeenRead ()
				{
						return m_read;
				}
				public string getMsg ()
				{
						return m_msg;
				}
				public string getSubject ()
				{
						return m_sub;
				}
				
		}
		
		private ArrayList m_messages;
		private ArrayList m_unlocked;
		private bool msgGuiUp = false;
		private int numSelected;
		private InputManager inCtrl;
		// Use this for initialization
		void Start ()
		{
				inCtrl=gameObject.GetComponent<InputManager>();
				m_messages = new ArrayList{
					new Message(false,false,"Test","WHOLE MESSAGE"),
					new Message(false,false,"Test2","Derp"),
					new Message(false,false,"Test3","WHOLE MESSAGE"),
					new Message(false,true,"Test4","Hidden"),
					new Message(false,false,"Test5","WHOLE MESSAGE"),
				};
				reevalUnLocked ();
			
		}
		public void reevalUnLocked ()
		{
				m_unlocked = new ArrayList ();
				foreach (Message msg in m_messages) {
						if (!msg.isLocked ()) {
								m_unlocked.Add (msg);
						}
				}
		}
		public bool getMsgGuiUp ()
		{
				return msgGuiUp;
		}
		public void activate (int msg)
		{
				((Message)m_messages [msg]).unlock ();
				reevalUnLocked ();
		}
		public ArrayList getAllMessagesIncludingLock ()
		{
				return m_messages;
		}
		public ArrayList getAllMessages ()
		{
				return m_unlocked;
		}	
		public Message getMessage (int i)
		{
				return (Message)m_unlocked [i];
		}
		public int getSelected ()
		{
				return numSelected;
		}
		public string getSelSub ()
		{
				return getMessage (getSelected ()).getSubject ();
		}
		public string getSelMsg ()
		{
				getMessage (getSelected ()).markAsRead ();
				return getMessage (getSelected ()).getMsg ();
		}
		public void selectUp ()
		{
				if (numSelected != 0) {
						numSelected -= 1;
				}
		}
		public void selectDown ()
		{
				if (numSelected != m_unlocked.Count - 1) {
						numSelected += 1;
				}
		}
		private bool ansiblePresent ()
		{
				int onlyPlayer = 1 << 9;
				int allButPlayer = ~onlyPlayer;
				Collider2D hit = Physics2D.OverlapPoint (new Vector2 (gameObject.transform.position.x, gameObject.transform.position.y), allButPlayer);
				if (hit != null) {
						return hit.name == "prefabAnsible";
				} else {
						return false;
				}
		}
		private void guiToggle ()
		{
				msgGui gui = gameObject.GetComponent<msgGui> ();

				bool yPres = inCtrl.useBuildPress();
				if (yPres && ansiblePresent ()) {
						if (msgGuiUp == true) {
								msgGuiUp = false;
								gui.enabled = false;
						} else {
								msgGuiUp = true;
								gui.enabled = true;
						}
						//createRecipeByNum (0);
				}
		}
		// Update is called once per frame
		void Update ()
		{
				guiToggle ();
		}
}
