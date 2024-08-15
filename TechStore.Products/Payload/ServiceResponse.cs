namespace TechStore.Products.Payload
{
    public class ServiceResponse
    {
        public int ResponseCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
