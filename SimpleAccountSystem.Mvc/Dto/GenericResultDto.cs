namespace SimpleAccountSystem.Mvc.Dto
{
    public class GenericResultDto<T>
    {
        public int draw { get; set; } = 1;
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<T>? data { get; set; }
    }
}
