using UnityEngine;
public class PlayerCollisionHandler : MonoBehaviour
{
    private const string HitString = "Hit";
    private const float CollisionCooldown = 1f;
    private const float HitDecreaseMoveSpeed = -2f;

    private static readonly int Hit = Animator.StringToHash(HitString);

    [SerializeField] private Animator animator;

    private float _cooldownTimer;
    private LevelGenerator _levelGenerator;
    private void Start()
    {
        _levelGenerator = FindAnyObjectByType<LevelGenerator>();
    }

    private void Update()
    {
        _cooldownTimer += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_cooldownTimer < CollisionCooldown)
        {
            return;
        }

        _levelGenerator.ChangeChunkMoveSpeed(HitDecreaseMoveSpeed);
        animator.SetTrigger(Hit);
        _cooldownTimer = 0f;
    }
}
