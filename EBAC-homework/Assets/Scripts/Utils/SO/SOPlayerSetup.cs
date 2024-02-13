using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu]
public class SOPlayerSetup : ScriptableObject
{
    public Animator player;

    [Header("Movement Parameters")]
#region Movement Parameters
    public Vector2 friction = new Vector2(.1f, 0);
    public float speed = 5;
    public float runSpeed;
    public float forceJump = 5;
    public float _currentSpeed;
    public float groundCheckRadius = 0.3f;
    public string TagToCheck = "Ground";
#endregion

    [Header("Animation Setup")]
#region Animation Parameters
    public float stretchScaleY = 1.5f;
    public float stretchScaleX = 0.7f;
    public float animDuration = .3f;
    public Ease jumpEase = Ease.OutBack;
    public Ease landEase = Ease.OutBack;
#endregion

 [Header("Player Animation")]
#region Player Animation
    public string boolRun = "Run";
    public string triggerDeath = "Death";
#endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
