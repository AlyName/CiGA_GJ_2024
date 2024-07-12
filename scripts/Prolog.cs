using Godot;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

public partial class Prolog : Node2D
{
	[Export]
	public string[] prolog_str;
	AnimatedSprite2D sun;
	Label label;
	Sprite2D label_box;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){
		sun=GetNode<AnimatedSprite2D>("PrologSun");
		label_box=GetNode<Sprite2D>("PrologBox");
		label=UI.get_ui().get_prolog_label();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta){

	}

	public async Task start_prolog(){
		label_box.Visible=false;
		UI.get_ui().light_off();
		sun.Play("default");
		label.Text="";
		await Main.get_main().wait_time(5f);
		Tween m_tween=GetTree().CreateTween();
		m_tween.TweenProperty(Main.get_main().GetNode<AnimatedSprite2D>("Foreground"),"modulate",new Color(1,1,1,0),0.5f);
		Debug.WriteLine("aa");
		for(int i=0;i<prolog_str.Length;i++){
			Debug.WriteLine(i.ToString());
			await Main.get_main().wait_time(2.5f);
			sun.Play((i+1).ToString());
			label_box.Visible=true;
			label.Text=prolog_str[i];
			label.VisibleRatio=0;
			if(i==3){
				UI.get_ui().light_on();
				CardWheel.get_wheel().ZIndex=265;
				CardGenerator.get_generator().generate_card("ThreeRow",0,-1);
			}
			m_tween=GetTree().CreateTween();
			m_tween.TweenProperty(label,"visible_ratio",1,0.5f);
			await Main.get_main().wait_time(1.25f);
			sun.Play("default");
		}

		await Main.get_main().wait_time(3.75f);
		label.Visible=false;
		UI.get_ui().set_description("");
		UI.get_ui().set_description_2("");
		CardWheel.get_wheel().ZIndex=0;
		m_tween=GetTree().CreateTween();
		m_tween.TweenProperty(this,"modulate",new Color(1,1,1,0),3f);
		await Main.get_main().wait_time(3f);
		Main.get_main().GetNode<Background>("Background").set_run=true;
		MonoControl.get_control().add_card_event(new CardEvent(CardEvent.Type.GameStart));
		QueueFree();
	}
}
