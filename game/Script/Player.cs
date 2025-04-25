using Godot;

public partial class Player : CharacterBody2D
{
    [Export] public float Speed = 200f;
    [Export] public ProgressBar CollectionProgress;

    private Vector2 _touchInput = Vector2.Zero;
    private ProgressBar _progressBar;
    private bool _isDisabled = false;

    public override void _Ready()
    {
        CollectionProgress.Visible = false;
        _progressBar = GetNode<ProgressBar>("ProgressUI/ProgressBar");
        _progressBar.Visible = false;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (_isDisabled) return;

        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 direction = OS.HasFeature("mobile") ? _touchInput : GetInputDirection();

        if (direction != Vector2.Zero)
        {
            Velocity = direction.Normalized() * Speed;
        }
        else
        {
            Velocity = Vector2.Zero;
        }

        MoveAndSlide();
    }

    private Vector2 GetInputDirection()
    {
        return Input.GetVector("left", "right", "up", "down");
    }

    public void ShowProgressBar(bool show, float maxValue = 5f)
    {
        CollectionProgress.Visible = show;
        CollectionProgress.MaxValue = maxValue;
        if (!show) CollectionProgress.Value = 0;
    }

    public void UpdateProgress(float value)
    {
        CollectionProgress.Value = value;
    }

    public void DisableTemporarily(float duration)
    {
        _isDisabled = true;
        GetTree().CreateTimer(duration).Timeout += () => _isDisabled = false;
    }


    public void SetTouchInput(Vector2 input)
    {
        _touchInput = input;
    }
}