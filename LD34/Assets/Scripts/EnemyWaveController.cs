using UnityEngine;
using System.Collections;

public class EnemyWaveController : MonoBehaviour {

	public int currentWave;
	private int enemyCount;

	private float startLeft;
	private float startRight;
	private float startTop;
	private float startBottom;
	private float centerX;
	private float centerY;
	private float edgeLeft;
	private float edgeRight;
	private float edgeTop;
	private float edgeBottom;



	void Start () {
		currentWave = 0;
		
		Game.Bounds b = Game.Instance.bounds;
		
		startLeft = b.outLeft;
		startRight = b.outRight;
		startTop = b.outTop;
		startBottom = b.outBottom;
		centerX = b.centerX;
		centerY = b.centerY;
		edgeLeft = b.l1;
		edgeRight = b.r1;
		edgeTop = b.t1;
		edgeBottom = b.b1;

		StartNewWave();
	}
	
	void Update () {
	
	}

	void OnEnemyKilled(Enemy e) {
		e.OnEnemyKilled -= OnEnemyKilled;
		enemyCount--;

		if (enemyCount <= 0) {
			currentWave++;
			StartCoroutine(StartNewWaveDelayed(4));
		}
	}

	private IEnumerator StartNewWaveDelayed(float delay) {
		yield return new WaitForSeconds(delay);
		StartNewWave();
	}


	void StartNewWave() {
		Debug.Log("Start new wave: " + currentWave);
		
		if (currentWave == 0) {

			float fleeDelay = 8f;
			MoveShip(createEnemy(0, startLeft, startTop)).AddToPath(0, edgeLeft, edgeTop, 2f).FleeOnDone(fleeDelay);
			MoveShip(createEnemy(0, startLeft, startTop)).AddToPath(0, edgeLeft, edgeBottom, 2f).FleeOnDone(fleeDelay);

			MoveShip(createEnemy(0, startRight, startTop)).AddToPath(0, edgeRight, edgeTop, 2f).FleeOnDone(fleeDelay);
			MoveShip(createEnemy(0, startRight, startTop)).AddToPath(0, edgeRight, edgeBottom, 2f).FleeOnDone(fleeDelay);
		}
		else if (currentWave == 1) {
			float fleeDelay = 8f;
			MoveShip(createEnemy(0, startLeft, startTop)).AddToPath(0, edgeLeft, edgeTop, 2f).FleeOnDone(fleeDelay);
			MoveShip(createEnemy(0, startLeft, startTop)).AddToPath(0, edgeLeft, centerY, 2f).FleeOnDone(fleeDelay);
			MoveShip(createEnemy(0, startLeft, startTop)).AddToPath(0, edgeLeft, edgeBottom, 2f).FleeOnDone(fleeDelay);

			MoveShip(createEnemy(0, startRight, startTop)).AddToPath(0, edgeRight, edgeTop, 2f).FleeOnDone(fleeDelay);
			MoveShip(createEnemy(0, startRight, startTop)).AddToPath(0, edgeRight, centerY, 2f).FleeOnDone(fleeDelay);
			MoveShip(createEnemy(0, startRight, startTop)).AddToPath(0, edgeRight, edgeBottom, 2f).FleeOnDone(fleeDelay);
		}
	}

	private GameObject createEnemy(int type, float x, float y) {
		enemyCount++;
		GameObject go = GameObject.Instantiate(Game.Instance.enemyRegistry[type]);
		go.layer = Game.Instance.enemyLayer;
		go.transform.SetParent(Game.Instance.transform);
		go.transform.position = new Vector3(x, y);
		go.GetComponent<Enemy>().OnEnemyKilled += OnEnemyKilled;
		facePlayerX(go);
		return go;
	}

	private GameObject lookAtPlayer(GameObject go) {
		go.AddComponent<LookAtPlayer>();
		return go;
	}

	

	private GameObject facePlayerX(GameObject go) {
		if (go.transform.position.x < Game.Instance.bounds.centerX) {
			go.transform.Rotate(0, 0, 180);
		}
		return go;
	}

	private MoveShip MoveShip(GameObject go) {
		return go.AddComponent<MoveShip>();
	}

	private void OnDestroy() {
		StopAllCoroutines();
	}
}
