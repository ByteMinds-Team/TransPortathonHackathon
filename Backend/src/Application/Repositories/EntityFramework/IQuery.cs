namespace Application.Repositories.EntityFramework;

public interface IQuery<T>
{
    IQueryable<T> Query();
}