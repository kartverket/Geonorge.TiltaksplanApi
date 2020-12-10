namespace Geonorge.TiltaksplanApi.Web.Middleware
{
    public class RequestMetrics
    {
        public string User { get; set; }
        public string Method { get; set; }
        public string Path { get; set; }
        public int ResponseCode { get; set; }
        public long ProcessingTime { get; set; }
    }
}
