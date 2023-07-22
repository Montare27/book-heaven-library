namespace Interfaces
{
    using System.Linq.Expressions;

    public interface IFilterService<T>
    {
        public void FilterAndSort();
        public void Filter();
        public void Sort();

        public List<T> ReturnAsList();
    }
}
