namespace Nyelvtanulas
{
    public interface IUserManager
    {
        void Add(Teacher teacher);
        IQueryable<Teacher> GetAll();
    }
}
