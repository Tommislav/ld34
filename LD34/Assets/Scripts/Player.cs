using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {


	private float thrustTime = -1;
	private float shootTime = -1;
	private bool disassembleChecked;

	private bool thrustIsDown;
	private bool thrustWasDown;
	private bool shootIsDown;
	private bool shootWasDown;
	private bool disassembleIsDown;

	private Rigidbody2D _rb;
	private Vector2 _velocity;
	private float _thrustForce = 0f;
	private float _maxThrustForce = 0.5f;
	private float _thrustAdd = 0.01f;
	private float _thrustDecay = 0.97f;

	private ParticleSystem _thrust;
	private ParticleSystem _disassemble;

	private float _maxVel = 0.5f;

	public void AddThrust() {

	}

	public void Shoot() {
		BulletSpawner[] spawners = GetComponentsInChildren<BulletSpawner>();
		for(int i=0; i<spawners.Length; i++) {
			spawners[i].Fire();
		}
	}

	public void Disassemble() {
		_disassemble.Play();
		Game.Instance.Disassemble();
	}



	void Awake() {
		_rb = GetComponent<Rigidbody2D>();
		_thrust = transform.FindChild("thrust").gameObject.GetComponent<ParticleSystem>();
		_disassemble = transform.FindChild("disassemble").gameObject.GetComponent<ParticleSystem>();
		_velocity = new Vector2();
	}


	void Start () {
	
	}
	
	void Update () {

		thrustWasDown = thrustIsDown;
		thrustIsDown = Input.GetKey(KeyCode.Z);
		shootWasDown = shootIsDown;
		shootIsDown = Input.GetKey(KeyCode.X);
		
		if (!shootIsDown && !thrustIsDown) { disassembleIsDown = false; }

		if (shootIsDown && thrustIsDown && !disassembleChecked) {
			disassembleChecked = true;
			if (Mathf.Abs(shootTime - thrustTime) < 0.1f) {
				Disassemble();
				disassembleIsDown = true;
			}
		}

		if (disassembleIsDown) {
			thrustIsDown = shootIsDown = false;
		}

		bool thrustStarted = thrustIsDown && !thrustWasDown;
		bool thrustEnded = !thrustIsDown && thrustWasDown;
		bool shootStarted = shootIsDown && !shootWasDown;
		
		if (shootStarted) {
			shootTime = Time.realtimeSinceStartup;
			disassembleChecked = false;
		}

		if (thrustStarted) {
			thrustTime = Time.realtimeSinceStartup;
			disassembleChecked = false;

			_thrust.Play();
		}
		else if (thrustEnded) {
			_thrust.Stop();
		}
		

		
		
		if (shootIsDown) {
			Shoot();
		}


		_velocity.y = Game.Instance.gravity;

		_thrustForce = thrustIsDown ? Mathf.Clamp(_thrustForce + _thrustAdd, 0f, _maxThrustForce) : _thrustForce * _thrustDecay;
		_velocity.y += _thrustForce;

		
		if (transform.position.y >= Game.Instance.bounds.yMax) {
			transform.position = new Vector3(transform.position.x, Game.Instance.bounds.yMax, transform.position.z);
			
		}

		if (transform.position.y <= Game.Instance.bounds.yMin) {
			transform.position = new Vector3(transform.position.x, Game.Instance.bounds.yMin, transform.position.z);
			
		}

		_velocity.y = Mathf.Clamp(_velocity.y, -_maxVel, _maxVel);
		_rb.velocity = _velocity;


		Vector3 p = transform.position;
		p = new Vector3(p.x + _velocity.x, p.y + _velocity.y, 0);
		transform.position = p;
		
	}

	private void OnHitByBullet(Bullet b) {
		Game.Instance.OnPlayerDamage();
	}
}
