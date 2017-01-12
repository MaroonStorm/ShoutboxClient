using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Threading;
using System.Text.RegularExpressions;
using XenData;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace XenManagers
{
    public class ConnectionManager
    {
        private ForumData forumData = new ForumData();
        private UserData userData = new UserData();
        private MessageManager messageManager = new MessageManager();

        private CookieContainer cookieContainer = new CookieContainer();

        private string serverUrl;
        private string serverDirectory;
        private string userAgent;

        /// <summary>
        /// Constructor: takes in values, but they are not used (yet).
        /// * This can be pretty much ignored, no changes are needed here.
        /// </summary>
        public ConnectionManager(string s, string d, string a)
        {
            serverUrl = s;
            serverDirectory = d;
            userAgent = a;
        }

        /// <summary>
        /// Main login function.
        /// </summary>
        public async Task<LoginState> Login(string u, string p, string tsa)
        {
            try
            {
                cookieContainer.SetCookies(new Uri("http://age-of-aincrd.com"), "cookie_check=1;");

                string[] loginParams = new string[] { u, p };
                string rLoginData = string.Format("login={0}&register=0&password={1}", loginParams);
                byte[] bLoginData = Encoding.UTF8.GetBytes(rLoginData);

                HttpWebRequest loginRequest = (HttpWebRequest)WebRequest.Create("https://age-of-aincrad.com/forum/login/login");
                loginRequest.Method = "POST";
                loginRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.99 Safari/537.36";
                loginRequest.ContentType = "application/x-www-form-urlencoded";
                loginRequest.Referer = "http://age-of-aincrad.com/forum/";
                loginRequest.CookieContainer = cookieContainer;
                loginRequest.AllowAutoRedirect = false;
                loginRequest.ContentLength = (long)bLoginData.Length;

                Stream loginRequestStream = await loginRequest.GetRequestStreamAsync();
                await loginRequestStream.WriteAsync(bLoginData, 0, bLoginData.Length);
                loginRequestStream.Close();

                HttpWebResponse loginResponse = (HttpWebResponse)await loginRequest.GetResponseAsync();
                loginRequestStream = loginResponse.GetResponseStream();

                StreamReader loginRequestReader = new StreamReader(loginRequestStream);
                string loginRequestData = await loginRequestReader.ReadToEndAsync();

                cookieContainer.Add(loginResponse.Cookies);

                if (loginResponse.StatusCode == HttpStatusCode.SeeOther)
                {
                    if (tsa != "Two Step Code (Optional)" || tsa != "")
                    {
                        HttpWebRequest movePage = (HttpWebRequest)WebRequest.Create("http://age-of-aincrad.com/forum/login/two-step?redirect=http%3A%2F%2Fage-of-aincrad.com%2Fforum%2F&remember=0");
                        movePage.Method = "GET";
                        movePage.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.99 Safari/537.36";
                        movePage.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                        movePage.Referer = "http://age-of-aincrad.com/forum/";
                        movePage.CookieContainer = cookieContainer;

                        string[] tsaParams = new string[] { tsa };
                        string rTsaData = string.Format("remember=0&code={0}", tsaParams);
                        byte[] bTsaData = Encoding.UTF8.GetBytes(rTsaData);

                        HttpWebRequest tfaRequest = (HttpWebRequest)WebRequest.Create("http://age-of-aincrad.com/forum/login/two-step");
                        tfaRequest.Method = "POST";
                        tfaRequest.ContentLength = (long)bTsaData.Length;

                        Stream tfaRequestStream = await tfaRequest.GetRequestStreamAsync();
                        await tfaRequestStream.WriteAsync(bTsaData, 0, bTsaData.Length);
                        tfaRequestStream.Close();

                        HttpWebResponse tfaResponse = (HttpWebResponse)await tfaRequest.GetResponseAsync();
                        tfaRequestStream = tfaResponse.GetResponseStream();

                        StreamReader tfaRequestReader = new StreamReader(loginRequestStream);
                        string tfaRequestData = await loginRequestReader.ReadToEndAsync();
                    }

                    HttpWebRequest tokenRequest = (HttpWebRequest)WebRequest.Create("http://age-of-aincrad.com/forum/");
                    tokenRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.99 Safari/537.36";
                    tokenRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                    tokenRequest.Referer = "http://age-of-aincrad.com/forum/login/two-step?redirect=http%3A%2F%2Fage-of-aincrad.com%2Fforum%2F&remember=0";
                    tokenRequest.CookieContainer = cookieContainer;

                    HttpWebResponse tokenRequestStream = (HttpWebResponse)await tokenRequest.GetResponseAsync();
                    Stream tokenResponseStream = tokenRequestStream.GetResponseStream();

                    StreamReader tokenRequestReader = new StreamReader(tokenResponseStream);
                    string tokenRequestData = await tokenRequestReader.ReadToEndAsync();

                    if (tokenRequestData.Contains("_xfToken"))
                    {
                        string tokenValue = tokenRequestData.Substring(tokenRequestData.IndexOf("_xfToken") + 17);
                        tokenValue = tokenValue.Split(new char[] { '"' })[0];
                        forumData.SetToken(tokenValue);
                    }

                    tokenRequestStream.Close();
                    tokenResponseStream.Close();
                    tokenRequestReader.Close();
                }

                loginResponse.Close();
                loginRequestReader.Close();

                if (forumData.GetToken() != "")
                {
                    userData.SetLoginState(true);
                    userData.SetUsername(u);
                    userData.SetPassword(p);

                    return LoginState.STATE_SUCCESS;
                }

                return LoginState.STATE_FAILED;
            }
            catch(Exception ex)
            {
                return LoginState.STATE_FAILED;
            }
        }

        /// <summary>
        /// This is used along-side Login() to verify if the token was successful when loading a new page.
        /// * This can be pretty much ignored, no changes are needed here.
        /// </summary>
        public async Task<bool> VerifyLogin()
        {
            HttpWebRequest tokenRequest = (HttpWebRequest)WebRequest.Create("http://age-of-aincrad.com/forum/");
            tokenRequest.CookieContainer = cookieContainer;

            HttpWebResponse tokenRequestStream = (HttpWebResponse)await tokenRequest.GetResponseAsync();
            Stream tokenResponseStream = tokenRequestStream.GetResponseStream();
            StreamReader tokenRequestReader = new StreamReader(tokenResponseStream);

            string tokenRequestData = await tokenRequestReader.ReadToEndAsync();
            if (tokenRequestData.Contains("_xfToken"))
            {
                string tokenValue = tokenRequestData.Substring(tokenRequestData.IndexOf("_xfToken") + 17);
                tokenValue = tokenValue.Split(new char[] { '"' })[0];
                forumData.SetToken(tokenValue);

                tokenRequestStream.Close();
                tokenResponseStream.Close();
                tokenRequestReader.Close();

                return true;
            }

            return false;
        }

        public async Task<bool> Send2FA(string k)
        {

            return false;
        }
        /// <summary>
        /// Sends a message to the server.
        /// * This can be pretty much ignored, no changes are needed here.
        /// * This can be ignored
        /// </summary>
        public async Task<string> SendMessage(string message, string colour, bool me)
        {
            string finalMessage = ((me) ? "/me " : "") + (message.StartsWith("/pm") ? message : "[color=" + colour + "]" + message + "[/color]");

            string[] messageParams = new string[]
            {
                finalMessage,
                ((int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds).ToString(),
                colour,
                forumData.GetToken()
            };

            string rMessageData = string.Format("message={0}&sidebar=0&lastrefresh={1}&color={2}&room=1&_xfRequestUri=%2Fforum%2Fshoutbox%2F&_xfNoRedirect=1&_xfToken={3}&_xfResponseType=json", messageParams);
            byte[] bMessageData = Encoding.UTF8.GetBytes(rMessageData);

            HttpWebRequest sendMessageRequest = (HttpWebRequest)WebRequest.Create("https://age-of-aincrad.com/forum/taigachat/post.json");
            sendMessageRequest.CookieContainer = cookieContainer;
            sendMessageRequest.Method = "POST";
            sendMessageRequest.KeepAlive = true;
            sendMessageRequest.ContentType = "application/x-www-form-urlencoded";
            sendMessageRequest.ContentLength = (long)bMessageData.Length;
            sendMessageRequest.Referer = "age-of-aincrad.com/forum/";
            sendMessageRequest.Headers["x-Ajax-Referer"] = "http://age-of-aincrad.com/forum/shoutbox";
            sendMessageRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.99 Safari/537.36";

            Stream messageStream = await sendMessageRequest.GetRequestStreamAsync();
            await messageStream.WriteAsync(bMessageData, 0, bMessageData.Length);
            messageStream.Close();

            try
            {
                HttpWebResponse messageResponse = (HttpWebResponse)await sendMessageRequest.GetResponseAsync();
                messageStream = messageResponse.GetResponseStream();
            }
            catch
            {
                goto Exit;
            }

            StreamReader streamReader = new StreamReader(messageStream);
            string messageStreamData = await streamReader.ReadToEndAsync();

            return messageStreamData;

        Exit:;
            messageStreamData = "";
            return messageStreamData;
        }

        /// <summary>
        /// Used to fetch data from the server, it populates a list of messages and users online.
        /// </summary>
        public async Task<List<FormattedMessage>> GetShoutboxData()
        {
            // Setup the data that will be sent in the POST request
            string[] messageParams = new string[] { messageManager.GetLastRefresh().ToString(), forumData.GetToken() };
            string rMessagesData = string.Format("sidebar=1&lastrefresh={0}&fake=0&room=1&_xfRequestUri=%2Fforum%2F&_xfNoRedirect=1&_xfToken={1}&_xfResponseType=json", messageParams);
            byte[] bMessagesData = Encoding.UTF8.GetBytes(rMessagesData);

            // Opens a HTTP connection to the website
            HttpWebRequest getMessagesRequest = (HttpWebRequest)WebRequest.Create("https://age-of-aincrad.com/forum/taigachat/list.json");
            getMessagesRequest.CookieContainer = cookieContainer;
            getMessagesRequest.Method = "POST";
            getMessagesRequest.KeepAlive = true;
            getMessagesRequest.ContentType = "application/x-www-form-urlencoded";
            getMessagesRequest.ContentLength = (long)bMessagesData.Length;
            getMessagesRequest.Referer = "age-of-aincrad.com/forum/";
            getMessagesRequest.Headers["x-Ajax-Referer"] = "http://age-of-aincrad.com/forum/shoutbox";
            getMessagesRequest.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.99 Safari/537.36";

            // Begins a stream which allows data to be sent to the website
            Stream getMessagesStream = await getMessagesRequest.GetRequestStreamAsync();
            await getMessagesStream.WriteAsync(bMessagesData, 0, bMessagesData.Length);
            getMessagesStream.Close();

            // Waits for response
            HttpWebResponse getMessagesResponse = (HttpWebResponse) await getMessagesRequest.GetResponseAsync();
            getMessagesStream = getMessagesResponse.GetResponseStream();

            // Using the response, we convert it to a string using the stream
            StreamReader getMessagesReader = new StreamReader(getMessagesStream);
            string getMessagesData = await getMessagesReader.ReadToEndAsync();

            // From the stream, we can convert the JSON feed directly to PostData because it has all the parameters that the JSON feed supplies
            PostData postData = await JsonConvert.DeserializeObjectAsync<PostData>(getMessagesData);

            int lastUpdate = 0;
            int userId = -1;

            // Remove all the old messages from the 
            messageManager.ClearMessages();

            // Now we go through all of the messages in the list
            foreach (RawMessage current in postData.messages)
            {
                if (current.html != "")
                {
                    string messageHtml = current.html;
                    bool isWhisper = false;
                    int mLastUpdate = current.last_update;

                    string tUgc = "ugc'";
                    string tAlt = "alt=";
                    string tDataUserId = "data-userid=";

                    // Chops up the raw message HTML to fetch the username
                    string sUserName = messageHtml;
                    sUserName = sUserName.Substring(sUserName.IndexOf(tAlt) + tAlt.Length + 1);
                    sUserName = sUserName.Substring(0, sUserName.IndexOf("\""));

                    // ... the message itself
                    string sUserMessage = messageHtml;
                    sUserMessage = sUserMessage.Substring(sUserMessage.IndexOf(tUgc) + tUgc.Length + 1);
                    sUserMessage = sUserMessage.Substring(0, sUserMessage.IndexOf("div>") - 2);

                    // ... and the user ID
                    string sUserId = messageHtml;
                    sUserId = sUserId.Substring(sUserId.IndexOf(tDataUserId) + tDataUserId.Length + 1);
                    sUserId = sUserId.Substring(0, sUserId.IndexOf('"'));

                    // Now check if the message was a whisper/pm message
                    isWhisper = (messageHtml.Contains("&raquo;") ? true : false);

                    // Convert the user ID string to int
                    try
                    {
                        userId = Convert.ToInt32(sUserId);
                    }
                    catch
                    {
                        System.Windows.MessageBox.Show("Unable to convert UserID to a valid int.");
                    }

                    // Add the message to the list
                    messageManager.AddMessage(new FormattedMessage(userId, sUserName, sUserMessage, messageHtml, mLastUpdate, isWhisper));

                    // If this message is newer than the last update timestamp, keep record of this new timestamp
                    if (mLastUpdate > lastUpdate)
                    {
                        lastUpdate = mLastUpdate;
                    }
                }
            }

            // Once the loop has been completed, just log the new timestamp for future use
            if (messageManager.GetMessages().Count > 0)
            {
                messageManager.SetLastRefresh(lastUpdate + 1);
            }

            // Now we can fetch the members in chat data by using Regular Expressions
            string onlineUsers = Regex.Replace(postData.onlineUsers, "<.*?\n.*?>", String.Empty);
            onlineUsers = Regex.Replace(onlineUsers, "<.*?>", String.Empty);
            onlineUsers = Regex.Replace(onlineUsers, @"\s+", " ");
            onlineUsers = Regex.Replace(onlineUsers, "Members in Chat \\(.*?\\) ", "");

            // Now we have a string of only usernames that are separated by a comma, we can populate a list of users
            List<string> userList = new List<string>();
            foreach (string u in onlineUsers.Split(','))
            {
                userList.Add(u);
            }

            // And save this list into the message manager
            messageManager.AddUserList(userList);

            // Now return the messages that were fetched
            return messageManager.GetMessages();
        }

        /// <summary>
        /// Returns the current forum data.
        /// </summary>
        public ForumData GetForumData()
        {
            return forumData;
        }

        /// <summary>
        /// Returns the current user data.
        /// </summary>
        public UserData GetUserData()
        {
            return userData;
        }

        /// <summary>
        /// Returns a list of users from the MessageManager.
        /// </summary>
        public List<string> GetUsers()
        {
            return messageManager.GetUserList();
        }

        /// <summary>
        /// Returns the last message ID.
        /// </summary>
        public int GetLastMessageID()
        {
            return messageManager.GetLastID();
        }
    }
}
