using System.Collections;
using System.Collections.Generic;

public class EventCenter {

    //避免实例化
    private EventCenter() { }

    public delegate void noParamMethod();
    private static Dictionary<EventDefine, noParamMethod> eventList = new Dictionary<EventDefine, noParamMethod>();

    /// <summary>
    /// 添加监听
    /// </summary>
    /// <param name="ed">事件码</param>
    /// <param name="methon">监听方法</param>
    public static void AddListener(EventDefine ed, noParamMethod method)
    {
        if (!eventList.ContainsKey(ed))
        {
            eventList.Add(ed, method);
        }
        else
        {
            eventList[ed] += method;
        }
    }

    /// <summary>
    /// 广播事件
    /// </summary>
    /// <param name="ed">事件码</param>
    public static void Broadcast(EventDefine ed)
    {
        if (eventList.ContainsKey(ed))
        {
            eventList[ed]();
        }
    }

    /// <summary>
    /// 移除事监听者
    /// </summary>
    /// <param name="ed">事件码</param>
    ///  /// <param name="methon">要移除的监听方法</param>
    public static void RemoveListener(EventDefine ed, noParamMethod method)
    {
        if (eventList.ContainsKey(ed))
        {
            eventList[ed] -= method;
        }
    }
}
