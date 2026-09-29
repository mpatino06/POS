namespace POS.Infrastructure.Commons.Bases.Request
{
    public class BaseFiltersRequest : BasePaginationRequest
    {
        public int? NumFilter { get; set; }
        public string? TextFilter { get; set; }
        public int? StateFilter { get; set; }
        public string? StartDate { get; set; } = null;
        public string? EndDate { get; set;} = null;
        public bool? Download { get; set; } = false;

    }
}
