using Godot;

public partial class Particles : Node2D{
	public static Particles instance;
	public Particles(){
		instance = this;
	}
	[Export]
	public ParticleProcessMaterial explode_material;
	[Export]
	public Texture2D explode_star;
	public override void _Ready(){
		
	}

	public void explode_stars(Vector2 pos,int amount){
		GpuParticles2D particles = new GpuParticles2D();
		particles.Position = pos;
		AddChild(particles);
		particles.OneShot=true;
		particles.Explosiveness=1.0f;
		particles.ProcessMaterial=explode_material;
		particles.Texture=explode_star;
		particles.Amount = amount;
		particles.Lifetime = 5.0f;
		// particles.Emitting = true;
	}
	public static Particles get_particles(){
		return instance;
	}
}
