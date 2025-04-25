using Godot;

public partial class Computer : Area2D
{
    [Export] public float CollectionTime = 4f;
    [Export] public Sprite2D Screen;

    private bool _isActive = true;
    private Player _player;

    public override void _Ready()
    {
        BodyEntered += OnBodyEntered;
        BodyExited += OnBodyExited;
    }

    public override void _Process(double delta)
    {
        if (!_isActive || _player == null) return;

        _player.UpdateProgress((float)GetMeta("Progress", 0f) + (float)delta);
        SetMeta("Progress", (float)GetMeta("Progress", 0f) + (float)delta);

        if ((float)GetMeta("Progress", 0f) >= CollectionTime)
        {
            CompleteCollection();
        }
    }

    private void OnBodyEntered(Node2D body)
    {
        if (body is Player player && _isActive)
        {
            _player = player;
            _player.ShowProgressBar(true, CollectionTime);
            SetMeta("Progress", 0f);
        }
    }

    private void OnBodyExited(Node2D body)
    {
        if (body is Player player)
        {
            _player?.ShowProgressBar(false);
            _player = null;
        }
    }

    private void CompleteCollection()
    {
        _isActive = false;
        Screen.Modulate = new Color(0, 0, 0);
        _player?.ShowProgressBar(false);

        if (GetTree().Root.HasNode("GameManager"))
        {
            GetNode<GameManager>("/root/GameManager").OnDataCollected();
        }
    }
}