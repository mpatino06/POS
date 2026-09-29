using Microsoft.EntityFrameworkCore;
using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Request;
using POS.Infrastructure.Commons.Bases.Response;
using POS.Infrastructure.Persistences.Contexts;
using POS.Infrastructure.Persistences.Interfaces;

namespace POS.Infrastructure.Persistences.Repositories;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{

    public CategoryRepository(PosContext context) : base(context) { }

    public async Task<BaseEntityResponse<Category>> ListCategories(BaseFiltersRequest filters)
    {
        var response = new BaseEntityResponse<Category>();

        //var categories = (from c in _context.Categories
        //                  where c.AuditDeleteUser == null && c.AuditDeleteDate == null
        //                  select c).AsNoTracking().AsQueryable();

        var categories = GetEntityQueryable(c => c.AuditDeleteUser == null && c.AuditDeleteDate == null);

        if (filters.NumFilter is not null && !string.IsNullOrEmpty(filters.TextFilter))
        {
            switch (filters.NumFilter)
            {
                case 1:
                    categories = categories.Where(c => c.Name!.Contains(filters.TextFilter));
                    break;
                case 2:
                    categories = categories.Where(c => c.Description!.Contains(filters.TextFilter));
                    break;
            }
        }

        if (filters.StateFilter is not null)
        {
            categories = categories.Where(c => c.State.Equals(filters.StateFilter));
        }

        if (!string.IsNullOrEmpty(filters.StartDate) && !string.IsNullOrEmpty(filters.EndDate))
        {
            categories = categories.Where(c => c.AuditCreateDate >= Convert.ToDateTime(filters.StartDate) 
                                            && c.AuditCreateDate <= Convert.ToDateTime(filters.EndDate).AddDays(1));
        }

        if (filters.Sort is null) filters.Sort = "Id";

        response.TotalRecords = await categories.CountAsync();
        response.Items = await Ordering(filters, categories, (bool)filters.Download!).ToListAsync();

        return response;
    }

}
