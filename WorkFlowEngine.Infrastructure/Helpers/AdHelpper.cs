using Microsoft.Extensions.Options;
using System.DirectoryServices;
using WorkFlowEngine.Infrastructure.Abstraction;
using WorkFlowEngine.Shared.Settings;

namespace WorkFlowEngine.Infrastructure.Helpers
{

    public class AdHelpper : IAdHelpper
    {
        private readonly AdSettings _adSettings;

        public AdHelpper(IOptions<AdSettings> config)
        {
            _adSettings = config.Value;
        }
        public bool CheckUserInGroup(string userName, string GroupName)
        {
            var de = new DirectoryEntry(_adSettings.Path, _adSettings.User, _adSettings.Password, AuthenticationTypes.Secure);
            DirectorySearcher deSearch = new DirectorySearcher();
            deSearch.SearchRoot = de;
            userName = GetUsernameWithoutDomain(userName);
            if (_adSettings.Path.Contains("OU"))
            {
                deSearch.Filter = "(&(SAMAccountName=" + userName + ")(objectClass=user)(objectCategory=person)(memberof=CN=" + GroupName + "," + _adSettings.Path.Substring(_adSettings.Path.IndexOf("OU")) + "))";
            }
            else
            {
                if (!string.IsNullOrEmpty(_adSettings.SearchFilterPostfix))
                {
                    deSearch.Filter = "(&(SAMAccountName=" + userName + ")(objectClass=user)(objectCategory=person)(memberof=CN=" + GroupName + "," + _adSettings.SearchFilterPostfix + "))";
                }
                else
                {
                    deSearch.Filter = "(&(SAMAccountName=" + userName + ")(objectClass=user)(objectCategory=person)(memberof=CN=" + GroupName + "," + _adSettings.Path.Substring(_adSettings.Path.IndexOf("DC")) + "))";
                }
            }
            deSearch.SearchScope = SearchScope.Subtree;
            SearchResultCollection results = deSearch.FindAll();
            return results.Count > 0;
        }

        public List<string> GetUserGroups(string userName)
        {
            DirectoryEntry de = new DirectoryEntry(_adSettings.Path, _adSettings.User, _adSettings.Password, AuthenticationTypes.Secure);
            DirectorySearcher deSearch = new DirectorySearcher();
            deSearch.SearchRoot = de;
            userName = GetUsernameWithoutDomain(userName);
            deSearch.Filter = "(SAMAccountName=" + userName + ")";   //"(&(objectClass=user)(cn=" + UserName + "))";
            deSearch.CacheResults = true;
            deSearch.PropertiesToLoad.Add("MemberOf");
            deSearch.SearchScope = SearchScope.Subtree;
            SearchResult results = deSearch.FindOne();
            List<string> GroupsNames = new List<string>();

            if (results != null)
            {
                DirectoryEntry entry = results.GetDirectoryEntry();

                if (entry.Properties["MemberOf"].Value != null)
                {
                    if (entry.Properties["MemberOf"].Value is object[])
                    {
                        GroupsNames = ((object[])entry.Properties["MemberOf"].Value).Select(obj => obj.ToString()).ToList();
                    }
                    else
                    {
                        GroupsNames.Add(entry.Properties["MemberOf"].Value.ToString());
                    }
                }
            }

            List<string> ReturnedGroupsNames = new List<string>();

            foreach (string groupName in GroupsNames)
            {
                string tempGroupName = groupName.Substring(groupName.IndexOf("=") + 1);
                tempGroupName = tempGroupName.Substring(0, tempGroupName.IndexOf(","));
                ReturnedGroupsNames.Add(tempGroupName);
            }

            return ReturnedGroupsNames;
        }

        private string GetUsernameWithoutDomain(string username)
        {
            if (username.Contains("\\"))
                return username.Substring(username.IndexOf("\\") + 1);
            return username;

        }
    }
}
