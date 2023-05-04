using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;


public class EndLvlBehavior : MonoBehaviour
{
    public TextMeshProUGUI levelNameHolder;
    public TextMeshProUGUI levelTimeHolder;
    public List<GameObject> orbs;
    [Range(-1000f, 0f)]
    public float startPosition;
    public float animationSpeed;
    private float actualPosition;
    private bool anim = false;

    // Start is called before the first frame update
    void Start()
    {

        GetComponent<RectTransform>().localPosition = new Vector2(0, startPosition);
        actualPosition = startPosition;
        GlobalEvents.current.LevelEnd += OnLevelEnd;
    }

    // Update is called once per frame
    void Update()
    {
        if(anim)
        {
            if(actualPosition>0)
            {
                actualPosition = 0;
                anim= false;
            }
            else
            {
                actualPosition += animationSpeed * Time.deltaTime;
                GetComponent<RectTransform>().localPosition = new Vector2(0,actualPosition);
            }
        }

    }

    private void OnLevelEnd(LevelEndEventArgs args)
    {
        anim = true;
        levelNameHolder.text = args.LvlNumber.ToString();
        levelTimeHolder.text = timeConverter(args.Time);
        for(int a=0; a<orbs.Count; a++)
        {
            if(a<args.Orbs ) orbs[a].SetActive(true);
            else orbs[a].SetActive(false);
        }
    }

    private string  timeConverter(float time)
    {
        time /= 60;
        string ret = time.ToString("n2");
        ret= ret.Replace(',', ':');
        return ret;
    }    

}
