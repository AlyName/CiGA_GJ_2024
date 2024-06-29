using Godot;
using System;
using System.Diagnostics;

public partial class Card : Sprite2D
{
	// 物体的初始位置
	private Vector2 _initialPosition;

	// 检测玩家是否正在拖拽
	private bool _dragging = false;

	// Timer用于处理返回原位的动画
	private Timer _returnTimer;

	// 返回原位的速度（每秒移动的像素数）
	private float _returnSpeed = 5f;

	public override void _Ready()
	{
		// 保存物体的初始位置
		_initialPosition = Position;

		// 创建并添加Timer节点
		_returnTimer = new Timer();
		AddChild(_returnTimer);
		_returnTimer.WaitTime = 0.01f; // 设置Timer的间隔时间
		_returnTimer.OneShot = false; // 设置为循环模式
		_returnTimer.Connect("timeout", new Callable(this, nameof(OnReturnTimerTimeout))); // 连接timeout信号
	}

	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton eventMouseButton)
		{
			if (eventMouseButton.ButtonIndex == MouseButton.Left)
			{
				Debug.WriteLine(GetRect());
				Debug.WriteLine(GetGlobalMousePosition());
				// 鼠标左键按下且位置在物体上
				if (eventMouseButton.Pressed && (GetRect()).HasPoint(ToLocal(GetGlobalMousePosition())))
				{
					// 开始拖拽
					_dragging = true;
				}
				else
				{
					// 停止拖拽
					_dragging = false;
					// 启动Timer开始返回原位
					_returnTimer.Start();
				}
			}
		}
	}

	public override void _Process(double delta)
	{
		if (_dragging)
		{
			// 获取鼠标位置，并设置物体的位置
			Position = GetGlobalMousePosition();
		}
	}

	private void OnReturnTimerTimeout()
	{
		// 计算当前位置到初始位置的方向和距离
		Vector2 direction = _initialPosition - Position;
		float distance = direction.Length();

		// 如果物体已经非常接近初始位置，直接设置到初始位置
		if (distance < _returnSpeed * _returnTimer.WaitTime)
		{
			Position = _initialPosition;
			_returnTimer.Stop(); // 停止Timer
		}
		else
		{
			// 向初始位置移动
			Position = (_initialPosition - Position) * _returnSpeed * (float)_returnTimer.WaitTime + Position;
		}
	}
}
