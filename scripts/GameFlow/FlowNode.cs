using System;
using Godot;

/// <summary>
/// 流程节点，分布在一个回合中
/// </summary>
public class FlowNode
{
    private readonly Action mOnEnterNodeAct;
    private Action mOnExitNodeAct;

    public bool IsLastNode { get; private set; } = false;
    
    public FlowNode(Action onEnterNodeAct, Action onExitNodeAct)
    {
        mOnEnterNodeAct = onEnterNodeAct;
        mOnExitNodeAct = onExitNodeAct;
    }

    /// <summary>
    /// 进入节点时
    /// </summary>
    public FlowNode OnEnterNode()
    {
        mOnEnterNodeAct?.Invoke();
        return this;
    }

    /// <summary>
    /// 退出节点时
    /// </summary>
    public FlowNode OnExitNode()
    {
        mOnExitNodeAct?.Invoke();
        return this;
    }

    public FlowNode SetLastNode()
    {
        IsLastNode = true;
        return this;
    }
}
