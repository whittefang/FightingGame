using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// handles all match information such as number of rounds
// handles round resets and end of match behaviors
public class RoundScript : MonoBehaviour {
	int playerOneWins;
	int PlayerTwoWins;
	int rounds;
	int timerCount;
	public bool firstTime;
	[SerializeField] Text TimerText;
	[SerializeField] Text roundDisplay;
	[SerializeField] Image[] RoundsGUI = new Image[4];
	[SerializeField]GameObject p1;
	[SerializeField]GameObject p2;
	[SerializeField] Health P1Health;
	[SerializeField] Health P2Health;
	[SerializeField] CharController P1control;
	[SerializeField] CharController P2control;
	Animator roundAnim;
	Canvas menu;
	GameObject menuControls;

	// Use this for initialization
	void Start () {
		firstTime = true;
		rounds = 0;
		menuControls = GameObject.Find ("EventSystem");
		menu = GameObject.Find ("GameOverMenu").GetComponent<Canvas> ();
		TimerText = GameObject.Find ("Timer").GetComponent<Text> ();
		RoundsGUI [0] = GameObject.Find ("P1Round1").GetComponent<Image> ();
		RoundsGUI [1] = GameObject.Find ("P1Round2").GetComponent<Image> ();
		RoundsGUI [2] = GameObject.Find ("P2Round1").GetComponent<Image> ();
		RoundsGUI [3] = GameObject.Find ("P2Round2").GetComponent<Image> ();
		
		roundAnim = GameObject.Find ("RoundCounter").GetComponent<Animator> ();
		roundDisplay = GameObject.Find ("RoundCounter").GetComponent<Text> ();
		InitializeChars ();
		menuControls.SetActive (false);
		menu.enabled = false;
		matchRestart (2);
		Time.timeScale = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Pause")){
			if (Time.timeScale == 1){
				Time.timeScale = 0;
				menu.enabled = true;
				menuControls.SetActive(true);
			}else {
				Time.timeScale = 1;
				menu.enabled = false;
				menuControls.SetActive(false);
			}
		}
	}
	
	// called when time runs out or hp reaches zero
	// determines winner and resets round or ends match
	public void endRound(){
		
		if (compareHPGreater()){
			playerOneWins++;
			if (playerOneWins ==1){
				RoundsGUI[0].enabled = true;
				matchRestart(3);
			}
			if (playerOneWins == 2){
				RoundsGUI[1].enabled = true;
				MatchWon(true);
			}
		}else if (compareHPLess()){
			PlayerTwoWins++;
			if (PlayerTwoWins ==1){
				RoundsGUI[2].enabled = true;
				matchRestart(3);
			}
			if (PlayerTwoWins == 2){
				RoundsGUI[3].enabled = true;
				MatchWon(false);
			}
		}else {
			//start new round
			matchRestart(3);
		}
	}
	
	public void InitializeChars(){
		p1 = GameObject.FindWithTag ("PlayerOne");
		p2 = GameObject.FindWithTag ("PlayerTwo");
		// player one initialization
		P1Health = p1.GetComponent<Health>();
		P2Health = p2.GetComponent<Health>();
		P1control = p1.GetComponent<CharController> ();
		P2control = p2.GetComponent<CharController> ();
	}
	
	// ui option that leads to restart of match
	public void Rematch(){
		Application.LoadLevel (3);
	}
	// two functions to compare health of players regarless of character
	bool compareHPGreater(){

		return (P1Health.GetHealth() > P2Health.GetHealth());
	}
	
	bool compareHPLess(){
		return (P1Health.GetHealth() < P2Health.GetHealth()); 
	}
	// re write later for use with different characters
	// turns character components off or on
	void ToggleActiveComponents(bool IsActive){
		P1Health.healthActive(IsActive);
		P1control.SetControlsEnabled(IsActive);
		P2Health.healthActive(IsActive);
		P2control.SetControlsEnabled(IsActive);
	}
	// ui option that leads to Main Menu
	public void MainMenu(){
		Application.LoadLevel (1);
	}
	public void CharacterSelect(){
		Application.LoadLevel (2);
	}
	// resets the game for the next round
	void matchRestart(float x){
		rounds++;
		ToggleActiveComponents (false);
		roundDisplay.text = "Round" + rounds.ToString ();
		roundDisplay.enabled = true;
		roundAnim.SetTrigger ("slide");
		StartCoroutine (RegainControl(x));
		CancelInvoke();
		
		
	}
	
	//function to reset health with proper characters
	void healthReset(){
		P1Health.resetHealth ();
		P2Health.resetHealth ();		
		
	}
	// called when a player wins two rounds
	// pulls up main menu
	void MatchWon(bool p1win){
		ToggleActiveComponents (false);
		
		if (p1win)
			roundDisplay.text = "Player One Wins";
		else
			roundDisplay.text = "Player Two Wins";
		roundDisplay.enabled = true;
		roundAnim.SetTrigger ("slide");
		CancelInvoke();
		timerCount = 0;
		StartCoroutine(pullUpMenu (4));
	}
	
	// handles match timer, decides winner when timer reaches 0 
	void timer (){
		timerCount++;
		TimerText.text = (99 - timerCount).ToString();
		if (timerCount > 99){
			endRound();
		}else{ 
			Invoke ("timer", 1f);
		}
	}
	
	// turns on all functions for player control and resets player positions and health
	IEnumerator RegainControl(float x){
		yield return new WaitForSeconds(x);
		ToggleActiveComponents (true);
		
		p1.GetComponent<Transform>().position = new Vector3 (-6, -3, 0);
		p2.GetComponent<Transform>().position = new Vector3 (6, -3, 0);
		roundDisplay.enabled = false;
		healthReset ();
		
		
		timerCount = 0;
		TimerText.text = "99";
		Invoke ("timer", 1f);
	}
	
	// brings up menu after 4 second delay
	IEnumerator pullUpMenu(float x){
		yield return new WaitForSeconds(x);
		menu.enabled = true;
		menuControls.SetActive (true);
	}
	
}
