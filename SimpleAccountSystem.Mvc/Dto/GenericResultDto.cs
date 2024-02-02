using System.Diagnostics.CodeAnalysis;

namespace SimpleAccountSystem.Mvc.Dto
{
    [ExcludeFromCodeCoverage]
    public class GenericResultDto<T>
    {
        public int draw { get; set; } = 1;
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public IEnumerable<T>? data { get; set; }
    }
}
