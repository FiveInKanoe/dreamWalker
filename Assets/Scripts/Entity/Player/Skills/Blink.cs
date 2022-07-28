using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Blink", menuName = "Skills/Blink")]
public class Blink : Skills
{
    [SerializeField] private float maxRadius;
    [SerializeField] private BlinkMarker markerPrefab;

    private GameObject blinkCont;

    private BlinkMarker currentMarker;


    public override void Initialize(Player player, GameObject skillContainer)
    {
        SkillContainer = skillContainer;

        Player = player;
        blinkCont = new GameObject("Blink");
        blinkCont.transform.SetParent(SkillContainer.transform);  
    }

    public override IEnumerator Usage()
    {
        while (true)
        {
            yield return new WaitUntil(() => Input.GetKey(HotKey));
            Perform();

            bool isBlinked = false;
            yield return new WaitUntil(() => 
            { 
                isBlinked = Input.GetMouseButton(0);
                return Input.GetMouseButton(0) || Input.GetMouseButton(1);
            });

            if (isBlinked)
            {
                Player.transform.position = currentMarker.transform.position;
            }

            Destroy(currentMarker.gameObject);
            currentMarker = null;

            if (isBlinked)
            {
                yield return new WaitForSecondsRealtime(CoolDown);
            }  
        }
    }

    private void Perform()
    {
        if (currentMarker == null)
        {
            currentMarker = Instantiate(markerPrefab);
            currentMarker.transform.SetParent(blinkCont.transform);

            currentMarker.PlayerTransform = Player.transform;
            currentMarker.MaxRadius = maxRadius;
        }
    }
    
}
