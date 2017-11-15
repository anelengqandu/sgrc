namespace sgrc.DikizaCS.DAL.Utils
{
    public class PagingInfo
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public int ResultCount { get; set; }
        public string SearchString { get; set; }
    }
}