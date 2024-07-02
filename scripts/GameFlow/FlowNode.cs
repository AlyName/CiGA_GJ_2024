using System;
using Godot;

/// <summary>
/// 流程节点，分布在一个回合中
/// </summary>
public class FlowNode{
    private Action<LevelManager> mNodeAct;

    public string NodeName { get; }
    public bool IsLastNode { get; private set; } = false;
    
    public FlowNode(string nodeName, Action<LevelManager> NodeAct){
        NodeName=nodeName;
        mNodeAct=NodeAct;
    }

    /// <summary>
    /// 进入节点时
    /// </summary>
    // public FlowNode OnEnterNode()
    // {
    //     mOnEnterNodeAct?.Invoke();
    //     return this;
    // }

    /// <summary>
    /// 退出节点时
    /// </summary>
    public FlowNode OnNode(LevelManager n_levelmanager)
    {
        mNodeAct?.Invoke(n_levelmanager);
        return this;
    }

    // public FlowNode SetLastNode()
    // {
    //     IsLastNode = true;
    //     return this;
    // }
}