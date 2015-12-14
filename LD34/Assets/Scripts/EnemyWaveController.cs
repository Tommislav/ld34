using UnityEngine;
using System.Collections;

public class EnemyWaveController : MonoBehaviour {

	public int currentWave;
	public HintScript hint;
	private int enemyCount;
	


	void Start () {
#if !UNITY_EDITOR
		currentWave = 0;
#endif

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

		if (currentWave % 3 == 0) {
			Game.Instance.AddHealth();
		}

		Game.Instance.SetCurrentWave(currentWave);

		if (currentWave == 0) {
			hint.ShowHint("WELCOME! LETS START RIGHT AWAY!");

			createEnemy(2).AddComponent<WaveZigZagTopDown>().SetExtraProp(0.5f).Init(0, WaveBase.Dir.left, 1f);
			createEnemy(2).AddComponent<WaveZigZagTopDown>().SetExtraProp(0.5f).Init(0, WaveBase.Dir.right, 3f);

			createEnemy(2).AddComponent<WaveZigZagTopDown>().SetExtraProp(0.5f).Init(0, WaveBase.Dir.left, 6f);
			createEnemy(2).AddComponent<WaveZigZagTopDown>().SetExtraProp(0.5f).Init(0, WaveBase.Dir.right, 8f);

			createEnemy(2).AddComponent<WaveZigZagTopDown>().SetExtraProp(0.5f).Init(0, WaveBase.Dir.left, 11f);
			createEnemy(2).AddComponent<WaveZigZagTopDown>().SetExtraProp(0.5f).Init(0, WaveBase.Dir.right, 13f);

		} else if (currentWave == 1) {

			hint.ShowHint("Z + X TO DISASSEMBLE! DO IT IF YOU GROW OUT OF CONTROL");

			createEnemy(2).AddComponent<WaveOneLapAround>().Init(1, WaveBase.Dir.right, 1);
			createEnemy(2).AddComponent<WaveOneLapAround>().Init(3, WaveBase.Dir.right, 1);

			createEnemy(2).AddComponent<WaveOneLapAround>().Init(1, WaveBase.Dir.right, 5f);
			createEnemy(2).AddComponent<WaveOneLapAround>().Init(3, WaveBase.Dir.right, 5f);

			createEnemy(1).AddComponent<WaveSineSideToSide>().Init(0, WaveBase.Dir.left, 6);
			createEnemy(1).AddComponent<WaveSineSideToSide>().Init(0, WaveBase.Dir.left, 7);
			createEnemy(1).AddComponent<WaveSineSideToSide>().Init(0, WaveBase.Dir.left, 8);

			createEnemy(2).AddComponent<WaveOneLapAround>().Init(1, WaveBase.Dir.right, 9f);
			createEnemy(2).AddComponent<WaveOneLapAround>().Init(3, WaveBase.Dir.right, 9f);

		} else if (currentWave == 2) {

			hint.ShowHint("THESE DUMMY OBJECTS WILL CLOG YOUR SHIP");

			for (int i = 0; i < 10; i++) {
				createEnemy(3).AddComponent<WaveZigZagTopDown>().SetExtraProp(0.05f).Init(1, WaveBase.Dir.left, (i * 0.3f));
				createEnemy(3).AddComponent<WaveZigZagTopDown>().SetExtraProp(0.05f).Init(1, WaveBase.Dir.right, (5 + i * 0.3f));
			}

			createEnemy(1).AddComponent<WavePeakInFromSide>().SetExtraProp(4f).Init(3, WaveBase.Dir.left, 6f).AddComponent<LookAtPlayer>();
			createEnemy(1).AddComponent<WavePeakInFromSide>().SetExtraProp(4f).Init(6, WaveBase.Dir.right, 10f).AddComponent<LookAtPlayer>();

		} else if (currentWave == 3) {

			hint.ShowHint("EVERY 3 WAVES YOU'LL GET +1 HEALTH!");

			createEnemy(2).AddComponent<WavePeakInFromSide>().SetExtraProp(4f).Init(0, WaveBase.Dir.left, 2f);
			createEnemy(2).AddComponent<WavePeakInFromSide>().SetExtraProp(4f).Init(1, WaveBase.Dir.right, 2.5f);
			createEnemy(2).AddComponent<WavePeakInFromSide>().SetExtraProp(4f).Init(2, WaveBase.Dir.left, 3f);
			createEnemy(2).AddComponent<WavePeakInFromSide>().SetExtraProp(4f).Init(3, WaveBase.Dir.right, 3.5f);
			createEnemy(2).AddComponent<WavePeakInFromSide>().SetExtraProp(4f).Init(4, WaveBase.Dir.left, 3.5f);
			createEnemy(2).AddComponent<WavePeakInFromSide>().SetExtraProp(4f).Init(5, WaveBase.Dir.right, 4f);
			createEnemy(2).AddComponent<WavePeakInFromSide>().SetExtraProp(4f).Init(6, WaveBase.Dir.left, 4.5f);

		} else if (currentWave == 4) {
			hint.ShowHint("BIG ENEMIES MAKES YOU A BIGGER TARGET");

			createEnemy(2).AddComponent<WaveOneLapAround>().Init(0, WaveBase.Dir.right, 0);
			createEnemy(2).AddComponent<WaveOneLapAround>().Init(1, WaveBase.Dir.right, 0);
			createEnemy(2).AddComponent<WaveOneLapAround>().Init(2, WaveBase.Dir.right, 0);
			
			createEnemy(2).AddComponent<WaveOneLapAround>().Init(0, WaveBase.Dir.right, 1);
			createEnemy(2).AddComponent<WaveOneLapAround>().Init(1, WaveBase.Dir.right, 1);
			createEnemy(2).AddComponent<WaveOneLapAround>().Init(2, WaveBase.Dir.right, 1);
			
			createEnemy(2).AddComponent<WaveOneLapAround>().Init(0, WaveBase.Dir.right, 2);
			createEnemy(2).AddComponent<WaveOneLapAround>().Init(1, WaveBase.Dir.right, 2);
			createEnemy(2).AddComponent<WaveOneLapAround>().Init(2, WaveBase.Dir.right, 2);
			
		} else if (currentWave == 5) {

			createEnemy(0).AddComponent<WaveOneLapAround>().Init(2, WaveBase.Dir.right, 0);
			

		} else if (currentWave == 6) {

			hint.ShowHint("BOSS!!!");

			for (int i = 0; i < 30; i++) {
				createEnemy(3).AddComponent<WaveShield>().Init(0, WaveBase.Dir.right, (i * 0.3f) + Random.Range(-0.1f, 0.45f));

			}

			createEnemy(0).AddComponent<WavePeakInFromSide>().SetIsBoss().SetExtraProp(50f).Init(3, WaveBase.Dir.right, 8f).AddComponent<LookAtPlayer>();

			
		} else {

			bool isBossWave = (currentWave % 6 == 0);

			if (currentWave == 7) {
				hint.ShowHint("FROM NOW ON WAVES ARE RANDOMLY GENERATED!");
			} else if (isBossWave) {
				hint.ShowHint("BOSS!!!");
			} else {
				hint.ShowHint("WAVE " + currentWave);
			}


			if (isBossWave) {
				for (int i = 0; i < 30; i++) {
					createEnemy(3).AddComponent<WaveShield>().Init(0, WaveBase.Dir.left, (i * 0.3f) + Random.Range(-0.1f, 0.45f));
					createEnemy(3).AddComponent<WaveShield>().Init(0, WaveBase.Dir.right, (i * 0.3f) + Random.Range(-0.1f, 0.45f));
				}

				createEnemy(0).AddComponent<WavePeakInFromSide>().SetIsBoss().SetExtraProp(50f).Init(Random.Range(0,6), WaveBase.Dir.left, 8f).AddComponent<LookAtPlayer>();
				createEnemy(0).AddComponent<WavePeakInFromSide>().SetIsBoss().SetExtraProp(50f).Init(Random.Range(0, 6), WaveBase.Dir.right, 8f).AddComponent<LookAtPlayer>();

				return;
			}



			float rnd = Random.Range(0f,1f);
			
			if (rnd < 0.2f) {
				int num = Random.Range(5, 10);
				float delay = Random.Range(0.5f, 3f);

				for (int i = 0; i < num; i++) {
					GameObject go = createRandomEnemy().AddComponent<WaveZigZagTopDown>().Init(0, WaveBase.Dir.left, i * delay);
					
				}
			}
			else if (rnd < 0.4f) {
				int num = Random.Range(5, 10);
				float delay = Random.Range(0.5f, 3f);

				for (int i=0; i < num; i++) {
					GameObject go = createRandomEnemy().AddComponent<WaveZigZagTopDown>().Init(0, WaveBase.Dir.right, i*delay);
				}
			}
			else if (rnd < 0.6f) {
				createRandomEnemy().AddComponent<WavePeakInFromSide>().SetExtraProp(Random.Range(1f, 5f)).Init(0, WaveBase.Dir.left, 2f);
				createRandomEnemy().AddComponent<WavePeakInFromSide>().SetExtraProp(Random.Range(1f, 5f)).Init(1, WaveBase.Dir.right, 2.5f);
				createRandomEnemy().AddComponent<WavePeakInFromSide>().SetExtraProp(Random.Range(1f, 5f)).Init(2, WaveBase.Dir.left, 3f);
				createRandomEnemy().AddComponent<WavePeakInFromSide>().SetExtraProp(Random.Range(1f, 5f)).Init(3, WaveBase.Dir.right, 3.5f);
				createRandomEnemy().AddComponent<WavePeakInFromSide>().SetExtraProp(Random.Range(1f, 5f)).Init(4, WaveBase.Dir.left, 3.5f);
				createRandomEnemy().AddComponent<WavePeakInFromSide>().SetExtraProp(Random.Range(1f, 5f)).Init(5, WaveBase.Dir.right, 4f);
				createRandomEnemy().AddComponent<WavePeakInFromSide>().SetExtraProp(Random.Range(1f, 5f)).Init(6, WaveBase.Dir.left, 4.5f);
			}
			else if (rnd < 1f) {

				int index = Random.Range(0, 3);
				createRandomEnemy().AddComponent<WaveOneLapAround>().Init(index, WaveBase.Dir.right, 0);
				createRandomEnemy().AddComponent<WaveOneLapAround>().Init(index, WaveBase.Dir.right, 1);
				createRandomEnemy().AddComponent<WaveOneLapAround>().Init(index, WaveBase.Dir.right, 2);
				createRandomEnemy().AddComponent<WaveOneLapAround>().Init(index, WaveBase.Dir.right, 3);
			}
			else if (rnd < 1f) {

			}


			if (Random.value < 0.1f) {
				createRandomEnemy().AddComponent<WaveSineSideToSide>().Init(0, WaveBase.Dir.left, Random.Range(1f, 10f));
			}

		}
	}

	private GameObject createRandomEnemy() {
		int[] types = { 0, 1, 1, 1, 2, 2, 2, 2, 3, 3 };
		int i = Random.Range(0, types.Length-1);
		GameObject go = createEnemy(types[i]);
		if (Random.value < 0.05f) {
			go.AddComponent<LookAtPlayer>();
		}
		return go;
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
