using System;
using System.Collections.Generic;

/// <summary>
/// 流程节点线
/// </summary>
/// <remarks>
/// 卡牌或者buff的节点线，每当有效果需要在流程中触发，
/// 就可以生成一个，并注入对应的内容，在生命周期结束时销毁
/// </remarks>
public class FlowNodeLine
{
    private Dictionary<string, Action<object>> nodeEnterDic = new();
    private Dictionary<string, Action<object>> nodeExitDic = new();
    /// <summary> 上下文数据，暂时写成object吧 </summary>
    private object mContext;
    
    public int MaxRound { get; }

    public FlowNodeLine(object context, int maxRound)
    {
        mContext = context;
        MaxRound = maxRound;
    }

    public FlowNodeLine AddEnterNodeAct(string flowNode, Action<object> act)
    {
        nodeEnterDic.Add(flowNode, act);
        return this;
    }
    
    public FlowNodeLine AddExitNodeAct(string flowNode, Action<object> act)
    {
        nodeExitDic.Add(flowNode, act);
        return this;
    }
    
    public void OnEnterNode(string flowNode)
    {
        if(nodeEnterDic.TryGetValue(flowNode, out var act))
            act?.Invoke(mContext);
    }
    
    public void OnExitNode(string flowNode)
    {
        if(nodeExitDic.TryGetValue(flowNode, out var act))
            act?.Invoke(mContext);
    }
}