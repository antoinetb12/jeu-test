using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ControlleurJeu : MonoBehaviour
{
    public float levelStartDelay = 2f;
    public float turnDelay = 0.1f;
    private CarteControlleur cartecontrolleur;
    private Player player;
    public static ControlleurJeu instance = null;
    private int level = 3;
    private List<Ennemi> ennemies;
    private bool ennemiesMoving;
    public bool playerTurn=true;
    private bool doingSetup = true;
    private Text levelText;                                    //Text to display current level number.
    private GameObject levelImage;
    // Start is called before the first frame update
    void Awake()
    {
        player = GetComponent<Player>();
        ennemies = new List<Ennemi>();
        cartecontrolleur = GetComponent<CarteControlleur>();
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);

        //Get a component reference to the attached BoardManager script
        cartecontrolleur = GetComponent<CarteControlleur>();

        //Call the InitGame function to initialize the first level 
        InitGame();
    }
        void InitGame()
        {
            doingSetup = true;
            levelImage = GameObject.Find("levelImage");
            levelText = GameObject.Find("LevelText").GetComponent<Text>();
            levelText.text = "Jour " + level;
            levelImage.SetActive(true);
            Invoke("HideLevelImage", levelStartDelay);
            ennemies.Clear();
            //Call the SetupScene function of the BoardManager script, pass it current level number.
            cartecontrolleur.SetupScene(level);

        }
    
    void HideLevelImage()
    {
        //Disable the levelImage gameObject.
        levelImage.SetActive(false);

        //Set doingSetup to false allowing player to move again.
        doingSetup = false;
    }
    public void addEnnemiesToList(Ennemi script)
    {
        ennemies.Add(script);
    }
    IEnumerator MoveEnnemies()
    {
        ennemiesMoving = true;
        yield return new WaitForSeconds(turnDelay);
     /*   if (ennemies.Count == 0)
        {
            yield return new WaitForSeconds(turnDelay);
        }*/
        for(int i = 0; i < ennemies.Count; i++)
        {
            ennemies[i].MoveEnnemy();
            yield return new WaitForSeconds(ennemies[i].moveTime);
        }
        playerTurn = true;
        ennemiesMoving = false;
    }
    public void gameOver()
    {
        enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
       if(playerTurn || ennemiesMoving)
        {
            return;
        }
        StartCoroutine(MoveEnnemies());

    }
}
