using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    [SerializeField]
    Transform player;

    Animator playerAnimator;
    [SerializeField]
    Animation animation;

    Dictionary<string, List<string>> animations;
    public Dictionary<string, int> animationIntegers;

    
    public List<string> animationList = new List<string>();

    [SerializeField]
    [Tooltip("common , rare")]
    List<string> animationSerieDirections = new List<string>();

    AnimationClip[] arrclip;
    Dictionary<string, AnimationClip> animationClips = new Dictionary<string, AnimationClip>();

    float nextAnimation = 0f;
    void Start()
    {
        playerAnimator = player.GetComponent<Animator>();
        AssignAnimations();      
        GetAnimations();
        GenerateAnimationSerieDirected();
        //StartCoroutine(makeDelayForStartAnimation());
    }


    void Update()
    {
        
    }

    void GetAnimations()
    {
        arrclip = playerAnimator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in arrclip)
        {
            print(" KKKK : " + clip.name);
            animationClips.Add(clip.name, clip);
            //print("NAME : " + clip.name);
        }
    }

    void AssignAnimations() 
    {
        animations = new Dictionary<string, List<string>> () 
        {
            {"common", new List<string>(){"SagAyakSektir", "SolAyakSektir"} },
            {"rare", new List<string>(){"SagDizSektir", "SolDizSektir", "KafaSektir"} },
        };
        animationIntegers = new Dictionary<string, int>()
        {
            {"SagAyakSektir", 2}, {"SolAyakSektir", 1},
            {"SagDizSektir", 3}, {"SolDizSektir", 4}, {"KafaSektir", 5},
        };
    }

    public void GenerateAnimationSerieDirected() 
    {
        animationList = new List<string>();

        foreach (string rarity in animationSerieDirections) 
        {
            animationList.Add(animations[rarity][Random.Range(0, animations[rarity].Count)]);
        }
    }

    void GenerateAnimationSerieFullRandom(int numberOfAnimations, int chanceOfRare) 
    {
        for (int i = 0; i < numberOfAnimations; i++) 
        {
            if (Random.Range(0, 100) < chanceOfRare)
            {
                animationList.Add(animations["rare"][Random.Range(0, animations["rare"].Count)]);
            }
            else            
            {
                animationList.Add(animations["common"][Random.Range(0, animations["common"].Count)]);
            }
            
        }
    }

    IEnumerator makeDelayForStartAnimation() 
    {
        yield return new WaitForSeconds(1f);
        playNextAnimation();
    }

   
    public void playNextAnimation() 
    {
        if (animationList.Count > 0)
        {
            print("PLAYING 22: " + animationList[0]);
            playerAnimator.SetInteger("AnimFactor", animationIntegers[animationList[0]]);
            //playerAnimator.Play(animationList[0]);

            animationList.RemoveAt(0);
        }
        else 
        {
            playerAnimator.SetInteger("AnimFactor", 0);
        }
    }

    

    /*void CheckForNextAnimation() // updatede cagirilacak
    {
        if (nextAnimation < Time.timeSinceLevelLoad && animationList.Count > 0) 
        {
            print("NExt Anim : " + nextAnimation + " :: " + Time.timeSinceLevelLoad + " :: " + animationList[0] + " :: " +animationClips[animationList[0]].length);
            nextAnimation = Time.timeSinceLevelLoad + animationClips[animationList[0]].length;
            playNextAnimation();
        }
    }*/

}
