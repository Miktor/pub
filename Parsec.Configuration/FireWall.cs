using System.Text;
using System;
using NetFwTypeLib;


namespace Parsec.Configuration
{
    static class FireWall    
    {
        public static void GetFullInfo(ref StringBuilder sb)
        {
            Type objectType = Type.GetTypeFromCLSID(new Guid("{304CE942-6E39-40D8-943A-B913C40C9CD4}"));
            INetFwMgr manager = Activator.CreateInstance(objectType) as NetFwTypeLib.INetFwMgr;
            INetFwProfile profile = manager.LocalPolicy.CurrentProfile;

            sb.AppendLine("- Firewall Enabled: " + profile.FirewallEnabled);

            if (profile.AuthorizedApplications.Count != 0)
            {
                sb.AppendLine("- Authorized Applications:");
                sb.AppendLine("");

                foreach (INetFwAuthorizedApplication app in profile.AuthorizedApplications)
                {
                    if (app.Enabled)
                        sb.Append("  " + app.Name);
                    switch (app.Scope)
                    {
                        case NET_FW_SCOPE_.NET_FW_SCOPE_ALL:
                            sb.AppendLine(" Scope All");
                            break;
                        case NET_FW_SCOPE_.NET_FW_SCOPE_CUSTOM:
                            sb.AppendLine(" Scope Custom");
                            break;
                        case NET_FW_SCOPE_.NET_FW_SCOPE_LOCAL_SUBNET:
                            sb.AppendLine(" Scope Local SubNet");
                            break;
                        case NET_FW_SCOPE_.NET_FW_SCOPE_MAX:
                            sb.AppendLine(" Scope MAX");
                            break;
                    }
                }
            }

            if (profile.GloballyOpenPorts.Count != 0)
            {
                sb.AppendLine("");
                sb.AppendLine("- Authorized Ports:");
                sb.AppendLine("");

                foreach (INetFwOpenPort port in profile.GloballyOpenPorts)
                {
                    if (port.Enabled)
                    {
                        sb.Append("  " + port.Name);

                        switch (port.Protocol)
                        {
                            case NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_ANY:
                                sb.AppendLine(" Any " + port.RemoteAddresses + ":" + port.Port);
                                break;
                            case NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_TCP:
                                sb.AppendLine(" TCP " + port.RemoteAddresses + ":" + port.Port);
                                break;
                            case NET_FW_IP_PROTOCOL_.NET_FW_IP_PROTOCOL_UDP:
                                sb.AppendLine(" UDP " + port.RemoteAddresses + ":" + port.Port);
                                break;
                        }
                    }
                }
            }
        }        
    }
}
