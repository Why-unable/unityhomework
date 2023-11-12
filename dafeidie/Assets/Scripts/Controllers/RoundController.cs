using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class RoundController : MonoBehaviour, ISceneController, IUserAction
{
    int round = 0;
    int max_round = 5;
    float timer = 0.5f;
    GameObject disk;
    DiskFactory factory ;
    public CCActionManager actionManager;
    public ScoreController scoreController;
    public UserGUI userGUI;

    //before Start
    void Awake() {
        SSDirector director = SSDirector.getInstance();
        director.currentSceneController = this;
        director.currentSceneController.LoadSource();
        
        //Add同时赋值给RoundController的CCAcM、ScoreC
        gameObject.AddComponent<CCActionManager>();
        gameObject.AddComponent<ScoreController>();

        gameObject.AddComponent<DiskFactory>();
        factory = Singleton<DiskFactory>.Instance;
        gameObject.AddComponent<UserGUI>();
        userGUI = gameObject.GetComponent<UserGUI>();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (userGUI.mode == 0) return;
        GetHit();
        gameOver();
        if (round > max_round) {
            return;
        }
        timer -= Time.deltaTime;
        if (timer <= 0 && actionManager.RemainActionCount() == 0) {
            //从工厂中得到10个飞碟，为其加上动作
            for (int i = 0; i < 10; ++i) {
                disk = factory.GetDisk(round);
                actionManager.MoveDisk(disk);
            }
            round += 1;
            if (round <= max_round) {
                userGUI.round = round;
            }
            timer = 4.0f;
        }
        
    }

    public void LoadSource() 
    {

    }

    public void gameOver() 
    {
        if (round > max_round && actionManager.RemainActionCount() == 0)
            userGUI.gameMessage = "THE END";
    }

    public void GetHit() {
        if (Input.GetButtonDown("Fire1")) {

			Vector3 mp = Input.mousePosition; //get Screen Position

			//create ray, origin is camera, and direction to mousepoint
			Camera ca = Camera.main;
			Ray ray = ca.ScreenPointToRay(Input.mousePosition);

			//Return the ray's hit
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)) {
                //点中飞碟则加分
                scoreController.Record(hit.transform.gameObject);
                //设为不可见
                hit.transform.gameObject.SetActive(false);
			}
            else{
                //没点中飞碟则惩罚
                UnityEngine.Debug.Log("nope");
                scoreController.Publish();
            }
		}
    }
}
