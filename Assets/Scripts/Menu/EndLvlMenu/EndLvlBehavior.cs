using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class EndLvlBehaviour : MonoBehaviour
{
    [Range(-50f, 0f)]
    public TextMesh levelNameHolder;
    public float startPosition;
    public float animationSpeed;
    private float actualPosition;
    private bool anim = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector2(0, startPosition);
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
                transform.position = new Vector2(0,actualPosition);
            }
        }

    }

    private void OnLevelEnd(LevelEndEventArgs args)
    {
        
    }

}
