using UnityEngine;
using System.Collections;

public class EnemyWaveController : MonoBehaviour {

	public int currentWave;
	private int enemyCount;
	


	void Start () {
		currentWave = 0;
		
		Game.Bounds b = Game.Instance.bounds;
		
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
			createEnemy(0).AddComponent<WaveSineSideToSide>().Init(0, WaveBase.Dir.left, 0);
			createEnemy(1).AddComponent<WaveSineSideToSide>().Init(0, WaveBase.Dir.left, 4).AddComponent<LookAtPlayer>();
			createEnemy(2).AddComponent<WaveSineSideToSide>().Init(1, WaveBase.Dir.left, 8);
		}

		else if (currentWave %2 == 1) {


			createEnemy(0).AddComponent<WaveZigZagTopDown>().Init(0, WaveBase.Dir.left, 0);
			createEnemy(1).AddComponent<WaveZigZagTopDown>().Init(0, WaveBase.Dir.left, 2);
			createEnemy(2).AddComponent<WaveZigZagTopDown>().Init(0, WaveBase.Dir.left, 4);
			createEnemy(1).AddComponent<WaveZigZagTopDown>().Init(0, WaveBase.Dir.left, 6);
			createEnemy(2).AddComponent<WaveZigZagTopDown>().Init(0, WaveBase.Dir.left, 8);


		} else if (currentWave % 2 == 0) {
			createEnemy(0).AddComponent<WaveZigZagTopDown>().Init(0, WaveBase.Dir.right, 0);
			createEnemy(1).AddComponent<WaveZigZagTopDown>().Init(0, WaveBase.Dir.right, 2);
			createEnemy(2).AddComponent<WaveZigZagTopDown>().Init(0, WaveBase.Dir.right, 4);
			createEnemy(1).AddComponent<WaveZigZagTopDown>().Init(0, WaveBase.Dir.right, 6);
			createEnemy(2).AddComponent<WaveZigZagTopDown>().Init(0, WaveBase.Dir.right, 8);
		}
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
