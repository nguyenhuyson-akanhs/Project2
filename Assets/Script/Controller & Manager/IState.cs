using UnityEngine;

public interface IState
{
    /// <summary>
    /// Tiền xử lí, gọi 1 lần duy nhất trước khi vào looping, ví dụ khởi động animation, audio...
    /// </summary>
    public void Enter();

    /// <summary>
    /// Giai đoạn looping, xử lí hành động liên tục
    /// </summary>
    public void FrameUpdate();

    /// <summary>
    /// Kết thúc state, dừng animation, audio...
    /// </summary>
    public void Exit();
}
