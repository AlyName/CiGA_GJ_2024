using Godot;
using System;
using System.Diagnostics;

public partial class Card : CardMove{
	public int card_level,score,card_type;

	public string card_type_s,card_description,card_description_2,card_name;

	public Color n_color;

	protected bool is_effect_0_add_score=true;
	public bool is_use_in_level=true,is_instant_effect=false;
	protected CardEvent event_0=null,event_1=null;
	public EffectManager.Type effect_0=EffectManager.Type.None,effect_1=EffectManager.Type.None;

	public Card(){

	}
	public Card(cfg.Item n_card_item){
		card_type_s=n_card_item.CardTypeS;
		card_description=n_card_item.DefaultCardDescription;
		card_type=n_card_item.CardTypeId;
		card_name=n_card_item.CardDisplayName;
		is_use_in_level=n_card_item.IsUseInLevel;
		is_instant_effect=n_card_item.IsInstantEffect;
		score=n_card_item.BaseScore;
        card_description_2=n_card_item.DefaultDescription2;
		if(n_card_item.Event0Type!=""){
			event_0=new CardEvent((CardEvent.Type)Enum.Parse(typeof(CardEvent.Type),n_card_item.Event0Type));
		}
		if(n_card_item.Event1Type!=""){
			event_1=new CardEvent((CardEvent.Type)Enum.Parse(typeof(CardEvent.Type),n_card_item.Event1Type));
		}
		if(n_card_item.Effect1!=""){
			effect_1=(EffectManager.Type)Enum.Parse(typeof(EffectManager.Type),n_card_item.Effect1);
		}

		animated=new AnimatedSprite2D();
		animated.SpriteFrames=ResourceLoader.Load<SpriteFrames>("res://imgs/"+n_card_item.Texture);
		animated.ZIndex=-1;
		Texture=ResourceLoader.Load<Texture2D>("res://imgs/border_enlarge.png");
	}
	protected override void use_card(){
		CardEvent now_event=new CardEvent();
		if(is_use_in_level){
			now_event.card_level=card_level;
			now_event.card_type=card_type;
			now_event.card_type_s=card_type_s;
			now_event.score=score;
			now_event.is_instant_effect=is_instant_effect;
			if(is_use==0){
				if(is_effect_0_add_score){
					now_event.event_type=CardEvent.Type.AddScore;
					
				}else{
					now_event.event_type=CardEvent.Type.Effect;
					now_event.action=new Action<LevelManager,int>(generate_effect_action(effect_0));
				}
			}else{
				now_event.event_type=CardEvent.Type.Effect;
				now_event.action=new Action<LevelManager,int>(generate_effect_action(effect_1));
			}
			
		}else{
			if(is_use==0){
				now_event=event_0;
			}else{
				now_event=event_1;
			}
		}
		now_event.pos=Position;
		MonoControl.get_control().add_card_event(now_event);

		if(is_use==0){
			AudioManager.Instance.PlaySound(AudioEnum.PlayUp);
		}else{
			AudioManager.Instance.PlaySound(AudioEnum.PlayDown);
		}
	}

	Action<LevelManager,int> generate_effect_action(EffectManager.Type n_type){
		if(n_type==EffectManager.Type.None){
			return new Action<LevelManager, int>(
					(LevelManager n_level_manager,int n_card_level)=>{
						return;
					}
			);
		}
		return new Action<LevelManager,int>(
				(LevelManager n_level_manager,int n_card_level)=>{
					n_level_manager.effect_manager.effect_queue.Add(
							new Tuple<EffectManager.Type,int>(
								n_type,
								n_card_level
							)
					);
				}
		);
	}
	protected override void check_sprite(){
		base.check_sprite();
		// if(is_use_in_level)card_label.Text="+"+score.ToString();
		// else card_label.Text="";
		//TODO
		if(card_level==1)n_color=new Color(0,0.5f,1,1);
		else if(card_level==2)n_color=new Color(1,0,1,1);
		else if(card_level==3)n_color=new Color(1,0.5f,0,1);
		else n_color=new Color(1,1,1,1);
		if(state!=0){
			SelfModulate=n_color;
		}
		if(can_drag==1){
			// TODO
			string n_card_description=card_description.Replace("%N%",(8-MonoControl.get_control().get_duplicate_times()).ToString());
			UI.get_ui().set_description(n_card_description);
            
            UI.get_ui().set_description_2(card_description_2);
		}
	}
}
