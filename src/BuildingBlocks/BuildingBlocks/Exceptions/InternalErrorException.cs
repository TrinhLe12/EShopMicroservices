namespace BuildingBlocks.Exceptions
{
    public class InternalErrorException : Exception
    {
        public InternalErrorException() : base("Internal error occurred!") { }
    }
}
