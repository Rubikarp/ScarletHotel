using UnityEngine;


public class ScoresManager : Singleton<ScoresManager>
{
    public float luxeScore;
    public float susScore;
    public float mythScore;
    public float scandalScore;

    public int maxScore =100;



    public ScoreProgressBar luxeProgressBar;
    public ScoreProgressBar mythProgressBar;
    public ScoreProgressBar scandalProgressBar;
    public ScoreProgressBar susProgressBar;

    public enum ScoreType
    {
        Luxe,
        Sus,
        Myth,
        Scandal
    }
    private void Start()
    {
        updateVisuals();
    }
    private void updateVisuals()
    {
        luxeProgressBar.SetCurrentFill(luxeScore / maxScore);
        susProgressBar.SetCurrentFill(susScore / maxScore);
        mythProgressBar.SetCurrentFill(mythScore / maxScore);
        scandalProgressBar.SetCurrentFill(scandalScore / maxScore);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void addAmmountToScore(int addAmmount,ScoreType scoreType)
    {

        switch (scoreType)
        {
            case ScoreType.Luxe:   
                luxeScore = Mathf.Clamp(luxeScore + addAmmount, 0, maxScore);
                updateVisuals();
                break;

            case ScoreType.Sus:
                susScore = Mathf.Clamp(susScore + addAmmount, 0, maxScore);
                updateVisuals();
                break;
            case ScoreType.Myth:
                mythScore = Mathf.Clamp(mythScore + addAmmount, 0, maxScore);
                updateVisuals();
                break;
            case ScoreType.Scandal:
                scandalScore = Mathf.Clamp(scandalScore + addAmmount, 0, maxScore);
                updateVisuals();
                break;
            default:
                break;
        }
    }

  
    public void setScoreAmmount(int newAmmount, ScoreType scoreType)
    {
        switch (scoreType)
        {
            case ScoreType.Luxe:
                luxeScore = Mathf.Clamp(newAmmount, 0, maxScore); 
                updateVisuals();
                break;

            case ScoreType.Sus:
                susScore = Mathf.Clamp(newAmmount, 0, maxScore);
                updateVisuals();
                break;
            case ScoreType.Myth:
                mythScore = Mathf.Clamp(newAmmount, 0, maxScore);
                updateVisuals();
                break;
            case ScoreType.Scandal:
                scandalScore = Mathf.Clamp(newAmmount, 0, maxScore);
                updateVisuals();
                break;
            default:
                break;
        }

    }

    //debug
    public void setluxeammount(int newscore)
    {
        setScoreAmmount(newscore, ScoreType.Luxe);
    }

    //debug
    public void addluxeammount(int scoreAdd)
    {
        addAmmountToScore(scoreAdd, ScoreType.Luxe);
    }

}
