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
		
		//if (currentWave == 0) {


			createEnemy(0).AddComponent<WaveA>().Init(0, WaveBase.Dir.left, 0);
			createEnemy(0).AddComponent<WaveA>().Init(1, WaveBase.Dir.left, 2);
			createEnemy(0).AddComponent<WaveA>().Init(2, WaveBase.Dir.left, 4);
			createEnemy(0).AddComponent<WaveA>().Init(3, WaveBase.Dir.left, 6);
			createEnemy(0).AddComponent<WaveA>().Init(4, WaveBase.Dir.left, 8);


		//}
		//else if (currentWave == 1) {
			
		//}
	}

	private GameObject createEnemy(int type) {
		enemyCount++;
		GameObject go = GameObject.Instantiate(Game.Instance.enemyRegistry[type]);
		go.layer = Game.Instance.enemyLayer;
		go.transform.SetParent(Game.Instance.transform);
		//go.transform.position = new Vector3(x, y);
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
