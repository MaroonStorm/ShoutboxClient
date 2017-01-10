namespace XenData
{
    public class ForumData
    {
        private string xfToken = "";

        public void SetToken(string t)
        {
            xfToken = t;
        }

        public string GetToken()
        {
            return xfToken;
        }
    }
}
