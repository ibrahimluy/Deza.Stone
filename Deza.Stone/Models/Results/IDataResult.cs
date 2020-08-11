namespace Deza.Stone
{
    public interface IDataResult<out T> : IResult
    {
        T Data { get; }
    }
}
