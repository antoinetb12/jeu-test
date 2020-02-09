using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
public class CarteControlleur : MonoBehaviour
{
    [Serializable]
    public class Count
    {
        public int minimum;
        public int maximum;
        public Count(int min,int max)
        {
            minimum = min;
            maximum = max;
        }
    }
    public int colums = 8;
    public int rows = 8;
    public Count murCount = new Count(5, 9);
    public Count foodCount = new Count(5, 9);
    public GameObject exit;
    public GameObject[] sol;
    public GameObject[] mur;
    public GameObject[] food;
    public GameObject[] ennemi;
    public GameObject[] murExterieur;
    public Text t;

    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();
    void initialiseList()
    {
        gridPositions.Clear();
        for(int x=1; x < colums - 1; x++)
        {
            for (int y = 1; y < rows - 1; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }
    Vector3 randomPosition()
    {
        int randomIndex = Random.Range(0, gridPositions.Count);
        Vector3 randomPosition = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex);
        return randomPosition;
    }
    void afficheObjet(GameObject[] affichage,int minimum,int maximum)
    {
        int nbObject = Random.Range(minimum, maximum + 1);
        Vector3 randomPositio;
        for(int i=0; i<nbObject; i++)
        {
            randomPositio = randomPosition();
            GameObject affichageChoisi = affichage[Random.Range(0, affichage.Length)];
            Instantiate(affichageChoisi, randomPositio, Quaternion.identity);
        }
    }
    void boardSetup()
    {
        boardHolder = new GameObject("Board").transform;
        for (int x = -1; x < colums + 1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {
                GameObject aintancier;
                if (x==-1 ||y==-1 || x==colums || y == rows)
                {
                    aintancier=murExterieur[Random.Range(0, murExterieur.Length)];
                }
                else
                {
                    aintancier = sol[Random.Range(0, sol.Length)];

                }
                GameObject instance = Instantiate(aintancier, new Vector3(x, y, 0f), Quaternion.identity);
                instance.transform.SetParent(boardHolder);
            }
        }
    }
    // Start is called before the first frame update
    public void SetupScene(int level)
    {
        boardSetup();
        initialiseList();
        afficheObjet(mur, murCount.minimum,murCount.maximum);
        afficheObjet(food, foodCount.minimum, foodCount.maximum);
        int ennemyCount = (int)Mathf.Log(level, 2f);
        afficheObjet(ennemi, ennemyCount, ennemyCount);
        Instantiate(exit, new Vector3(colums - 1, rows - 1, 0F), Quaternion.identity);
    }
}
