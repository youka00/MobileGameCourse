using Godot;

public partial class GameManager : Node
{
    // Signals
    [Signal] public delegate void DataCollectedEventHandler();
    [Signal] public delegate void PlayerSpottedEventHandler();

    // Game stats
    public int CollectedData { get; private set; }
    public int TotalComputers { get; private set; }

    public override void _Ready()
    {
        // Count all active computers in the level
        TotalComputers = GetTree().GetNodesInGroup("computers").Count;
        GD.Print($"Total computers: {TotalComputers}");
    }

    public void OnDataCollected()
    {
        CollectedData++;
        GD.Print($"Data collected: {CollectedData}/{TotalComputers}");

        EmitSignal(SignalName.DataCollected);

        // Simple level complete check
        if (CollectedData >= TotalComputers)
        {
            GD.Print("LEVEL COMPLETE!");
        }
    }
    public void OnPlayerSpotted()
    {
        EmitSignal(SignalName.PlayerSpotted);
    }
}