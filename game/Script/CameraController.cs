using Godot;
using System;

public partial class CameraController : Camera2D
{
    [Export] public CharacterBody2D Player;
    [Export] public int RoomWidth = 192;
    [Export] public int RoomHeight = 54;

    private Vector2I currentRoom = new Vector2I(0, 0);

    public override void _Process(double delta)
    {
        Vector2 offset = new Vector2(RoomWidth / 2, RoomHeight / 2);
        Vector2 playerOffsetPos = Player.Position + offset;

        Vector2I newRoom = new Vector2I(
            Mathf.FloorToInt(playerOffsetPos.X / RoomWidth),
            Mathf.FloorToInt(playerOffsetPos.Y / RoomHeight)
        );

        if (newRoom != currentRoom)
        {
            currentRoom = newRoom;

            Vector2 roomCenter = new Vector2(
                newRoom.X * RoomWidth + RoomWidth / 2,
                newRoom.Y * RoomHeight + RoomHeight / 2
            );

            Position = roomCenter;
        }
    }
}
