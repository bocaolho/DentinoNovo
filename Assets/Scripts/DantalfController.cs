using UnityEngine;
using System.Collections;

public class DantalfController : MonoBehaviour {

	Animator anim_main;
	public GUIText dentalfText;
	public GameObject dialogbox1;
	public Camera camera;
	public GameObject takeThis;
	public int talkIndex;
	private DentinoControllerScript dentinoController;
	private int textState;
	private bool canGoNext;
	
	private string[,] texts = { 
		{"Saudações!\nMeu nome é Dentalf!", 
			"Nesse treinamento,\n vou te ensinar um\n pouco sobre higiene!",
			"Antes de mais nada, \nÉ perigoso ir sozinho!\n Leve isso ...", 
			"Boa sorte..." } ,

		{ "Olá novamente,\n meu caro Dentino!", 
			"Parabéns! \n Você completou seu\n treinamento, mas...", 
			"... eu sou um dente\n de leite e agora\n estou velho.\n", 
			"É sua vez, Dentino!\nMe deixe orgulhoso\n e salve os dentes!" } };
	/*
	private string[,] texts = { 
		{"¡Saludos! Mi\n nombre es Dentalf!", 
			"En este entrenamien-\n to, voy a enseñar un\n poco sobre la\n higiene oral!", 
			"En primer lugar, es\n peligroso ir solo!\n Tome este ...", 
			"Buena suerte ..." }, 
		
		{ "Hola de nuevo,\n mi querido Dentino!", 
			"¡Felicitaciones! Ha\n completado su for-\n mación, pero ...",
			"... soy un diente\n de leche y ya soy\n viejo.", 
			"Es tu turno, Dentino!\n Haz que me sienta\n orgulloso y protege\n la boca!" } };
	*/
	private int[] textsLenghts = { 4, 4 };
	private int takeThisState = 2;
	private int takeThisIndex = 0;
	private int touchId;
	private bool rolling;

	// Use this for initialization
	void Start () {
		anim_main = GetComponent<Animator>();
		touchId = -1;
		textState = -1;
		canGoNext = false;
		rolling = false;
		dentalfText.color = Color.black;
		dentalfText.fontSize = 26;
	}
	
	// Update is called once per frame
	void Update () {
		if (rolling)
			dentalfText.transform.position = camera.WorldToViewportPoint(dialogbox1.transform.position + new Vector3 (0.15f,0,0));

		foreach (Touch t in Input.touches) {
			if (touchId == -1 && t.phase == TouchPhase.Began) {
				touchId = t.fingerId;
				if (canGoNext)
					nextText();
			}
			if (t.fingerId == touchId && t.phase == TouchPhase.Ended) {
				touchId = -1;
			}
		}
		if (Input.GetMouseButtonDown(0) && 
		    ((Application.platform == RuntimePlatform.WindowsEditor) || 
		 	(Application.platform == RuntimePlatform.WindowsPlayer) ||
		 	(Application.platform == RuntimePlatform.WindowsWebPlayer) ||
		 	(Application.platform == RuntimePlatform.LinuxPlayer) ||
			 (Application.platform == RuntimePlatform.OSXEditor) ||
			 (Application.platform == RuntimePlatform.OSXPlayer) ||
			 (Application.platform == RuntimePlatform.OSXWebPlayer))) {
			if (canGoNext) {
				nextText();
			}
		}
		if (Input.GetKeyDown(KeyCode.Z) && 
		    ((Application.platform == RuntimePlatform.WindowsEditor) || 
		 (Application.platform == RuntimePlatform.WindowsPlayer) ||
		 (Application.platform == RuntimePlatform.WindowsWebPlayer) ||
		 (Application.platform == RuntimePlatform.LinuxPlayer) ||
		 (Application.platform == RuntimePlatform.OSXEditor) ||
		 (Application.platform == RuntimePlatform.OSXPlayer) ||
		 (Application.platform == RuntimePlatform.OSXWebPlayer))) {
			if (canGoNext) {
				nextText();
			}
		}
	}
	
	void OnCollisionEnter2D (Collision2D coll) {
		if (coll.gameObject.tag == "Player") {
			dentinoController = coll.gameObject.GetComponent<DentinoControllerScript>();
			dentinoController.lockPosition = true;
			GetComponent<BoxCollider2D>().enabled = false;
			anim_main.SetTrigger ("StartTalk");
		}
	}

	void startText() {
		rolling = true;
		textState = 0;
		canGoNext = true;
		dentalfText.text = texts[talkIndex, 0];
	}

	void nextText() {
		textState++;

		if (takeThisState == textState && takeThisIndex == talkIndex)
			takeThis.SetActive (true);

		if (textState < textsLenghts[talkIndex]) {
			dentalfText.text = texts[talkIndex, textState];
		} else {
			dentalfText.text = "";
			canGoNext = false;
			textState = 0;
			anim_main.SetTrigger ("EndTalk");
		}
	}

	void endedText() {
		rolling = false;
		if (talkIndex == 0) {
			dentinoController.lockPosition = false;
		}
		if (talkIndex == 1) {
			dentinoController.jumpForever = true;
			Invoke ("GoToMainMenu", 5);
		}
	}

	void GoToMainMenu() {
		Application.LoadLevel("MainMenu");
	}
}
