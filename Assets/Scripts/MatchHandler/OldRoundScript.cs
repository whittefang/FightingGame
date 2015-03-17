using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// handles all match information such as number of rounds
// handles round resets and end of match behaviors
public class OldRoundScript : MonoBehaviour {
/*	int playerOneWins;
	int PlayerTwoWins;
	int rounds;
	int timerCount;
	public bool firstTime;
	[SerializeField] Text TimerText;
	[SerializeField] Text roundDisplay;
	[SerializeField] Image[] RoundsGUI = new Image[4];
	[SerializeField]GameObject p1;
	[SerializeField]GameObject p2;
	Animator roundAnim;
	Canvas menu;
	GameObject menuControls;
	[SerializeField] CharacterAttackScriptJAT CAScriptP1;
	[SerializeField] CharacterAttackScriptJAT CAScriptP2;
	[SerializeField] CharacterAttackScriptZoner ZAScriptP1;
	[SerializeField] CharacterAttackScriptZoner ZAScriptP2;
	[SerializeField] CharacterAttackScriptPixie PAScriptP1;
	[SerializeField] CharacterAttackScriptPixie PAScriptP2;
	[SerializeField] CharacterAttackScriptGrap GAScriptP1;
	[SerializeField] CharacterAttackScriptGrap GAScriptP2;
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
		if (p1.GetComponent<CharacterAttackScriptJAT>() != null){
			CAScriptP1 = p1.GetComponent<CharacterAttackScriptJAT>();
		}else if(p1.GetComponent<CharacterAttackScriptZoner>() != null){
			ZAScriptP1 = p1.GetComponent<CharacterAttackScriptZoner>();
		}else if (p1.GetComponent<CharacterAttackScriptPixie>() != null){
			PAScriptP1 = p1.GetComponent<CharacterAttackScriptPixie>();
		}else if (p1.GetComponent<CharacterAttackScriptGrap>() != null){
			GAScriptP1 = p1.GetComponent<CharacterAttackScriptGrap>();
		}

		// player two initialization
		if (p2.GetComponent<CharacterAttackScriptJAT>() != null){
			CAScriptP2 = p2.GetComponent<CharacterAttackScriptJAT>();
		}else if(p2.GetComponent<CharacterAttackScriptZoner>() != null){
			ZAScriptP2 = p2.GetComponent<CharacterAttackScriptZoner>();
		}else if (p2.GetComponent<CharacterAttackScriptPixie>() != null){
			PAScriptP2 = p2.GetComponent<CharacterAttackScriptPixie>();
		}else if (p2.GetComponent<CharacterAttackScriptGrap>() != null){
			GAScriptP2 = p2.GetComponent<CharacterAttackScriptGrap>();
		}
	}

	// ui option that leads to restart of match
	public void Rematch(){
		Application.LoadLevel (3);
	}
	// two functions to compare health of players regarless of character
	bool compareHPGreater(){
		bool x = false;
		if ( CAScriptP1!= null && CAScriptP2 != null){
			x = CAScriptP1.GetHealth() > CAScriptP2.GetHealth();
		}else if ( CAScriptP1!= null && ZAScriptP2 != null){
			x = CAScriptP1.GetHealth() > ZAScriptP2.GetHealth();
		}else if ( CAScriptP1!= null && PAScriptP2 != null){
			x = CAScriptP1.GetHealth() > PAScriptP2.GetHealth();
		}else if ( CAScriptP1!= null && GAScriptP2 != null){
			x = CAScriptP1.GetHealth() > GAScriptP2.GetHealth();
		}else if ( ZAScriptP1!= null && CAScriptP2 != null){
			x = ZAScriptP1.GetHealth() > CAScriptP2.GetHealth();
		}else if ( ZAScriptP1!= null && ZAScriptP2 != null){
			x = ZAScriptP1.GetHealth() > ZAScriptP2.GetHealth();
		}else if ( ZAScriptP1!= null && PAScriptP2 != null){
			x = ZAScriptP1.GetHealth() > PAScriptP2.GetHealth();
		}else if ( ZAScriptP1!= null && GAScriptP2 != null){
			x = ZAScriptP1.GetHealth() > GAScriptP2.GetHealth();
		}else if ( PAScriptP1!= null && PAScriptP2 != null){
			x = PAScriptP1.GetHealth() > PAScriptP2.GetHealth();
		}else if ( PAScriptP1!= null && GAScriptP2 != null){
			x = PAScriptP1.GetHealth() > GAScriptP2.GetHealth();
		}else if ( PAScriptP1!= null && CAScriptP2 != null){
			x = PAScriptP1.GetHealth() > CAScriptP2.GetHealth();
		}else if ( PAScriptP1!= null && ZAScriptP2 != null){
			x = PAScriptP1.GetHealth() > ZAScriptP2.GetHealth();
		}else if ( GAScriptP1!= null && GAScriptP2 != null){
			x = GAScriptP1.GetHealth() > GAScriptP2.GetHealth();
		}else if ( GAScriptP1!= null && CAScriptP2 != null){
			x = GAScriptP1.GetHealth() > CAScriptP2.GetHealth();
		}else if ( GAScriptP1!= null && ZAScriptP2 != null){
			x = GAScriptP1.GetHealth() > ZAScriptP2.GetHealth();
		}else if ( GAScriptP1!= null && PAScriptP2 != null){
			x = GAScriptP1.GetHealth() > PAScriptP2.GetHealth();
		}
		return x;
	}

	bool compareHPLess(){
		bool x = false;
		if ( CAScriptP1!= null && CAScriptP2 != null){
			x = CAScriptP1.GetHealth() < CAScriptP2.GetHealth();
		}else if ( CAScriptP1!= null && ZAScriptP2 != null){
			x = CAScriptP1.GetHealth() < ZAScriptP2.GetHealth();
		}else if ( CAScriptP1!= null && PAScriptP2 != null){
			x = CAScriptP1.GetHealth() < PAScriptP2.GetHealth();
		}else if ( CAScriptP1!= null && GAScriptP2 != null){
			x = CAScriptP1.GetHealth() < GAScriptP2.GetHealth();
		}else if ( ZAScriptP1!= null && CAScriptP2 != null){
			x = ZAScriptP1.GetHealth() < CAScriptP2.GetHealth();
		}else if ( ZAScriptP1!= null && ZAScriptP2 != null){
			x = ZAScriptP1.GetHealth() < ZAScriptP2.GetHealth();
		}else if ( ZAScriptP1!= null && PAScriptP2 != null){
			x = ZAScriptP1.GetHealth() < PAScriptP2.GetHealth();
		}else if ( ZAScriptP1!= null && GAScriptP2 != null){
			x = ZAScriptP1.GetHealth() < GAScriptP2.GetHealth();
		}else if ( PAScriptP1!= null && PAScriptP2 != null){
			x = PAScriptP1.GetHealth() < PAScriptP2.GetHealth();
		}else if ( PAScriptP1!= null && GAScriptP2 != null){
			x = PAScriptP1.GetHealth() < GAScriptP2.GetHealth();
		}else if ( PAScriptP1!= null && CAScriptP2 != null){
			x = PAScriptP1.GetHealth() < CAScriptP2.GetHealth();
		}else if ( PAScriptP1!= null && ZAScriptP2 != null){
			x = PAScriptP1.GetHealth() < ZAScriptP2.GetHealth();
		}else if ( GAScriptP1!= null && GAScriptP2 != null){
			x = GAScriptP1.GetHealth() < GAScriptP2.GetHealth();
		}else if ( GAScriptP1!= null && CAScriptP2 != null){
			x = GAScriptP1.GetHealth() < CAScriptP2.GetHealth();
		}else if ( GAScriptP1!= null && ZAScriptP2 != null){
			x = GAScriptP1.GetHealth() < ZAScriptP2.GetHealth();
		}else if ( GAScriptP1!= null && PAScriptP2 != null){
			x = GAScriptP1.GetHealth() < PAScriptP2.GetHealth();
		}
		return x;
	}
	// re write later for use with different characters
	// turns character components off or on
	void ToggleActiveComponents(bool IsActive){
		if (CAScriptP1 != null) {
				CAScriptP1.removeFB ();
			CAScriptP1.healthActive (IsActive);
			CAScriptP1.SetControlsEnabled(IsActive);
		}else  if (ZAScriptP1 != null) {
			ZAScriptP1.removeFB ();
			ZAScriptP1.SetControlsEnabled(IsActive);
			ZAScriptP1.healthActive (IsActive);
		}else  if (PAScriptP1 != null) {
			PAScriptP1.SetControlsEnabled(IsActive);
			PAScriptP1.healthActive (IsActive);
		}else  if (GAScriptP1 != null) {
			GAScriptP1.SetControlsEnabled(IsActive);
			GAScriptP1.healthActive (IsActive);
		}
		
		if (CAScriptP2 != null) {
			CAScriptP2.removeFB ();
			CAScriptP2.healthActive (IsActive);
			CAScriptP2.SetControlsEnabled(IsActive);
		}else if (ZAScriptP2 != null) {
			ZAScriptP2.removeFB ();
			ZAScriptP2.SetControlsEnabled(IsActive);
			ZAScriptP2.healthActive (IsActive);
		}else  if (PAScriptP2 != null) {
			PAScriptP2.SetControlsEnabled(IsActive);
			PAScriptP2.healthActive (IsActive);
		}else  if (GAScriptP2 != null) {
			GAScriptP2.SetControlsEnabled(IsActive);
			GAScriptP2.healthActive (IsActive);
		}

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
		if (CAScriptP1 != null) {
			CAScriptP1.resetHealth ();
		} else if (ZAScriptP1 != null) {
			ZAScriptP1.resetHealth ();		
		} else if (PAScriptP1 != null) {
			PAScriptP1.resetHealth ();		
		}else if (GAScriptP1 != null) {
			GAScriptP1.resetHealth ();		
		}
		
		if (CAScriptP2 != null) {
			CAScriptP2.resetHealth ();		
		}else if (ZAScriptP2 != null) {
			ZAScriptP2.resetHealth ();		
		} else if (PAScriptP2 != null) {
			PAScriptP2.resetHealth ();		
		}else if (GAScriptP2 != null) {
			GAScriptP2.resetHealth ();		
		}
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
*/
}
