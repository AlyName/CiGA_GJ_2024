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
	[Export]
	public PackedScene orb_scene,ring_scene,multi_scene_1;
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

	public void explode_orbs(Vector2 pos,int amount){
		var orb = orb_scene.Instantiate();
		orb.Set("position",pos);
		orb.Set("num_particles",amount);
		orb.Set("repeat",false);
		AddChild(orb);
	}

	public void explode_ring(Vector2 pos){
		var ring = ring_scene.Instantiate();
		ring.Set("position",pos);
		AddChild(ring);
	}
	public void explode_multi(Vector2 pos){
		var multi = multi_scene_1.Instantiate();
		multi.Set("position",pos);
		AddChild(multi);
	}

	public void card_explode(Vector2 pos,int score){
		if(score<=10){
			explode_orbs(pos,16);
		}else if(score<=50){
			explode_orbs(pos,64);
			Camera.get_camera().shake(3f,5f);
		}else if(score<=250){
			explode_orbs(pos,256);
			Camera.get_camera().shake(5f,10f,0.99f);
		}else{
			explode_multi(pos);
			Camera.get_camera().shake(10f,20f,0.99f);
		}
	}
	public static Particles get_particles(){
		return instance;
	}
}
