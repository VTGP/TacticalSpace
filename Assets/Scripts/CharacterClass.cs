using UnityEngine;
using System.Collections;

public class CharacterClass : MonoBehaviour {

	//Character types:
	public enum UnitType{
		//insert different unit types here
		//right now, just contains three basic types
		SOLDIER,//futuristic warrior
		SCOUT,//futuristic ranger
		ENGINEER//futuristic "mage"
	};

	//Character Stats:
	public UnitType unitType;//keeps track of the character type

	private int maxHealth;//maximum hitpoints
	private int curHealth;//current hitpoints

	private int maxEnergy;//maximum energy
	private int curEnergy;//current energy

	private int level;//current character level
	private int maxExperience;//experience needed to level up
	private int curExperience;//current experience

	private int baseDamage;
	private int attackRange;

	private int moveRange;//the amount of space that the character can move in a turn


	//Character Inventory:
	//These items can affect the way a character acts in the game
	public enum weaponType{
		NONE,
		SWORD,
		LASER_RIFLE
	};
	public enum armorType{
		NONE,
		LIGHT,
		MEDIUM,
		HEAVY
	};
	public weaponType weapon;//currently equipped weapon
	public armorType armor;//currently equipped armor



	// Use this for initialization
	void Start () {
		//change int type later to recieve input from some source

		if (unitType == UnitType.SOLDIER) {
			//Soldier class stats:
			maxHealth = 150;
			curHealth = 150;
			maxEnergy = 5;
			curEnergy = 5;
			baseDamage = 40;
			attackRange = 1;
			moveRange = 4;
		} else if (unitType == UnitType.SCOUT) {
			//Scout class stats:
			maxHealth = 100;
			curHealth = 100;
			maxEnergy = 40;
			curEnergy = 40;
			baseDamage = 35;
			attackRange = 5;
			moveRange = 6;
		} else {
			//Scout class stats:
			maxHealth = 120;
			curHealth = 120;
			maxEnergy = 100;
			curEnergy = 100;
			baseDamage = 25;
			attackRange = 3;
			moveRange = 5;
		}
		//Stats shared by all unit types:
		level = 1;
		maxExperience = 100;
		curExperience = 0;

		//Sets default inventory:
		weapon = weaponType.NONE;
		armor = armorType.NONE;

	}
	
	// Update is called once per frame
	void Update () {
		//check if health or enengy are exceeding their maximum
		if (curHealth > maxHealth) {
			curHealth = maxHealth;
		}
		if (curEnergy > maxEnergy) {
			curEnergy = maxEnergy;
		}

		//check if the character can level up
		if (curExperience >= maxExperience) {
			levelUp();
		}
	}

//---------------Extra Functions------------------

	// Level up the character
	private void levelUp(){
		maxExperience *= 2;//doubles the amount of experience required to level up
		level++;//increments the level

		//increase base stats
		//change this later so the stat increase is based on unit type
		maxHealth += 10;
		curHealth = maxHealth;//on levelup, health and energy is restored
		maxEnergy += 5;
		curEnergy = maxEnergy;
		baseDamage += 5;
	}

	// Calculates the damage done during an attack
	public int attack(){
		//play animation
		//[insert code here]

		//calculate damage to do
		//base damage:
		int finalDamage = baseDamage;

		//random extra damage:
		//Random rng = new Random ();
		//int extraDamage = (rng.Next()) % 10;
		//finalDamage += extraDamage;//the outgoing damage can be anywhere from baseDamage to baseDamage+9

		//bonus damage from weapons:
		//--insert statements here--

		return finalDamage;
	}


	// Takes damage based on an input
	public void takeDamage(int d){
		curHealth -= d;
		if (curHealth < 0) {
			curHealth = 0;
		}
	}


	// Test if character is dead
	public bool isDead(){
		return (curHealth <= 0);
	}


	// Move character to the selected position
	public bool move(int x, int y, int z){
		//if move command destination is within move range, move and return true
		//else return false
		return false;
	}
}
