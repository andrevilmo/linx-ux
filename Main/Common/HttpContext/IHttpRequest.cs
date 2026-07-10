namespace LinxHttpContext
{
    public interface IHttpRequest
    {
        bool IsLocal { get; }
        string UserHostAddress { get; }
        object Inner { get; }
    }
}