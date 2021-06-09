﻿using System.Xml;
using kScripts;

public class MinEventActionSetWaypoint : MinEventActionBase
{
    string command;
    //ClientInfo _cInfo;
    private EntityPlayer entityPlayer;
    public KTeleportObject saveTeleport = new KTeleportObject();

    public override void Execute(MinEventParams _params)
    {



        if (command == null)
        {
            return;
        }
        else
        {
            if (!SingletonMonoBehaviour<ConnectionManager>.Instance.IsClient)
            {
                entityPlayer = GameManager.Instance.World.GetPrimaryPlayer();
                saveTeleport.Add("waypoint", entityPlayer.GetBlockPosition());
                KHelper.ChatOutput(entityPlayer, "Waypoint location stored.");
            }
            else
            {
                SingletonMonoBehaviour<ConnectionManager>.Instance.SendToServer(NetPackageManager.GetPackage<NetPackageConsoleCmdServer>().Setup(GameManager.Instance.World.GetPrimaryPlayerId(), command), false);
            }
        }
    }

    public override bool ParseXmlAttribute(XmlAttribute _attribute)
    {
        bool xmlAttribute = base.ParseXmlAttribute(_attribute);
        if (xmlAttribute || !(_attribute.Name == "command"))
        {
            return xmlAttribute;
        }

        this.command = _attribute.Value;
        return true;
    }
}