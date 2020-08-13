using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BootStart : MonoBehaviour {

    public Image  imageMail;
    public Image imageMailSystem;
    public Image imageMailTeam;

    public Text txtMail;
    public Text txtMailSystem;
    public Text txtMailTeam;
    void Start () 
    {
        RedPointSystem rps = new RedPointSystem();
        rps.InitRedPointTreeNode();

        rps.SetRedPointNodeCallBack(RedPointConst.mail, MailCallBack);
        rps.SetRedPointNodeCallBack(RedPointConst.mailSystem, MailSystemCallBack);
        rps.SetRedPointNodeCallBack(RedPointConst.mailTeam, MailTeamCallBack);

        rps.SetInvoke(RedPointConst.mailSystem, 3);
        rps.SetInvoke(RedPointConst.mailTeam, 2);
        rps.SetInvoke(RedPointConst.mailTeam, 0);
        //rps.SetInvoke(RedPointConst.mailAlliance, 2);
        //rps.SetInvoke(RedPointConst.mail, 2);
    }
	
    void MailCallBack(RedPointNode node)
    {
        txtMail.text = node.pointNum.ToString();
       // imageMail.gameObject.SetActive(node.pointNum > 0);
    }
    void MailSystemCallBack(RedPointNode node)
    {
        txtMailSystem.text = node.pointNum.ToString();
       // imageMailSystem.gameObject.SetActive(node.pointNum > 0);
    }
    void MailTeamCallBack(RedPointNode node)
    {
        txtMailTeam.text = node.pointNum.ToString();
        //imageMailTeam.gameObject.SetActive(node.pointNum > 0);
    }
	 
}

public class RedPointConst
{
    public const string main = "Main";
    public const string mail = "Main.Mail";//邮件系统
    public const string mailSystem = "Main.Mail.MailSystem";//系统邮件
    public const string mailTeam = "Main.Mail.MailTeam";//队伍邮件
    public const string mailAlliance = "Main.Mail.MailAlliance";//工会邮件

    public const string task = "Mail.Task";
    public const string alliance = "Main.Alliance";
}

public class RedPointSystem
{
    public delegate void OnPointNumChange(RedPointNode node);
    RedPointNode mRootNode;

    static List<string> lstRedPointTreeList = new List<string>
    {
        RedPointConst.mail,
        RedPointConst.main,
        RedPointConst.mailSystem,
        RedPointConst.mailTeam,
        RedPointConst.mailAlliance,

        RedPointConst.task,
        RedPointConst.alliance,
    };
    //将数据处理成树结构
    public void InitRedPointTreeNode()
    {
        mRootNode = new RedPointNode();//生成一个树父节点
        mRootNode.nodeName = RedPointConst.main;//节点名称

        foreach(var s in lstRedPointTreeList)//遍历定义的所有节点
        {
            var node = mRootNode;
            var treeNodeAy = s.Split('.');//以"."为分割点
            if (treeNodeAy[0]!=mRootNode.nodeName)
            {
                continue;
            }
            if (treeNodeAy.Length>1)
            {
                for(int i = 1; i < treeNodeAy.Length; i++)
                {
                    if (!node.dicChilds.ContainsKey(treeNodeAy[i]))
                    {
                        node.dicChilds.Add(treeNodeAy[i], new RedPointNode());//将各个节点存入字典中。
                    }
                    node.dicChilds[treeNodeAy[i]].nodeName = treeNodeAy[i];//节点名称
                    node.dicChilds[treeNodeAy[i]].parent = node;//节点的父节点

                    node = node.dicChilds[treeNodeAy[i]];
                }
            }
        }
    }
    //设置节点的回调函数
    public void SetRedPointNodeCallBack(string strNode,RedPointSystem.OnPointNumChange callBack)
    {
        var nodeList = strNode.Split('.');//以"."为分割点
        if (nodeList.Length==1)
        {
            if (nodeList[0]!=RedPointConst.main)
            {
                return;
            }
        }
        var node = mRootNode;
        for (int i=1;i<nodeList.Length;i++)
        {
            if (!node.dicChilds.ContainsKey(nodeList[i]))
            {
                Debug.Log("节点中不包含：" + nodeList[i]);
                return;
            }
            node = node.dicChilds[nodeList[i]];//获得节点
            if (i == nodeList.Length - 1)//获得阶段的最后一位元素
            {
                Debug.Log("节点中的最后一位元素：" + nodeList[i]);
                node.numChangeFunc = callBack;//赋值事件函数
                return;
            }
        }
    }
    //设置红点数量
    public void SetInvoke(string strNode,int rpNum)
    {
        var nodeList = strNode.Split('.');//以"."为分割点
        if (nodeList.Length==1)//如果为树的父节点，
        {
            if (nodeList[0]!=RedPointConst.main)//返回
            {
                Debug.Log("不是红点系统中的");
                return;
            }
        }
        var node = mRootNode;//树的父节点
        for (int i=1;i<nodeList.Length;i++)
        {
            if (!node.dicChilds.ContainsKey(nodeList[i])) //遍历树中是否包含该字符中的元素
            {
                Debug.Log("不包含：" + nodeList[i]);
                return;
            }
            node = node.dicChilds[nodeList[i]];//得到节点
            if (i==nodeList.Length-1)
            {
                node.SetRedPointNum(rpNum);//设置红点但数量
                return;
            }
        }
    }
}
public class RedPointNode
{
    public string nodeName;
    public int pointNum = 0;
    public RedPointNode parent = null;
    public RedPointSystem.OnPointNumChange numChangeFunc;

    public Dictionary<string, RedPointNode> dicChilds = new Dictionary<string, RedPointNode>();
    //设置节点数量
    public void SetRedPointNum(int rpNum)
    {
        if (dicChilds.Count>0)//红点数量只能设置叶子节点
        {
            Debug.Log("只能设置叶子节点");
            return;

        }
        pointNum = rpNum;
        NotifyPointNumChange();
        if (parent!=null)
        {
            parent.ChangePredPointNum();
        }

    }
    public void ChangePredPointNum()
    {
        int num = 0;
        foreach (var node in dicChilds.Values)
        {
            num += node.pointNum;//每个节点中的数量相加，再叶子节点显示
        }
        if (num!=pointNum)
        {
            pointNum = num;
            NotifyPointNumChange();//回调事件
        }
    }
    public void NotifyPointNumChange()
    {
       if(numChangeFunc!=null)numChangeFunc(this);
        //mChangeFunc?.Invoke(this);
    }

}
