using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XenData
{
    public class FormattedMessage
    {
        private string userName = "";
        private string userMessage = "";
        private string rawMessage = "";

        private bool messageWhisper = false;

        private int mId = 0;
        private int userId = 0;
        private int messageTime = 0;

        public FormattedMessage(int uId, string uName, string nMessage, string rMessage, int mTime, bool mWhisper)
        {
            userId = uId;
            userName = uName;
            userMessage = nMessage;
            messageTime = mTime;
            messageWhisper = mWhisper;

            rawMessage = rMessage;
        }

        public void SetID(int i)
        {
            mId = i;
        }

        public int GetID()
        {
            return mId;
        }

        public string GetName()
        {
            return userName;
        }

        public string GetMessage()
        {
            return userMessage;
        }

        public int GetTime()
        {
            return messageTime;
        }

        public bool IsWhisper()
        {
            return messageWhisper;
        }
    }
}
