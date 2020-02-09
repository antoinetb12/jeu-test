using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MovingObject
{
    public int food=100;
    public int wallDamage = 1;
    public int pointsPerFood = 10;
    public int pointsPerSoda = 20;
    public float restartLevelDelay = 1f;

    private Animator animator;
    private int foodScore;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "sortie")
        {
            Invoke("restart", restartLevelDelay);
        }else if (collision.tag == "nourriture")
        {
            food += pointsPerFood;
            collision.gameObject.SetActive(false);
        }
    }
    protected override void OnCantMove<T>(T component)
    {
        Wall hitWall = component as Wall;
        hitWall.DamageWall(wallDamage);
        animator.SetTrigger("playerAttaque");
    }
    private void restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
    public void loseFood(int loss)
    {
        animator.SetTrigger("playerHit");
        food -= loss;
        CheckIfGameOver();
    }
    // Start is called before the first frame update
    protected override void Start()
    {
        animator = GetComponent<Animator>();

        base.Start();
    }
    private void OnDisable()
    {
            
    }
    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        food--;
        base.AttemptMove<T>(xDir, yDir);
        CheckIfGameOver();
        ControlleurJeu.instance.playerTurn = false;


    }
    private void CheckIfGameOver()
    {
        if (food <= 0)
        {
            ControlleurJeu.instance.gameOver();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!ControlleurJeu.instance.playerTurn) 
            return;
        int horizontal = 0;
        int vertical = 0;
        horizontal = (int)Input.GetAxisRaw("Horizontal");
        vertical = (int)Input.GetAxisRaw("Vertical");
        if (horizontal != 0)
        {
            vertical = 0;
        }
        if(horizontal!=0 || vertical != 0)
        {
            AttemptMove<Wall>(horizontal, vertical);
        }
    }
   
}
