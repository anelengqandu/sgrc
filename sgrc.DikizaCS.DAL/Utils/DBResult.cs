

namespace sgrc.DikizaCS.DAL.Utils
{
    public class DBResult
    {
        public bool Success = true;
        public bool ThrewError = false;
        public string TitleText;
        public string DescripText;
        public string Status;
        public object Object;
      
        public DBResult() { }

        public DBResult(bool _success)
        {
            Success = _success;
        }

        public DBResult(bool _success, string _titleText, string _descripText)
        {
            Success = _success;
            TitleText = _titleText;
            DescripText = _descripText;
        }

        public DBResult(string _errorTitleText, string _descripText, object data)
        {
            Success = false;
            ThrewError = true;
            TitleText = _errorTitleText;
            DescripText = _descripText;
        }
    }

}
