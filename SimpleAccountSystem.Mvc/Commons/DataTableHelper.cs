using SimpleAccountSystem.Mvc.Dto;

namespace SimpleAccountSystem.Mvc.Commons
{
    public static class DataTableHelper
    {
        private const string ORDER_COLUMN = "order[0][column]";
        private const string ORDER_DIRECTION = "order[0][dir]";
        private const string SEARCH_VALUE = "search[value]";
        private const string SEARCH_REGEX = "search[regex]";
        private const string DRAW = "draw";
        private const string START = "start";
        private const string LENGTH = "length";

        public static GenericDataTableResultDto ExtractGenericQueryData(this IQueryCollection queryCollection)
        {
            var result = new GenericDataTableResultDto
            {
                OrderColumn = Convert.ToInt32(queryCollection[ORDER_COLUMN])
               ,Draw = Convert.ToInt32(queryCollection[DRAW])
               ,Start = Convert.ToInt32(queryCollection[START])
               ,Length= Convert.ToInt32(queryCollection[LENGTH])
               ,OrderDirection = queryCollection[ORDER_DIRECTION]
               ,SearchValue = queryCollection[SEARCH_VALUE]
               ,SearchRegex = queryCollection[SEARCH_REGEX]
            };

            return result;

        }
    }
}
