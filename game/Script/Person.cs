using Godot;

public partial class Person : Area2D
{
    [Export] public float DisableTime = 2f;
    // [Export] public Sprite2D AlertSprite;

    public override void _Ready()
    {
        // AlertSprite.Visible = false;
        BodyEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node2D body)
    {
        if (body is Player player)
        {
            // AlertSprite.Visible = true;
            player.DisableTemporarily(DisableTime);
        }
    }
}