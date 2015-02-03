using UnityEngine;
using System.Collections;

public class CharacterClass : MonoBehaviour {

	//Character types:
	public enum unitType{
		//insert different unit types here
		//right now, just contains three basic types
		SOLDIER,//futuristic warrior
		SCOUT,//futuristic ranger
		ENGINEER//futuristic "mage"
	};

	//Character Stats:
	public unitType character;//keeps track of the character type

	public int maxHealth;//maximum hitpoints
	public int curHealth;//current hitpoints

	public int maxEnergy;//maximum energy
	public int curEnergy;//current energy

	public int level;//current character level
	public int maxExperience;//experience needed to level up
	public int curExperience;//current experience

	public int baseDamage;
	public int attackRange;

	public int moveRange;//the amount of space that the character can move in a turn


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
		//sets the initial stats for a character
		int type = 0;//used to determine the character type, currently defaults to soldier
		//change int type later to recieve input from some source

		if (type == 0) {
			//Soldier class stats:
			character = unitType.SOLDIER;
			maxHealth = 150;
			curHealth = 150;
			maxEnergy = 5;
			curEnergy = 5;
			baseDamage = 40;
			attackRange = 1;
			moveRange = 4;
		} else if (type == 1) {
			//Scout class stats:
			character = unitType.SCOUT;
			maxHealth = 100;
			curHealth = 100;
			maxEnergy = 40;
			curEnergy = 40;
			baseDamage = 35;
			attackRange = 5;
			moveRange = 6;
		} else {
			//Scout class stats:
			character = unitType.ENGINEER;
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
