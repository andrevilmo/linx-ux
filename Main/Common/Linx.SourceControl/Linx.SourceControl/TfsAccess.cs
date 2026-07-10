using EnvDTE;
using Microsoft.TeamFoundation.Client;
using Microsoft.TeamFoundation.VersionControl.Client;
using System;
using System.IO;

namespace Linx.SourceControl
{
    public static class TfsAccess
    {
        public static bool VerifySourceControl(DTE appDTE, string outputFile)
        {
            var workspaceInfo = Workstation.Current.GetLocalWorkspaceInfo(outputFile);

            if (!appDTE.SourceControl.IsItemUnderSCC(outputFile) || appDTE.SourceControl.IsItemCheckedOut(outputFile) || workspaceInfo == null)
                return true;

            TfsTeamProjectCollection server = new TfsTeamProjectCollection(workspaceInfo.ServerUri);
            Workspace sourceWorkspace = workspaceInfo.GetWorkspace(server);
            return sourceWorkspace.PendEdit(outputFile) > 0;
        }

        public static string GetWorkspaceMappedPath(string dirToCheck)
        {
            string maps = String.Empty;
            WorkspaceInfo[] aWs = Workstation.Current.GetAllLocalWorkspaceInfo();
            foreach (var ws in aWs)
            {
                foreach (var mPath in ws.MappedPaths)
                {
                    if (!string.IsNullOrWhiteSpace(mPath))
                        maps += (string.IsNullOrWhiteSpace(maps) ? "" : "\r\n") + mPath;
                    if (!string.IsNullOrWhiteSpace(mPath) && Directory.Exists(mPath))
                    {
                        if (string.IsNullOrWhiteSpace(dirToCheck))
                            return mPath;
                        else
                        {
                            if (Directory.Exists(Path.Combine(mPath, dirToCheck)))
                                return mPath;
                        }
                    }
                }
            }
            return String.Empty;
        }
    }
}
