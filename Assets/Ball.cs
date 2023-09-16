using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public string type;
    public int i;
    public int j;

    public float intial_destination = -9999;

    // Start is called before the first frame update
    void Start()
    {
    }

/*    // Update is called once per frame
    void Update()
    {
        if (intial_destination != -9999)
        {
            
            if (transform.position.y > intial_destination)
            {
                transform.Translate(Vector3.down * 1f * Time.deltaTime);
            }
            else
            {
                intial_destination = -9999;
//                GetComponent<Rigidbody2D>().isKinematic = true;

            }
        }

    }*/

    public void setSelected()
    {
        // TODO: if ball type is not 'x'
        if (!this.type.Equals("x") && !this.type.Equals("z")) {

            if(GameManager.instance.selected1 == null)
            {
                GameManager.instance.selected1 = this;
                markAsSelected1(this.gameObject);
                
                
            }
            else if(GameManager.instance.selected1!=null & GameManager.instance.selected1 != gameObject)
            {
                Ball selectedBall1 = GameManager.instance.selected1.GetComponent<Ball>();

                if ((Mathf.Abs(selectedBall1.i- this.i) == 1 && (selectedBall1.j == this.j)) || (Mathf.Abs(selectedBall1.j - this.j) == 1) && (selectedBall1.i  == this.i))
                {
                    
                    

                    
                    GameManager.instance.selected2 = this;
                    GameManager.instance.checkImpact();
                    GameManager.instance.checkedForOrder = false;
                    //analyze impact
                    //switch
                    //remove selection
                }
                else
                {
                    GameManager.instance.selected1 = this;
                    markAsSelected1(this.gameObject);
                    removeSelected(selectedBall1.gameObject);
                    
                    
                }
            }
        }
        else if (this.type.Equals("z"))
        {
            Ball ball = GetComponent<Ball>();

            GameManager.instance.removeWithBombAction(ball);

        }
    }

    private void markAsSelected1(GameObject gameObject)
    {
        // enable the marker from here using getchild
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void removeSelected(GameObject gameObject)
    {
        // remove the marker for selection
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    /*    private void removeSelection()
        {
            if (selected1 != null)
            {
                selected1.removeSelected(selected1.gameObject);
            }
            if (selected2 != null)
            {
                selected2.removeSelected(selected2.gameObject);
            }

            selected1 = null;
            selected2 = null;
        }*/
/*    private void OnCollisionEnter2D(Collision2D collision)
    {
        

        
        

        if (gameObject.tag != collision.otherCollider.tag && collision.otherCollider.GetComponent<Rigidbody2D>().velocity != Vector2.zero)
        {
            intial_destination = -9999;
        }
    }*/
}
