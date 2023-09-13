namespace SimpleAccountSystem.Mvc.Dto
{
    public class GenericDataTableResultDto
    {

        public int OrderColumn { get; set; }
        public int Start { get; set; } = 1;
        public int Length { get; set; } = 10;
        public int Draw { get; set; } = 1;

        public string? OrderDirection { get; set; }
        public string? SearchValue { get; set; }
        public string? SearchRegex { get; set; }
    }
}
