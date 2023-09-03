namespace SimpleAccountSystem.Mvc.Commons
{
    public class GenericListResultDto<T>
    {
        public IEnumerable<T>? List { get; set; }
        public int Page { get; set; } = 10;
        public int Offset { get; set; } = 1;
    }
}
