using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Assets.Scripts.Enum;

public class AnimationManager : MonoBehaviour
{
    [SerializeField]
    Transform player;

    Animator playerAnimator;
    [SerializeField]
    Animation animation;

    Dictionary<string, List<Actions>> animations;
    public Dictionary<Actions, int> animationIntegers;

    [Header("SagAyakSektir, SolAyakSektir, SagDizSektir, SolDizSektir, KafaSektir")]
    public List<Actions> animationList = new List<Actions>();

    [SerializeField]
    [Tooltip("common , rare")]
    List<string> animationSerieDirections = new List<string>();

    [Header("SagAyakSektir, SolAyakSektir, SagDizSektir, SolDizSektir, KafaSektir")]
    public List<Actions> tempAnimationList = new List<Actions>();

    AnimationClip[] arrclip;
    Dictionary<string, AnimationClip> animationClips = new Dictionary<string, AnimationClip>();

    [Header("must be checked before start")]
    public bool devAnimTestOn = false;

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
        animations = new Dictionary<string, List<Actions>> () 
        {
            {"common", new List<Actions>(){Actions.AyakSag, Actions.AyakSol} },
            {"rare", new List<Actions>(){Actions.DizSag, Actions.DizSol, Actions.Kafa } }
        };
        animationIntegers = new Dictionary<Actions, int>()
        {
            {Actions.AyakSag, 2}, {Actions.AyakSol, 1},
            {Actions.DizSag, 3}, {Actions.DizSol, 4}, {Actions.Kafa, 5},
        };
    }

    public void GenerateAnimationSerieDirected() 
    {


        if (!devAnimTestOn)
        {
            animationList = new List<Actions>();
            foreach (string rarity in animationSerieDirections)
            {
                animationList.Add(animations[rarity][Random.Range(0, animations[rarity].Count)]);
            }
        }

    }

    // devAnimTest Purposes
    
    [Button]
    public void PusAnimList()
    {
        animationList = tempAnimationList;
    }
    // devAnimTest Purposes //

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
