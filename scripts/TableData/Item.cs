
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Luban;
using SimpleJSON;


namespace cfg
{
public sealed partial class Item : Luban.BeanBase
{
    public Item(JSONNode _buf) 
    {
        { if(!_buf["card_type_s"].IsString) { throw new SerializationException(); }  CardTypeS = _buf["card_type_s"]; }
        { if(!_buf["is_use_in_level"].IsBoolean) { throw new SerializationException(); }  IsUseInLevel = _buf["is_use_in_level"]; }
        { if(!_buf["default_card_description"].IsString) { throw new SerializationException(); }  DefaultCardDescription = _buf["default_card_description"]; }
        { if(!_buf["default_description_2"].IsString) { throw new SerializationException(); }  DefaultDescription2 = _buf["default_description_2"]; }
        { if(!_buf["card_type_id"].IsNumber) { throw new SerializationException(); }  CardTypeId = _buf["card_type_id"]; }
        { if(!_buf["event_0_type"].IsString) { throw new SerializationException(); }  Event0Type = _buf["event_0_type"]; }
        { if(!_buf["event_1_type"].IsString) { throw new SerializationException(); }  Event1Type = _buf["event_1_type"]; }
        { if(!_buf["effect_1"].IsString) { throw new SerializationException(); }  Effect1 = _buf["effect_1"]; }
        { if(!_buf["is_instant_effect"].IsBoolean) { throw new SerializationException(); }  IsInstantEffect = _buf["is_instant_effect"]; }
        { if(!_buf["effect_img"].IsString) { throw new SerializationException(); }  EffectImg = _buf["effect_img"]; }
        { if(!_buf["base_score"].IsNumber) { throw new SerializationException(); }  BaseScore = _buf["base_score"]; }
        { if(!_buf["texture"].IsString) { throw new SerializationException(); }  Texture = _buf["texture"]; }
        { if(!_buf["card_display_name"].IsString) { throw new SerializationException(); }  CardDisplayName = _buf["card_display_name"]; }
        { if(!_buf["description_0"].IsString) { throw new SerializationException(); }  Description0 = _buf["description_0"]; }
        { if(!_buf["description_1"].IsString) { throw new SerializationException(); }  Description1 = _buf["description_1"]; }
        { if(!_buf["description_2"].IsString) { throw new SerializationException(); }  Description2 = _buf["description_2"]; }
        { if(!_buf["description_3"].IsString) { throw new SerializationException(); }  Description3 = _buf["description_3"]; }
    }

    public static Item DeserializeItem(JSONNode _buf)
    {
        return new Item(_buf);
    }

    public readonly string CardTypeS;
    public readonly bool IsUseInLevel;
    public readonly string DefaultCardDescription;
    public readonly string DefaultDescription2;
    public readonly int CardTypeId;
    public readonly string Event0Type;
    public readonly string Event1Type;
    public readonly string Effect1;
    public readonly bool IsInstantEffect;
    public readonly string EffectImg;
    public readonly int BaseScore;
    public readonly string Texture;
    public readonly string CardDisplayName;
    public readonly string Description0;
    public readonly string Description1;
    public readonly string Description2;
    public readonly string Description3;
   
    public const int __ID__ = 2289459;
    public override int GetTypeId() => __ID__;

    public  void ResolveRef(Tables tables)
    {
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
    }

    public override string ToString()
    {
        return "{ "
        + "cardTypeS:" + CardTypeS + ","
        + "isUseInLevel:" + IsUseInLevel + ","
        + "defaultCardDescription:" + DefaultCardDescription + ","
        + "defaultDescription2:" + DefaultDescription2 + ","
        + "cardTypeId:" + CardTypeId + ","
        + "event0Type:" + Event0Type + ","
        + "event1Type:" + Event1Type + ","
        + "effect1:" + Effect1 + ","
        + "isInstantEffect:" + IsInstantEffect + ","
        + "effectImg:" + EffectImg + ","
        + "baseScore:" + BaseScore + ","
        + "texture:" + Texture + ","
        + "cardDisplayName:" + CardDisplayName + ","
        + "description0:" + Description0 + ","
        + "description1:" + Description1 + ","
        + "description2:" + Description2 + ","
        + "description3:" + Description3 + ","
        + "}";
    }
}

}
