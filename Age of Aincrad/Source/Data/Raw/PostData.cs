using System;
using System.Collections.Generic;

namespace XenData
{
    public class PostData
    {
        public string robots
        {
            get;
            set;
        }
        public List<RawMessage> messages
        {
            get;
            set;
        }
        public List<int> messageIds
        {
            get;
            set;
        }
        public string onlineUsers
        {
            get;
            set;
        }
        public string reverse
        {
            get;
            set;
        }
        public int lastrefresh
        {
            get;
            set;
        }
        public string motd
        {
            get;
            set;
        }
        public int numInChat
        {
            get;
            set;
        }
        public bool twelveHour
        {
            get;
            set;
        }
        public string _visitor_conversationsUnread
        {
            get;
            set;
        }
        public string _visitor_alertsUnread
        {
            get;
            set;
        }
    }
}
