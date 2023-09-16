using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    // since , unity doesn't support serializing multidimensional arrays,
    // we have to use multiple single dimensional arrays & join them
    public GameObject[] row1;
    public GameObject[] row2;
    public GameObject[] row3;
    public GameObject[] row4;
    public GameObject[] row5;
    public GameObject[] row6;
    public GameObject[] row7;
    public GameObject[] row8;

    private Ball[,] balls;
    private string[,] ballTypes;

    public int current_level = 1;
    public int board_size = 5;


    public Ball selected1 = null;
    public Ball selected2 = null;

    HashSet<int[]> myhash = new HashSet<int[]>();

    Hashtable scoreMap = new Hashtable();

    public GameObject ballPrefabParent;

    public Ball[] ballPrefabs;

    public Ball[] r0Prefabs;
    public Ball[] r1Prefabs;
    public Ball[] r2Prefabs;
    public Ball[] r3Prefabs;
    public Ball[] r4Prefabs;
    public Ball[] r5Prefabs;
    public Ball[] r6Prefabs;
    public Ball[] r7Prefabs;



    public string[] targetBalls;
    public int[] targetBallScore;
    public TextMeshProUGUI[] targetTxtScore;


    private bool dropStarted = false;
    private bool dropEnded = false;

    public Ball lastDroppedBall;
    public Sprite[] sprites;

    public int PERCENTAGE_B = 40;
    public int PERCENTAGE_F = 10;
    public int PERCENTAGE_G = 30;
    public int PERCENTAGE_T = 20;


    public GameObject dialogYouWin;
    public GameObject dialogGameOver;

    public Transform[] starting_pos_y;

    public float timeout_seconds = 60;
    private float start_time;
    public TextMeshProUGUI txtTimeRemaining;

    public GameObject[] progressBars;

    public bool checkedForOrder = false;

    public GameObject dummyHigherSortingParent;
    public bool isGameFinished;


    public GameObject[] x0Blocks;
    public GameObject[] x2Blocks;
    public GameObject[] x3Blocks;

    GameObject[][] allWoods;

    public GameObject[] woods0;
    public GameObject[] woods1;
    public GameObject[] woods2;
    public GameObject[] woods3;
    public GameObject[] woods4;
    public GameObject[] woods5;
    public GameObject[] woods6;
    public GameObject[] woods7;


    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        start_time = Time.time;
        balls = new Ball[board_size, board_size];
        ballTypes = new string[board_size, board_size];

        addAllBalls();
        processWoods();
        setIntialScores();
        updatePercentages();
        getRowStr(balls, null);
        
    }



    public float delayStart = 0;


    public bool inDelay()
    {
        if (delayStart == 0)
            return false;
        else if (Time.time - delayStart > 1f)
        {
            delayStart = 0;
            return false;
        }
        else
            return true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameFinished)
        {

            if (inDelay())
            {
                return;
            }

            handleTiming();
            if (lastDroppedBall != null && !checkedForOrder)
            {
                checkAndFixOrder();
                //            checkAndFixOrder2();

            }

            if (isCheckNeeded())
            {
                
                //startChecks - checkImpact
                lastDroppedBall = null;
                dropStarted = false;
                dropEnded = false;

                /*            if (allStopped())
                            {*/
                //                Thread.Sleep(2000);
                checkImpact();
                checkedForOrder = false;
                //}
                /*            else
                            {
                                
                            }*/

            }
            else
            {
//                
            }
        }
    }

    private void updatePercentages()
    {
        PERCENTAGE_F = PERCENTAGE_B + PERCENTAGE_F;
        PERCENTAGE_G = PERCENTAGE_F + PERCENTAGE_G;
        PERCENTAGE_T = PERCENTAGE_G + PERCENTAGE_T;

    }

    private void handleTiming()
    {
        float now = Time.time;

        if (now - start_time > timeout_seconds)
        {
            // show you loose dialog
            dialogGameOver.SetActive(true);
            isGameFinished = true;
//            
            return;
        }

        int min = (((int)(now - start_time)) / 60);
        int sec = (((int)(now - start_time)) % 60);
        string timeRemaining = ((int)(timeout_seconds / 60 ) - min -1) + ":" + ((int)(59 - sec)).ToString().PadLeft(2, '0');
        txtTimeRemaining.text = timeRemaining;


        int sec_remaining = (min * 60) + sec;

        int progress = (int)((sec_remaining / timeout_seconds) * 100) / 20;

        for (int i = 0; i < progressBars.Length; i++)
        {
            if(i == progress)
            {
                progressBars[i].SetActive(true);
            }
            else
                progressBars[i].SetActive(false);

        }

    }

    private void checkAndFixOrder()
    {
        //

        Ball[,] for_sort = balls;
        for (int i = 0; i < for_sort.GetLength(0); i++)
        {
            float prev = for_sort[0, i].transform.position.y;
      //      
            for (int j = 1; j < balls.GetLength(1); j++)
            {
//            

                float diff = Math.Abs(prev - for_sort[j, i].transform.position.y);
                if (prev > for_sort[j, i].transform.position.y)
                {
  //                  
                }
                else
                {
//                    
                    if(diff <= 0.1f)
                    {
                        Vector3 new_p = for_sort[j - 1, i].transform.position;
                        new_p.y = new_p.y - 0.7f;
                        for_sort[j - 1, i].transform.position = for_sort[j-1, i].transform.position + (Vector3.up * 0.7f);
                     //   
                    }
                }

                prev = for_sort[j, i].transform.position.y;

 /*               
                if (balls[j, i].type != "x")
                {
                    if (balls[j, i].gameObject.GetComponent<Rigidbody2D>().velocity == Vector2.zero)
                    {
                        Vector3 ballPos = balls[j, i].transform.position;
                        if (Math.Abs(ballPos.y - starting_pos_y[j]) > diff)
                        {
                            
//                            ballPos.y = starting_pos_y[j];
//                            balls[j, i].transform.position = Vector3.zero;
                         //   balls[j,i].gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
                        }
                    }
                }*/

                /*                if (balls[j, i] == null)
                                {
                                    
                                }
                                else
                                {
                                    
                                }
                */
              }
          //  
        }


        balls = for_sort;
        //
        //getRowStr(balls,null);

        checkedForOrder = true;
    }

    private bool isCheckNeeded()
    {
        if (lastDroppedBall != null)
        {
//            
        }

/*        
        
        */
        if (lastDroppedBall != null)
        {
            
            

            
        }

        //     return  dropStarted && dropEnded && lastDroppedBall!= null && lastDroppedBall.GetComponent<Rigidbody2D>().velocity == Vector2.zero;
        return dropStarted && dropEnded && lastDroppedBall != null && lastDroppedBall.intial_destination == 8888;

    }

    private void setIntialScores()
    {
        scoreMap.Add("f", 0);
        scoreMap.Add("g", 0);
        scoreMap.Add("b", 0);
        scoreMap.Add("t", 0);
        scoreMap.Add("w", 0);
    }

    private void updateScore()
    {
        for (int i = 0; i < targetBallScore.Length; i++)
        {
            
            targetTxtScore[i].text = scoreMap[targetBalls[i]] + "/"+targetBallScore[i];
        }


    }

    private void addAllBalls()
    {

        addToBalls(row1, 0);
        addToBalls(row2, 1);
        addToBalls(row3, 2);
        addToBalls(row4, 3);
        addToBalls(row5, 4);

        if (board_size >= 6)
            addToBalls(row6, 5);
        if (board_size >= 7)
            addToBalls(row7, 6);
        if (board_size >= 8)
            addToBalls(row8, 7);



    }

    // adds the balls in the specified row to the balls array, 
    private void addToBalls(GameObject[] row, int row_number)
    {
        for (int i = 0; i < row.Length; i++)
        {
            Ball ball = row[i].GetComponent<Ball>();
            balls[row_number, i] = ball;
            ballTypes[row_number, i] = balls[row_number, i].type;

            ball.i = row_number;
            ball.j = i;
        }
    }

    public void checkImpact()
    {
        Ball old_selected1 = selected1;
        Ball old_selected2 = selected2;

        Ball[,] ballsBeforeSwitch = new Ball[balls.GetLength(0), balls.GetLength(0)];
        ballsBeforeSwitch = balls;
        if(selected1!=null && selected2 != null)
            // a dummy switch on the selected balls
            switchBeforeCheck(selected1, selected2);


        checkHorizontally();
        bool spriteAlreadySwitched = false;
        bool horizantalImpact = myhash.Count > 0;

        if (horizantalImpact)
        {
            
            /*            go_1_pos = old_selected1.transform.position;
                        go_2_pos = old_selected2.transform.position;
            */            //            StartCoroutine(switchSprites(old_selected1.gameObject, old_selected2.gameObject));

            if (selected1 != null && selected2 != null)
            {
                switchPosition(old_selected1, old_selected2);
                spriteAlreadySwitched = true;
            }

            // keep switched and do animation / transform


            ///StartCoroutine(Timer(5,Time.time));
            removeHittedBalls();
            // drop balls
            

        }

        myhash.Clear();

        checkVertically();
        bool verticalImpact = myhash.Count > 0;

        if (verticalImpact)
        {
            
    
            if (selected1 != null && selected2 != null)
                if (spriteAlreadySwitched == false)
                    switchPosition(old_selected1, old_selected2);

            //        StartCoroutine(Timer(5, Time.time));
            removeHittedBalls();
            myhash.Clear();
            
        }



        if (!horizantalImpact && !verticalImpact)
        {
            
            //  make switch back movement
            // set selecteds , to null
            
            //            balls = ballsBeforeSwitch;
            if (selected1 != null && selected2 != null)
                switchBeforeCheck(selected2,selected1);

            
            getRowStr(balls, null);
            

            removeSelection();
            return;
        }
        else
        {
            
            getRowStr(balls, null);
            
        }




//        DropNewBalls();
        if(horizantalImpact || verticalImpact)
        {
//            Thread.Sleep(500);
        }
        else
        {
//            Thread.Sleep(200);
        }
        Drop();
        removeSelection();
//        DoesPlayerWin();
    }

    private void removeSelection()
    {
        if(selected1 != null)
        {
            selected1.removeSelected(selected1.gameObject);
        }
        if(selected2 != null)
        {
            selected2.removeSelected(selected2.gameObject);
        }

        selected1 = null;
        selected2 = null;
    }

    private void switchPosition(Ball old_selected1, Ball old_selected2)
    {
        Vector3 tempBallPos = old_selected1.transform.position;
        old_selected1.transform.position = old_selected2.transform.position;
        old_selected2.transform.position = tempBallPos;

    }

    private void switchBeforeCheck(Ball ball1, Ball ball2)
    {
        int temp_i = selected1.i;
        int temp_j = selected1.j;

        
        

        Ball temp_ball = balls[ball1.i, ball1.j];
        balls[ball1.i, ball1.j] = balls[ball2.i, ball2.j];
        balls[ball2.i, ball2.j] = temp_ball;

    //    string temp_str = ballTypes[selected1.i, selected1.j];
//        ballTypes[selected1.i, selected1.j] = ballTypes[selected2.i, selected2.j];
  //      ballTypes[selected2.i, selected2.j] = temp_str;

    }

    private void checkHorizontally()
    {
        
        
        for (int i = 0; i < balls.GetLength(0); i++)
        {
            string row_str = "";
            for (int j = 0; j < balls.GetLength(0); j++)
            {
                
                row_str = row_str + balls[i, j].type;
//                row_str = row_str + ballTypes[i, j];

            }

            getPoses(row_str, "ggg", i, "horizontal");
            getPoses(row_str, "fff", i, "horizontal");
            getPoses(row_str, "bbb", i, "horizontal");
            getPoses(row_str, "ttt", i, "horizontal");
  //          getPoses(row_str, "zzz", i, "horizontal");

        }
    }
    private void checkVertically()
    {
        for (int i = 0; i < balls.GetLength(0); i++)
        {
            string row_str = "";
            for (int j = 0; j < balls.GetLength(0); j++)
            {
                row_str = row_str + balls[j, i].type;
                //row_str = row_str + ballTypes[i, j];
            }

            

            getPoses(row_str, "ggg", i,"vertical");
            getPoses(row_str, "fff", i,"vertical");
            getPoses(row_str, "bbb", i, "vertical");
            getPoses(row_str, "ttt", i,"vertical");
//            getPoses(row_str, "zzz", i, "vertical");

        }
    }

    private void getPoses(string row_str, string to_find, int pos_index, string direction)
    {

        int i;
        while ((i = row_str.IndexOf(to_find)) != -1)
        {

            char[] arr = row_str.ToCharArray();
            arr[i] = 'x';
            row_str = new string(arr);

            // add neighbouring letters
            for (int j = 0; j < to_find.Length; j++)
            {
                if (direction == "horizontal")
                {
                    myhash.Add(new int[] { pos_index, i + j });
                    
                }
                else
                {
                    myhash.Add(new int[] { i + j, pos_index });
                    
                }
            }
            }
            //            row_str.Replace()
        }
    private bool DoesPlayerWin()
    {
        
        for (int i = 0; i < targetBalls.Length; i++)
        {
            
            
            
            
            
            if ((int)scoreMap[targetBalls[i]] < (int)targetBallScore[i])
            {
                
                return false;
            }
        }

        return true;
    }
    private void removeHittedBalls()
    {
        foreach (var pos in myhash)
        {
            int i_index = pos[0];
            int j_index = pos[1];

            if (balls[i_index, j_index].gameObject.activeSelf)
            {
                //                balls[i_index, j_index].gameObject.SetActive(false);
                balls[i_index, j_index].transform.SetParent(dummyHigherSortingParent.transform);
                balls[i_index, j_index].gameObject.layer = LayerMask.NameToLayer("Ignore Collision");
                //balls[i_index, j_index].transform.localScale = new Vector3(1.2f, 1.2f, 0f);

                if(current_level == 8 || current_level == 9)
                {
                    removeWood(i_index, j_index);
                }

                string type = balls[i_index, j_index].type;
                if (scoreMap.Contains(type))
                {
                    int new_score = ((int)scoreMap[type]) + 1;
                    scoreMap[balls[i_index, j_index].type] = new_score;
                    updateScore();

                    if (DoesPlayerWin() && !isGameFinished)
                    {
                        Helper.upgradeUserLevel();
                        dialogYouWin.SetActive(true);
                        isGameFinished = true;
                    }
                }
            }
        }
        myhash.Clear();

    }

   

    public void Drop()
    {
        
        Ball[,] new_balls = new Ball[board_size, board_size];
        string[,] new_ballTypes = new string[board_size, board_size];

        new_balls =  orderForDropping(new_balls);

        dropStarted = true;
        new_balls = fillAndDrop(new_balls);
        dropEnded = true;
        balls = new_balls;
    }

    private Ball[,] orderForDropping(Ball[,] new_balls)
    {
        Debug.Log("Before ordering");
        getRowStr(balls, null);

        for (int i = 0; i < balls.GetLength(0); i++)
        {
            int n = balls.GetLength(1) - 1;
            string str = "";
            for (int j = balls.GetLength(1) - 1; j >= 0; j--)
            {
                if (balls[j, i].type == "x")
                {
                    //                    
                    str += "x";
                    new_balls[j, i] = balls[j, i];
                }
                else if (!balls[j, i].gameObject.activeSelf || balls[j,i].gameObject.layer == LayerMask.NameToLayer( "Ignore Collision"))
                {
                    //                  
                    str += "!";
                    new_balls[j, i] = null;

                    balls[j, i] = null;

                }
                else
                {
                    //                
                    str += balls[j, i].type;
                    int to_put = j;
                    for (int k = j + 1; k < balls.GetLength(1); k++)
                    {
                        //                  
                        if (balls[k, i] == null || !balls[k, i].gameObject.activeSelf || balls[j, i].gameObject.layer == LayerMask.NameToLayer("Ignore Collision"))
                        {
                            //                    
                            to_put = k;
                        }
                        else if (balls[k, i] == null)
                        {
                            //                  
                            to_put = k;
                        }

                    }

                    if (to_put != j)
                    {
                        //            
                        //           
                        new_balls[to_put, i] = balls[j, i];

                        balls[to_put, i] = new_balls[to_put, i];
                        new_balls[j, i] = null;
                        balls[j, i] = null;

                        //         
                    }
                    else
                        new_balls[j, i] = balls[j, i];
                }
            }
            //            



        }

//        display(new_balls, "AFTER ORDERING");
        Debug.Log("After ordering");
        getRowStr(new_balls, null);

        return new_balls;
    }

    private Ball[,] fillAndDrop(Ball[,] new_balls)
    {
        dropStarted = true;
        getRowStr(new_balls, null);
        
        //fills the discovered vaccant spaces with new balls , by shifting all the balls already there
        // additionally this will benefit on stting the correct information on the balls object attribute
        
        for (int col = 0; col < new_balls.GetLength(0); col++)
        {
            string s = "";
            for (int row = new_balls.GetLength(1) - 1; row >= 0; row--)
            {

                /*                
                                */

                if (new_balls[row, col] == null)
                {
                    


                    Ball new_ball_object = getRandomizedBall(row, col);

                    if (x0Blocks != null)
                    {
                        if (x0Blocks.Length > 0)
                        {
                            
                            if (x0Blocks[col] != null)
                            {
                                
                                if (row > x0Blocks[col].GetComponent<Ball>().i)// may be check pos
                                {
                                    //string new_layer = "x" + col;
                                    string new_layer = "xg";
                                    new_ball_object.gameObject.layer = LayerMask.NameToLayer(new_layer);
                                }
                            }
                            else
                            {
                                
                            }
                        }
                    }


                    ////////////////////////////////////////////////////////////////////
                    if (x2Blocks != null)
                    {
                        if (x2Blocks.Length > 0)
                        {
                            if (x2Blocks[col] != null)
                            {
                                
                                if (row > x2Blocks[col].GetComponent<Ball>().i)
                                {
                                    if (x2Blocks[col].gameObject.transform.position.y < new_balls[row, col].transform.position.y)
                                    {

                                        //                                    string new_layer = "x" + col;
                                        string new_layer = "yg";

                                        new_ball_object.gameObject.layer = LayerMask.NameToLayer(new_layer);
                                    }

                                }
                            }

                        }
                    }




                    if (x3Blocks != null)
                    {
                        if (x3Blocks.Length > 0)
                        {
                            if (x3Blocks[col] != null)
                            {
                                
                                if (row > x3Blocks[col].GetComponent<Ball>().i)
                                {
                                    if (x3Blocks[col].gameObject.transform.position.y < new_balls[row, col].transform.position.y)
                                    {

                                        //                                    string new_layer = "x" + col;
                                        string new_layer = "zg";

                                        new_ball_object.gameObject.layer = LayerMask.NameToLayer(new_layer);
                                    }
                                 
                                }
                            }
                          
                        }
                    }





                    new_ball_object.i = row; // setting the column
                    new_ball_object.j = col; // setting the row
                                             //                    return;

                    new_balls[row, col] = new_ball_object;
                    //                    new_ballTypes[row, col] = new_ball_object.type;


                    s += new_ball_object.type;


                    if (lastDroppedBall == null)
                        lastDroppedBall = new_ball_object;

                    else if (new_ball_object.i <= lastDroppedBall.i)
                    {
                        lastDroppedBall = new_ball_object;
                    }

                }

                else if (new_balls[row, col].gameObject.activeSelf)
                {
                    //                    s += new_ballTypes[row, col];
                    new_balls[row, col].i = row; // setting the column
                    new_balls[row, col].j = col; // setting the row

                    if (x0Blocks != null)
                    {
                        if (x0Blocks.Length > 0)
                        {
                            if (x0Blocks[col] != null)
                            {
                                
                                if (row > x0Blocks[col].GetComponent<Ball>().i)
                                {
                                    if (x0Blocks[col].gameObject.transform.position.y < new_balls[row, col].transform.position.y)
                                    {

                                        //                                    string new_layer = "x" + col;
                                        string new_layer = "xg";

                                        new_balls[row, col].gameObject.layer = LayerMask.NameToLayer(new_layer);
                                    }
                                    
                                }
                            }
                            
                        }
                    }


                    ////////////////////////////////////////////////////////////////////

                    if (x2Blocks != null)
                    {
                        if (x2Blocks.Length > 0)
                        {
                            if (x2Blocks[col] != null)
                            {
                                
                                if (row > x2Blocks[col].GetComponent<Ball>().i)
                                {
                                    if (x2Blocks[col].gameObject.transform.position.y < new_balls[row, col].transform.position.y)
                                    {

                                        //                                    string new_layer = "x" + col;
                                        string new_layer = "yg";

                                        new_balls[row, col].gameObject.layer = LayerMask.NameToLayer(new_layer);
                                    }
                                    else
                                    {
                                        
                                    }
                                }
                            }
                            else
                            {
                                
                            }
                        }
                    }
                 
                
                }



                ///////////////////////////////////////////////////////////////////////
                ///

                if (x3Blocks != null)
                {
                    if (x3Blocks.Length > 0)
                    {
                        if (x3Blocks[col] != null)
                        {
                            
                            if (row > x3Blocks[col].GetComponent<Ball>().i)
                            {
                                if (x3Blocks[col].gameObject.transform.position.y < new_balls[row, col].transform.position.y)
                                {

                                    //                                    string new_layer = "x" + col;
                                    string new_layer = "zg";

                                    new_balls[row, col].gameObject.layer = LayerMask.NameToLayer(new_layer);
                                }
                                
                            }
                        }
                       

                    }
                }
                else
                {
                    s += "!";
                }
            }
            
        }

            display(new_balls, "AFTER FILL");



        return new_balls;
    }

    private Ball getRandomizedBall(int row, int col)
    {
        int rand = new System.Random().Next(1, 105);
        

        string ball_type = "f";


        string[] ball_types = { "b", "f", "g", "t", "z" };
        int selected_ball_type = 0;

        if (rand < PERCENTAGE_B)
            selected_ball_type = 0;
        else if (rand < PERCENTAGE_F)
            selected_ball_type = 1;
        else if (rand < PERCENTAGE_G)
            selected_ball_type = 2;
        else if (rand < PERCENTAGE_T)
            selected_ball_type = 3;
        else if (rand > 100)
            selected_ball_type = 4;

        
        Ball ball = GameObject.Instantiate(getBallPrefabRow(row)[col] ,ballPrefabParent.transform);
        
//        Ball ball = GameObject.Instantiate(ballPrefabs[col] ,ballPrefabParent.transform);

//        ball.transform.position = (ball.transform.position + (Vector3.up * row)) ;
        ball.GetComponent<Image>().sprite = sprites[selected_ball_type];
        ball.type = ball_types[selected_ball_type];

        return ball;
    }

    Ball [] getBallPrefabRow(int r)
    {
//        if (r == 0)
//            return r0Prefabs;
        return getRowPrefabForCurrentLevel()[r];
  }

    Ball[][] getRowPrefabForCurrentLevel()
    {
        if(current_level == 1 || current_level == 2 )
        {
            Ball[][] rowPrefabs = new[] { r0Prefabs, r1Prefabs, r2Prefabs, r3Prefabs, r4Prefabs, r5Prefabs, r6Prefabs, r7Prefabs };
            return rowPrefabs;
        }
        if (current_level == 3)
        {
            Ball[][] rowPrefabs = new[] { r1Prefabs, r2Prefabs, r3Prefabs, r4Prefabs, r5Prefabs, r6Prefabs, r7Prefabs };
            return rowPrefabs;
        }

        if (current_level == 4)
        {
            Ball[][] rowPrefabs = new[] { r1Prefabs, r2Prefabs, r3Prefabs, r4Prefabs, r5Prefabs, r6Prefabs};
            return rowPrefabs;
        }
        if (current_level == 5)
        {
            Ball[][] rowPrefabs = new[] { r1Prefabs, r2Prefabs, r3Prefabs, r4Prefabs, r5Prefabs, r6Prefabs };
            return rowPrefabs;
        }
        else
        {
            Ball[][] rowPrefabs = new[] { r0Prefabs, r1Prefabs, r2Prefabs, r3Prefabs, r4Prefabs, r5Prefabs, r6Prefabs,r7Prefabs };
            return rowPrefabs;

        }

    }

    void display(Ball[,] list, string message)
    {
        Debug.Log("message");
        for (int i = 0; i < list.GetLength(0); i++)
        {
            for (int j = 0; j < list.GetLength(1); j++)
            {
/*                if(list[j, i]== null)
                    Debug.Log("--> !");
                else
                    Debug.Log("-->" + list[j, i].type);*/

            }
        }
        

    }

    public void getRowStr(Ball[,] bls, string bt)
    {
        for (int i = 0; i < bls.GetLength(0); i++)
        {
            string row_str = "";
            for (int j = 0; j < bls.GetLength(0); j++)
            {
/*                
                */

                if(bls[j,i] == null)
                    row_str = row_str + "!";
                else
                    row_str = row_str + bls[j, i].type;
            }
            Debug.Log("ROW STR: " + row_str);
        }

    }


    public void removeWithBombAction(Ball ball)
    {
        int row = ball.i;
        int col = ball.j;
        for (int i = row - 1; i <= row + 1; i++)
        {
            for (int j = col - 1; j <= col + 1; j++)
            {
                
                if (i >= 0 && i < board_size && j >= 0 && j < board_size)
                {
                    if (balls[i, j] != null)
                    {
                        if (balls[i, j].type != "x")
                        {

                            

                            myhash.Add(new int[] { i, j });

                        }

                    }
                }
            }
        }

        removeHittedBalls();
        myhash.Clear();

        Drop();
    }



    private void processWoods()
    {
        if (current_level == 8 || current_level == 9)
        {
            allWoods = new[] { woods0, woods1, woods2, woods3, woods4, woods5, woods6, woods7 };
        }

    }


    private void removeWood(int row, int col)
    {
        
        if (allWoods[row][col] == null)
        {
            
            return;
        }
        if (!allWoods[row][col].activeSelf)
        {
            
            return;
        }


        allWoods[row][col].SetActive(false);

        if (scoreMap.Contains("w"))
        {
            string type = "w";
            int new_score = ((int)scoreMap[type]) + 1;
            scoreMap[type] = new_score;
            updateScore();

            if (DoesPlayerWin() && !isGameFinished)
            {
                Helper.upgradeUserLevel();
                dialogYouWin.SetActive(true);
                isGameFinished = true;
            }
        }
    }

}
