using System;
using System.Collections.Generic;

using XenData;

namespace XenManagers
{
    public class MessageManager
    {
        private List<FormattedMessage> messageList = new List<FormattedMessage>();
        private List<String> userList = new List<String>();
        private int lastRefresh = 0;
        private int messageId = 0;

        public void AddMessage(FormattedMessage m)
        {
            m.SetID(messageId);
            messageList.Add(m);

            messageId++;
        }

        public int GetLastID()
        {
            return messageId;
        }

        public List<FormattedMessage> GetMessages()
        {
            return messageList;
        }

        public void ClearMessages()
        {
            messageList.Clear();
        }

        public void SetLastRefresh(int l)
        {
            lastRefresh = l;
        }

        public int GetLastRefresh()
        {
            return lastRefresh;
        }

        public void AddUserList(List<string> u)
        {
            userList = u;
        }

        public List<string> GetUserList()
        {
            return userList;
        }

        public void ClearUserList()
        {
            userList.Clear();
        }
    }
}
