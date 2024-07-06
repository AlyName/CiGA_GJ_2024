using Godot;
using System;

public partial class ShaderManager : Node
{
	private static ShaderManager sInstence;

	public static ShaderManager Instance => sInstence;

	[Export]public Shader outlineShader;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sInstence = this;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
