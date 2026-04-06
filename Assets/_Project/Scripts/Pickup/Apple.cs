public class Apple : Pickup
{
    private const float PowerUpMoveSpeedIncrease = 3f;

    private LevelGenerator _levelGenerator;
    // this is being called everytime an apple is spawned need to learn something
    // like DI to prevent this code smell
    
    public void Init(LevelGenerator levelGenerator)
    {
        this._levelGenerator = levelGenerator;
    }
    protected override void OnPickup()
    {
        _levelGenerator.ChangeChunkMoveSpeed(PowerUpMoveSpeedIncrease);
    }
}
