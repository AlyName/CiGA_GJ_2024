using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 回合流程
/// </summary>
public interface IFlow
{
    /// <summary>
    /// 到下一个节点
    /// </summary>
    // void NextNode();
    
    /// <summary>
    /// 获胜检测
    /// </summary>
    void WinCheck();

    /// <summary>
    /// 失败检测
    /// </summary>
    void LoseCheck();

    /// <summary>
    /// 回合结束结算
    /// </summary>
    void RoundEndCheck();
}

/// <summary>
/// 
/// </summary>
public class CardGameFlow: 
    IFlow
{
    /// <summary> 玩家获得点数 </summary>
    // private int mTotalPoint;
    
    // public int TotalPoint
    // {
    //     get => mTotalPoint;
    //     set
    //     {
    //         mTotalPoint = value;
    //         WinCheck();
    //     }
    // }

    // /// <summary> 当前回合数 </summary>
    // private int mCurRound;

    // private int mMaxRound = 10;//todo 待设置，这里临时写一个
    
    // public int CurRound
    // {
    //     get => mCurRound;
    //     set
    //     {
    //         mCurRound = value;
    //         RoundEndCheck();
    //     }
    // }

    private List<FlowNodeLine> mAllFlowNodeLine = new();

    // private Queue<FlowNode> mAllNodes = new();

    private FlowNode mCurNode = null;

    public void NextNode(LevelManager n_level_manager,int n_card_level)
    {
        if(mCurNode is not null)
        {
            mCurNode.OnNode(n_level_manager,n_card_level);//退出节点
            foreach (var flowNodeLine in mAllFlowNodeLine)//执行退出节点时的一些效果
                flowNodeLine.OnExitNode(mCurNode.NodeName);
        }
        
        // if (mCurNode?.IsLastNode == true)//是否是最后一个节点
        // {
        //     mAllNodes.Enqueue(mCurNode);
        //     mCurNode = null;//置空
        //     RoundEndCheck();
        // }
        // else
        // {
        //     var tempNode = mCurNode;
        //     mCurNode = mAllNodes.Dequeue();
        //     mCurNode.OnEnterNode();

        //     foreach (var flowNodeLine in mAllFlowNodeLine)//执行进入节点时的一些效果
        //         flowNodeLine.OnEnterNode(mCurNode.NodeName);
            
        //     if(tempNode is not null)
        //         mAllNodes.Enqueue(tempNode);
        // }
    }

    public void PushNode(FlowNode node){
        mCurNode=node;
    }

    public void WinCheck()
    {
        //todo 获胜检测
    }

    public void LoseCheck()
    {
        //todo 失败检测
    }

    public void RoundEndCheck()
    {
        // LoseCheck();
        // mAllFlowNodeLine = mAllFlowNodeLine
        //     .Where(nodeLine => nodeLine.MaxRound > mCurRound)
        //     .ToList();
    }

    public CardGameFlow(){
        //todo 这里是举例子
        // mAllNodes.Enqueue(new FlowNode("玩家抽卡", OnPlayerDrawCard, null));
        // mAllNodes.Enqueue(new FlowNode("玩家打出第一张牌", OnPlayerUseACard, AfterPlayerUseACard));
        // mAllNodes.Enqueue(new FlowNode("玩家打出第二张牌", OnPlayerUseACard, AfterPlayerUseACard).SetLastNode());
    }

    
    // /// <summary>
    // /// 当玩家抽牌时发生的事情
    // /// </summary>
    // private void OnPlayerDrawCard()
    // {
        
    // }
    
    // /// <summary>
    // /// 玩家使用一张牌发生的事情，使用可能有打出，弃牌，塞回牌库等，即手牌-1
    // /// </summary>
    // private void OnPlayerUseACard()
    // {
        
    // }
    
    // /// <summary>
    // /// 当玩家使用一张牌后，可以说成消耗一张牌后吧，即手牌数量-1后
    // /// </summary>
    // private void AfterPlayerUseACard()
    // {
        
    // }
}