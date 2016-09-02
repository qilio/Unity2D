using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UserInput : MonoBehaviour 
{
	[Header("Input")]
	public float h;

	[Header("Player")]
	public float maxHealth = 5;
	public float curHealth = 5;
	public bool dead;
	public bool onGround;
	[Range(0,200)]
	public float jumpHeight;
	[Range(0,5)]
	public float characterSpeed = 2.5f;
	public int lookPosition = 1;

	[Header("Weapon")]
	public float maxRateOfFire = 1;
	public float rateOfFire = 1;

	public static int gold = 0;

	public Transform bulletSpawnPoint;
	public LayerMask whatToHit;
	public GameObject bulletPrefab;

	public GameObject bulletSpawnPistol;
	public GameObject bulletSpawnShotgun;
	public GameObject bulletSpawnRifle;

	public GameObject bulletSpawn;

	public HUDManager HUD;

	private WeaponControl weaponControl;
	private Animator anim;
	private Rigidbody2D rigidbody2D;
	private int weaponRng = 2;
	private int weaponDmg = 1;
	private int pauseNum = 0;
	private static int wepIndex;

	private static int  rAmmo = 0;
	private static int sAmmo = 0;
	private string gunOut = "pistol";
	private bool inControlls;


    public AudioSource source;
    public AudioClip jump;
	public AudioClip empty;


	void Awake()
	{
		rigidbody2D = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		rateOfFire = maxRateOfFire;
		curHealth = maxHealth;
		weaponControl = GetComponent<WeaponControl> ();
	}

	void Update()
	{

		anim.SetFloat ("Speed", h);

		if (rateOfFire > 0) 
		{		
			rateOfFire -= Time.deltaTime;
		}

		if (Input.GetMouseButton(0) && rateOfFire < 0 && getPausenum() == 0) 
		{
			anim.SetTrigger ("Shoot");
			rateOfFire = maxRateOfFire;
			Shoot ();
		}

		if (Input.GetAxis ("Horizontal") < -0.1f && getPausenum() == 0) 
		{
			transform.localScale = new Vector3 (-1, 1, 1);
			GetComponentInChildren<ArmRotation> ().rotationOffset = 180;
			GetComponentInChildren<ArmRotation> ().zRotation = -1;
		}

		if (Input.GetAxis ("Horizontal") > 0.1f && getPausenum() == 0) 
		{
			transform.localScale = new Vector3 (1, 1, 1);
			GetComponentInChildren<ArmRotation> ().rotationOffset = 0;
			GetComponentInChildren<ArmRotation> ().zRotation = 1;
		}

		if (rigidbody2D.velocity.y == 0) {
			onGround = true;
		} else {
			onGround = false;
		}

		if (Input.GetKeyDown (KeyCode.Space) && onGround && getPausenum() == 0)
		{
			rigidbody2D.AddForce (Vector2.up * 1000 * jumpHeight);
            source.PlayOneShot(jump, 1.0f);
            anim.SetTrigger ("Jump");
		}

		if (Input.GetKeyDown (KeyCode.Q) && getPausenum() == 0) 
		{
			wepIndex = GetComponent<WeaponControl> ().SwitchWeaponUp(weaponRng, weaponDmg);
			HUD.weaponSwitch (wepIndex);
		}
		if (Input.GetKeyDown (KeyCode.E) && getPausenum() == 0) 
		{
			wepIndex = GetComponent<WeaponControl> ().SwitchWeaponDown(weaponRng, weaponDmg);
			HUD.weaponSwitch (wepIndex);
		}
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (getPausenum() == 0) {
				GameManager.pause ();
				HUD.pauseMen ();
				pauseNum = 1;
				return;
			}
			if (inControlls) {
				HUD.exitControlls ();
				return;
			}
			pauseNumZero ();
			GameManager.play ();
			HUD.pauseMen ();
		}

		if (weaponControl.weaponID == 0) {
			bulletSpawn = bulletSpawnPistol;
			gunOut = "pistol";
		}
		if (weaponControl.weaponID == 1) {
			bulletSpawn = bulletSpawnRifle;
			gunOut = "rifle";
		}
		if (weaponControl.weaponID == 2) {
			bulletSpawn = bulletSpawnShotgun;
			gunOut = "shotgun";
		}
	}

	void FixedUpdate()
	{
		h = Input.GetAxis ("Horizontal");

		if (Input.GetAxis ("Horizontal") < -0.1f && getPausenum() == 0) 
			transform.Translate (Vector2.right * characterSpeed * h * Time.deltaTime);
		
		if (Input.GetAxis ("Horizontal") > -0.1f && getPausenum() == 0) 
			transform.Translate (Vector2.right * characterSpeed * h * Time.deltaTime);

	}

	void Shoot()
	{
		if (!loseAmmo(gunOut)) {
			source.PlayOneShot(empty, 1.0f);
			return;
		}
		GetComponent<AudioSource> ().Play();
		Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint (Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
		Vector2 firePointPosition = new Vector2 (bulletSpawnPoint.position.x, bulletSpawnPoint.position.y);
		RaycastHit2D hit = Physics2D.Raycast (firePointPosition, mousePosition - firePointPosition, weaponRng, whatToHit);
		Debug.DrawLine (firePointPosition, (mousePosition - firePointPosition) * weaponRng);

		Instantiate (bulletPrefab, bulletSpawn.transform.position, bulletSpawn.transform.rotation);

		if (hit.collider != null) 
		{
			if (hit.collider.gameObject.GetComponent<AlienAI> ()) 
			{
				hit.collider.gameObject.GetComponent<AlienAI> ().ApplyDamage (weaponDmg);
			}
			if (hit.collider.gameObject.GetComponent<AlienBossAI> ()) 
			{
				hit.collider.gameObject.GetComponent<AlienBossAI> ().ApplyDamage (weaponDmg);
			}
			if (hit.collider.gameObject.GetComponent<Rocket> ()) 
			{
				hit.collider.gameObject.GetComponent<Rocket> ().ApplyDamage (weaponDmg);
			}

			Debug.DrawLine (firePointPosition, hit.point, Color.red);

		}
	}

	public void ApplyDamage(float damage)
	{
		curHealth -= damage;
		if (curHealth <= 0) 
		{
			anim.SetTrigger ("Dead");
			dead = true;
			pauseNumOne ();
		}
	}

	public bool loseMoney(int cost){
		int goldTemp = gold;
		if ((gold -= cost) >= 0) {
			gold -= cost;
			return true;
		}
		gold = goldTemp;
		return false;
	}

	public void gainMoney(int amount){
		gold += amount;
	}

	public int getGold(){
		return gold;
	}

	public void setGold(int num){
		gold = num;
	}

	public bool loseAmmo(string gun){
		if (gun == "pistol"){
			return true;
		}
		if (gun == "rifle") {
			if (rAmmo == 0) {
				return false;
			}
			rAmmo -= 1;
			setAmmo (HUD.ammoCount, rAmmo);
			return true;
		}
		if (gun == "shotgun") {
			if (sAmmo == 0) {
				return false;
			}
			sAmmo -= 1;
			setAmmo (HUD.ammoCount, sAmmo);
			return true;
		}
		return false;
	}

	public void buyRammo(){
		rAmmo += 20;
	}

	public void buySammo(){
		sAmmo += 20;
	}

	public void pauseNumZero(){
		pauseNum = 0;
	}

	public void pauseNumOne(){
		pauseNum = 1;
	}

	public int getPausenum(){
		return pauseNum;
	}

	public int getAmmo(int num){
		if (num == 0) {
			return 999;
		}else if (num == 1) {
			return rAmmo;
		}else if (num == 2) {
			return sAmmo;
		}
		return -1;
	}

	public void setAmmo(Text ammoCount, int count){
		ammoCount.text = count.ToString ();
	}

	public void setInControlls(bool ifIn){
		inControlls = ifIn;
	}
}
