using UnityEngine;
using UnityEngine.UI;

public class ResultBoard : MonoBehaviour
{
    [SerializeField]
    Text resultText;

    [SerializeField]
    Text myNickName;
    [SerializeField]
    Text opponentNickName;

    [SerializeField]
    Text myScore;
    [SerializeField]
    Text opponentScore;

    [SerializeField]
    ScoreBoard myBoard;
    [SerializeField]
    ScoreBoard opponentBoard;

    [SerializeField]
    AudioClip winClip;
    [SerializeField]
    AudioClip loseClip;

    private void Start()
    {
        int myScore = myBoard.totalSlot.CurrentScore;
        int opponentScore = opponentBoard.totalSlot.CurrentScore;

        if (myScore > opponentScore)
        {
            resultText.text = "YOU WIN!";
            SQLManager.instance.Result(true, myScore);
            GetComponent<AudioSource>().PlayOneShot(winClip);
        }
        else if (myScore < opponentScore)
        {
            resultText.text = "YOU LOSE!";
            SQLManager.instance.Result(false, myScore);
            GetComponent<AudioSource>().PlayOneShot(loseClip);
        }
        else
        {
            resultText.text = "DRAW!";
            SQLManager.instance.Result(false, myScore);
        }
        myNickName.text = myBoard.NickName.text;
        opponentNickName.text = opponentBoard.NickName.text;
        this.myScore.text = myScore.ToString();
        this.opponentScore.text = opponentScore.ToString();
    }
}
