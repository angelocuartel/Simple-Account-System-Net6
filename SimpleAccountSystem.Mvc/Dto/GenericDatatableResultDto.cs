﻿using System.Diagnostics.CodeAnalysis;

namespace SimpleAccountSystem.Mvc.Dto
{
    [ExcludeFromCodeCoverage]
    public class GenericDataTableResultDto
    {
        public int OrderColumn { get; set; }
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public string? OrderDirection { get; set; }
        public string? SearchValue { get; set; }
        public string? SearchRegex { get; set; }
    }
}
