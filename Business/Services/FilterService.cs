namespace business.Services
{
    using domain;
    using domain.Models;
    using Interfaces;

    public class FilterService : IFilterService<Book>
    {
        private IEnumerable<Book> _enumerable;
        private readonly IFilterModel _filterModel;


        public FilterService(IEnumerable<Book> enumerable, IFilterModel filterModel)
        {
            _enumerable = enumerable;
            _filterModel = filterModel;
        }

        public void FilterAndSort()
        {
            Filter();
            Sort();
        }

        public void Filter()
        {
            _enumerable = _filterModel switch
            {
                var filter when !string.IsNullOrEmpty(_filterModel.Title) => 
                    _enumerable.Where(x => 
                        x.BookDetails.Title.Equals(filter.Title)),
                
                var filter when !string.IsNullOrEmpty(_filterModel.GenreName) => 
                    _enumerable.Where(x => 
                        x.Genres.Any(g => g.Name.Equals(filter.GenreName))),
                
                var filter when !string.IsNullOrEmpty(_filterModel.AuthorName) => 
                    _enumerable.Where(x => 
                        x.Author.Name.Equals(filter.AuthorName)),
                
                var filter when filter.YearOfBook != null => 
                    _enumerable.Where(x => 
                        x.BookDetails.YearOfBook == filter.YearOfBook),
                
                _ => _enumerable,
            };
        }

        public void Sort()
        {
            _enumerable = _filterModel switch
            {
                var filter when filter.OrderByNewer.HasValue && filter.OrderByNewer.Value => 
                    _enumerable.OrderBy(x=>x.BookDetails.YearOfBook).AsEnumerable(),
                
                var filter when filter.OrderByOlder.HasValue && filter.OrderByOlder.Value => 
                    _enumerable.OrderByDescending(x=>x.BookDetails.YearOfBook).AsEnumerable(),
                
                _ => _enumerable,
            };
        }

        public List<Book> ReturnAsList() =>
            _enumerable.ToList();
    }
}
